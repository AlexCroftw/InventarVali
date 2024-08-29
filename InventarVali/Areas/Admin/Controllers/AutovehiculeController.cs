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

        public IActionResult Delete(int id) 
        {
            if (id == null || id == 0) 
            {
                return NotFound();
            }

            Autovehicule obj = _unitOfWork.Autovehicule.Get(x => x.Id == id);

            if (obj == null) 
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteVehicule(int id) 
        {
            Autovehicule obj = _unitOfWork.Autovehicule.Get(x => x.Id == id);

            if (id == null || id == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Autovehicule.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "The Autovehicule  was deleted successfully";

                return RedirectToAction("Index", "Autovehicule");
            }
            else
            {
                return View();
            }

            
        }
        #region API CALL
        [HttpGet]
        public IActionResult Getall()
        {
            List<Autovehicule> objAutovehiculeList = _unitOfWork.Autovehicule.GetAll(includeProperties: "Employees").ToList();
            return Json(new { data = objAutovehiculeList });
        }
        #endregion
    }
}

