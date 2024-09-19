using AutoMapper;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using InventarVali.Models.ViewModel;
using InventarVali.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventarVali.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ComputerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        public ComputerController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            List<Computer> objComputerList = _unitOfWork.Computer.GetAll(includeProperties: "Employees").ToList();
            var computerVM = _mapper.Map<List<ComputerVM>>(objComputerList);
            return View(computerVM);
        }

        public IActionResult Upsert(int? id)
        {
            ComputerDetailsVM computerDetailswVM = new()
            {
                EmployeeList = _unitOfWork.Employee.GetAll().Select(u => new SelectListItem
                {
                    Text = u.FullName,
                    Value = u.Id.ToString()
                }),
                Computer = new ComputerVM()
            };

            if (id == null || id == 0)
            {
                //Create
                return View(computerDetailswVM);
            }
            else
            {
                //Update
                var computer = _unitOfWork.Computer.Get(o => o.Id == id);
                computerDetailswVM.Computer = _mapper.Map<ComputerVM>(computer);
                return View(computerDetailswVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ComputerDetailsVM computerDetailsVM, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                var model = _mapper.Map<Computer>(computerDetailsVM.Computer);

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string computerPath = Path.Combine(wwwRootPath, @"images\computer");



                    if (!string.IsNullOrEmpty(model.ImageUrl))
                    {
                        //Delete old img
                        var oldimgPath = Path.Combine(wwwRootPath, computerDetailsVM.Computer.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldimgPath))
                        {
                            System.IO.File.Delete(oldimgPath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(computerPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    model.ImageUrl = @"\images\computer\" + fileName;
                }


                if (model.Id == 0)
                {
                    _unitOfWork.Computer.Add(model);
                }
                else
                {
                    _unitOfWork.Computer.Update(model);

                }

                _unitOfWork.Save();
                TempData["success"] = "Computer was created successfully";

                return RedirectToAction("Index", "Computer");
            }
            else
            {

                computerDetailsVM.EmployeeList = _unitOfWork.Employee.GetAll().Select(u => new SelectListItem
                {
                    Text = u.FullName,
                    Value = u.Id.ToString()
                });
                return View(computerDetailsVM);
            }

        }


        #region API CALLS
        [HttpGet]
        public IActionResult Getall()
        {
            List<Computer> objComputerList = _unitOfWork.Computer.GetAll(includeProperties: "Employees").ToList();
            return Json(new { data = objComputerList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var computerToBeDeleted = _unitOfWork.Computer.Get(o => o.Id == id);
            if (computerToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            if (!string.IsNullOrEmpty(computerToBeDeleted.ImageUrl))
            {
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, computerToBeDeleted.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

            }

            _unitOfWork.Computer.Remove(computerToBeDeleted);
            _unitOfWork.Save();
            TempData["success"] = "Computer was deleted successfully";
            return Json(new { success = true, message = "Deleted Successfully" });
        }
        #endregion
    }
}
