using AutoMapper;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using InventarVali.Models.ViewModel;
using InventarVali.Utility;
using InventarVali.Utility.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace InventarVali.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class AutovehiculeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly IMyEmailSender _myEmailSender;
        public AutovehiculeController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IMapper mapper, IMyEmailSender myEmailSender)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _myEmailSender = myEmailSender;
        }
        public IActionResult Index()
        {  var now = DateTime.Now;
            List<Autovehicule> objAutovehiculeslist = _unitOfWork.Autovehicule.GetAll(includeProperties: "Employees").ToList();
             foreach (var date in objAutovehiculeslist)
             {
                if (date.InsuranceExpirationDate.HasValue)
                {
                    TimeSpan diff = date.InsuranceExpirationDate.Value - now;
                    if (diff.Days <= 14) 
                    {
                        _myEmailSender.SendEmail("alex.croitoru@kmre.ro", $"Insurance Expiration Date  {now}", $"Please be informed that insurence for {date.LicensePlate} is expiring " +
                            $" in {diff.Days} days. Please Take Action ");
                    }
                }
                if (date.ITPExpirationDate.HasValue)
                {
                    TimeSpan diff = date.ITPExpirationDate.Value - now;
                    if (diff.Days <= 14)
                    {
                        _myEmailSender.SendEmail("alex.croitoru@kmre.ro", $"ITP Expiration  Date  {now}", $"Please be informed that ITP for {date.LicensePlate} is expiring " +
                            $" in {diff.Days} days. Please Take Action ");
                    }
                }
                if (date.VinietaExpirationDate.HasValue)
                {
                    TimeSpan diff = date.VinietaExpirationDate.Value - now;
                    if (diff.Days <= 14)
                    {
                        _myEmailSender.SendEmail("alex.croitoru@kmre.ro", $"Vinieta Expiration Date  {now}", $"Please be informed that Vinieta for {date.LicensePlate} is expiring " +
                            $" in {diff.Days} days. Please Take Action ");
                    }
                }
            }

            var autovehicule = _mapper.Map<List<AutovehiculeVM>>(objAutovehiculeslist);
            return View(autovehicule);
        }

        public IActionResult Upsert(int? id) 
        {
            AutovehiculeDetailsVM autovehiculeVM = new()
            {
                EmployeeList = _unitOfWork.Employee.GetAll().Select(u => new SelectListItem
                {
                    Text = u.FullName,
                    Value = u.Id.ToString()
                }),
                Autovehicule = new AutovehiculeVM()
            };

            if (id == null || id == 0)
            {
                //Create
                return View(autovehiculeVM);
            }
            else 
            {

                //Update
                var autovehicule = _unitOfWork.Autovehicule.Get(o => o.Id == id);
                autovehiculeVM.Autovehicule = _mapper.Map<AutovehiculeVM>(autovehicule);
                return View(autovehiculeVM);
            }
            
        }
        [HttpPost]
        public IActionResult Upsert(AutovehiculeDetailsVM autovehiculeVM, IFormFile? file) 
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<Autovehicule>(autovehiculeVM.Autovehicule);
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null) 
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string autovehiculePath = Path.Combine(wwwRootPath, @"images\autovehicule");

                    if (!string.IsNullOrEmpty(model.ImageUrl)) 
                    {
                        //Delete old img
                        var oldimgPath = Path.Combine(wwwRootPath, model.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldimgPath))
                        {
                            System.IO.File.Delete(oldimgPath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(autovehiculePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    model.ImageUrl = @"\images\autovehicule\" + fileName;
                }
                
                if (model.Id == 0)
                {
                    _unitOfWork.Autovehicule.Add(model);
                }
                else 
                {
                    _unitOfWork.Autovehicule.Update(model);

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
            var autovehicule = _mapper.Map<List<AutovehiculeVM>>(objAutovehiculeList);
            return Json(new { data = autovehicule });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var autovehiculeToBeDeleted = _unitOfWork.Autovehicule.Get(o => o.Id == id);
            if (autovehiculeToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            if (!string.IsNullOrEmpty(autovehiculeToBeDeleted.ImageUrl))
            {
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, autovehiculeToBeDeleted.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            
            _unitOfWork.Autovehicule.Remove(autovehiculeToBeDeleted);
            _unitOfWork.Save();

            TempData["success"] = "Autovehicule was deleted successfully";

            return Json(new { success = true, message = "Deleted Successfully" });
        }
        #endregion
    }
}

