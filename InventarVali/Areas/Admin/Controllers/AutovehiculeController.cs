using InventarVali.DataAccess.Repository;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using InventarVali.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography.X509Certificates;

namespace InventarVali.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AutovehiculeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AutovehiculeController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Autovehicule> objAutovehiculeslist = _unitOfWork.Autovehicule.GetAll(includeProperties: "Employees").ToList();
            return View(objAutovehiculeslist);
        }

        public IActionResult Upsert(int? id) 
        {
            AutovehiculeVM autovehiculeVM = new()
            {
                EmployeeList = _unitOfWork.Employee.GetAll().Select(u => new SelectListItem
                {
                    Text = u.FullName,
                    Value = u.Id.ToString()
                }),
                Autovehicule = new Autovehicule()
            };

            if (id == null || id == 0)
            {
                //Create
                return View(autovehiculeVM);
            }
            else 
            {
                //Update
                autovehiculeVM.Autovehicule = _unitOfWork.Autovehicule.Get(o => o.Id == id);
                return View(autovehiculeVM);
            }
            
        }
        [HttpPost]
        public IActionResult Upsert(AutovehiculeVM autovehiculeVM, IFormFile? file) 
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null) 
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string autovehiculePath = Path.Combine(wwwRootPath, @"images\autovehicule");

                    if (!string.IsNullOrEmpty(autovehiculeVM.Autovehicule.ImageUrl)) 
                    {
                        //Delete old img
                        var oldimgPath = Path.Combine(wwwRootPath, autovehiculeVM.Autovehicule.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldimgPath))
                        {
                            System.IO.File.Delete(oldimgPath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(autovehiculePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    
                    autovehiculeVM.Autovehicule.ImageUrl = @"\images\autovehicule\" + fileName;
                }

                if (autovehiculeVM.Autovehicule.Id == 0)
                {
                    _unitOfWork.Autovehicule.Add(autovehiculeVM.Autovehicule);
                }
                else 
                {
                    _unitOfWork.Autovehicule.Update(autovehiculeVM.Autovehicule);

                }
                
                _unitOfWork.Save();
                TempData["success"] = "Autovehicule was created successfully";

                return RedirectToAction("Index", "Autovehicule");
            }
            else 
            {
                autovehiculeVM.EmployeeList = _unitOfWork.Employee.GetAll().Select(u => new SelectListItem 
                {
                    Text = u.FullName,
                    Value = u.Id.ToString()
                });
                return View(autovehiculeVM);
            }
        }

       
        #region API CALL
        [HttpGet]
        public IActionResult Getall()
        {
            List<Autovehicule> objAutovehiculeList = _unitOfWork.Autovehicule.GetAll(includeProperties: "Employees").ToList();
            return Json(new { data = objAutovehiculeList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var autovehiculeToBeDeleted = _unitOfWork.Autovehicule.Get(o => o.Id == id);
            if (autovehiculeToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
                
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, autovehiculeToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Autovehicule.Remove(autovehiculeToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Deleted Successfully" });
        }
        #endregion
    }
}

