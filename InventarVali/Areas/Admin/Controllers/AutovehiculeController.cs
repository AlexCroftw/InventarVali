using AutoMapper;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using InventarVali.Models.ViewModel;
using InventarVali.Utility;
using InventarVali.Utility.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


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
        private readonly IConfiguration _config;

        public AutovehiculeController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IMapper mapper, IMyEmailSender myEmailSender, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _myEmailSender = myEmailSender;
            _config = config;
        }
        public IActionResult Index()
        {
            List<Autovehicule> objAutovehiculeslist = _unitOfWork.Autovehicule.GetAll(includeProperties: "Employees").ToList();
            var autovehicule = _mapper.Map<List<AutovehiculeVM>>(objAutovehiculeslist);
            return View(autovehicule);
        }

        public IActionResult GetInsurenceDoc(int? id)
        {
            
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var autovehicule = _unitOfWork.Autovehicule.Get(x => x.Id == id);

            var model = _mapper.Map<AutovehiculeVM>(autovehicule);

            if (string.IsNullOrEmpty(model.InsurenceDoc))
            {
                return NotFound();
            }

            return View(model);

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
                autovehiculeVM.Autovehicule.InsurenceDate = DateTime.Now;
                autovehiculeVM.Autovehicule.InsuranceExpirationDate = DateTime.Now.AddYears(1);
                autovehiculeVM.Autovehicule.ITPExpirationDate = DateTime.Now.AddYears(1);
                autovehiculeVM.Autovehicule.VinietaExpirationDate = DateTime.Now.AddYears(1);
                //Create
                return View(autovehiculeVM);
            }
            else
            {

                //Update
                var autovehicule = _unitOfWork.Autovehicule.Get(o => o.Id == id);
                autovehiculeVM.Autovehicule = _mapper.Map<AutovehiculeVM>(autovehicule);
                //Display Bool Value

                return View(autovehiculeVM);
            }

        }
        [HttpPost]
        public IActionResult Upsert(AutovehiculeDetailsVM autovehiculeVM, List<IFormFile?> file)
        {
            if (ModelState.IsValid)
            {
                
              
                var model = _mapper.Map<Autovehicule>(autovehiculeVM.Autovehicule);
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                foreach (var item in file)
                {
                    //Guid.NewGuid().ToString()
                    string fileName = model.LicensePlate + Path.GetExtension(item.FileName);

                    if (item != null && item.FileName.Contains("jpeg") || item.FileName.Contains("png"))
                    {
                        string autovehiculeImgPath = Path.Combine(wwwRootPath, @"images\autovehicule");

                        if (!string.IsNullOrEmpty(model.ImageUrl))
                        {
                            //Delete old img
                            var oldimgPath = Path.Combine(wwwRootPath, model.ImageUrl.TrimStart('\\'));

                            if (System.IO.File.Exists(oldimgPath))
                            {
                                System.IO.File.Delete(oldimgPath);
                            }
                        }

                        //Create File Img
                        using (var fileStream = new FileStream(Path.Combine(autovehiculeImgPath, fileName), FileMode.Create))
                        {
                            item.CopyTo(fileStream);
                        }
                        model.ImageUrl = @"\images\autovehicule\" + fileName;
                    }
                    else if (item != null && item.FileName.Contains("docx") || item.FileName.Contains("pdf"))
                    {
                        string autovehiculeImgPath = Path.Combine(wwwRootPath, @"files\autovehicule");

                        if (!string.IsNullOrEmpty(model.InsurenceDoc))
                        {
                            //Delete old img
                            var oldimgPath = Path.Combine(wwwRootPath, model.InsurenceDoc.TrimStart('\\'));

                            if (System.IO.File.Exists(oldimgPath))
                            {
                                System.IO.File.Delete(oldimgPath);
                            }
                        }

                        //Create File Img
                        using (var fileStream = new FileStream(Path.Combine(autovehiculeImgPath, fileName), FileMode.Create))
                        {
                            item.CopyTo(fileStream);
                        }
                        model.InsurenceDoc = @"\files\autovehicule\" + fileName;
                    }
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


        #region VALIDATORS
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyPlate(AutovehiculeVM autovehicule)
        {
            bool valid = Regex.IsMatch(autovehicule.LicensePlate, @"^[A-Z]{1,3}\s\d{1,3}\s[A-Z]{3}$");
            if (!valid)
            {
                return Json($"License {autovehicule.LicensePlate} has an invalid format. Format: VL-05-ESK");
            }
            //Verify if Plate is unique
            List<Autovehicule> objAutovehiculeslist = _unitOfWork.Autovehicule.GetAll(includeProperties: "Employees").ToList();
            var autovehiculeVMList = _mapper.Map<List<AutovehiculeVM>>(objAutovehiculeslist);
            foreach (var item in autovehiculeVMList) 
            {
                if (item.LicensePlate == autovehicule.LicensePlate) 
                {
                    return Json($"{autovehicule.LicensePlate} is already taken ");
                }
            }
            return Json(true);
        }

        #endregion

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

