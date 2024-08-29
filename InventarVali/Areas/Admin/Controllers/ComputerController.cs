using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using InventarVali.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventarVali.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ComputerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ComputerController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Computer> objComputerList = _unitOfWork.Computer.GetAll(includeProperties : "Employees").ToList();
            return View(objComputerList);
        }

        public IActionResult Upsert(int? id) 
        {
            ComputerVM computerVM = new()
            {
                EmployeeList = _unitOfWork.Employee.GetAll().Select(u => new SelectListItem
                {
                    Text = u.FullName,
                    Value = u.Id.ToString()
                }),
                Computer = new Computer()
            };

            if (id == null || id == 0)
            {
                //Create
                return View(computerVM);
            }
            else
            {
                //Update
                computerVM.Computer = _unitOfWork.Computer.Get(o => o.Id == id);
                return View(computerVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ComputerVM computerVM, IFormFile file) 
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string computerPath = Path.Combine(wwwRootPath, @"images\computer");

                   

                    if (!string.IsNullOrEmpty(computerVM.Computer.ImageUrl))
                    {
                        //Delete old img
                        var oldimgPath = Path.Combine(wwwRootPath, computerVM.Computer.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldimgPath))
                        {
                            System.IO.File.Delete(oldimgPath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(computerPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    computerVM.Computer.ImageUrl = @"\images\computer\" + fileName;
                }

                if (computerVM.Computer.Id == 0)
                {
                    _unitOfWork.Computer.Add(computerVM.Computer);
                }
                else
                {
                    _unitOfWork.Computer.Update(computerVM.Computer);

                }

                _unitOfWork.Save();
                TempData["success"] = "Computer was created successfully";

                return RedirectToAction("Index", "Computer");
            }
            else
            {
                computerVM.EmployeeList = _unitOfWork.Employee.GetAll().Select(u => new SelectListItem
                {
                    Text = u.FullName,
                    Value = u.Id.ToString()
                });
                return View(computerVM);
            }

        }

        public IActionResult Delete(int id) 
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Computer obj = _unitOfWork.Computer.Get(o => o.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteComputer(int id) 
        {
            Computer obj = _unitOfWork.Computer.Get(o => o.Id == id);
            if (id == 0 || id == null) 
            {
                return NotFound();
            }
            if (ModelState.IsValid) 
            {
                _unitOfWork.Computer.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "The Computer was deleted successfully";
                return RedirectToAction("Index", "Computer");
            }
            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult Getall() 
        {
            List<Computer> objComputerList = _unitOfWork.Computer.GetAll(includeProperties: "Employees").ToList();
            return Json(new { data = objComputerList });
        }
        #endregion
    }
}
