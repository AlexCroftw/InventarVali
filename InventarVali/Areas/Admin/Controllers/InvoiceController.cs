using AutoMapper;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using InventarVali.Models.ViewModel;
using InventarVali.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace InventarVali.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class InvoiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        public InvoiceController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IMapper mapper)

        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            List<Invoice> invoiceList = _unitOfWork.Invoice.GetAll(includeProperties: "Autovehicule").ToList();
            List<Autovehicule> autovehiculesList = _unitOfWork.Autovehicule.GetAll().ToList();

            foreach (var item in invoiceList)
            {
                item.Autovehicule.AddRange(autovehiculesList);
            }

            var invoiceVM = _mapper.Map<List<InvoiceVM>>(invoiceList);

            return View(invoiceVM);
        }

        public IActionResult DisplayInvoice(int? id) 
        {
            if (id == null || id == 0) 
            {
                return BadRequest();
            }

            var invoice = _unitOfWork.Invoice.Get(x => x.Id == id);
            var invoiceVM = _mapper.Map<InvoiceVM>(invoice);

            if (!String.IsNullOrEmpty(invoiceVM.InvoiceUrl)) 
            {
                return View(invoiceVM);
            }

           return NotFound();
        }

        public IActionResult Upsert(int? id)
        {
            var licensePlate = _unitOfWork.Autovehicule.GetAll().ToList();
            var licensePLateVM = _mapper.Map<List<AutovehiculeVM>>(licensePlate);

            InvoiceVM invoiceDetailsVM = new();
            {
                invoiceDetailsVM.Autovehicule = licensePLateVM;
            }


            if (id == null || id == 0)
            {
                invoiceDetailsVM.InvoiceDate = DateTime.Now;
                //Create
                return View(invoiceDetailsVM);
            }
            else
            {
                var invoice = _unitOfWork.Invoice.Get(x => x.Id == id);
                invoiceDetailsVM = _mapper.Map<InvoiceVM>(invoice);

                return View(invoiceDetailsVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(InvoiceVM invoiceDetailsVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<Invoice>(invoiceDetailsVM);
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null && Path.GetExtension(file.FileName) == ".pdf")
                {
                    string fileName = model.InvoiceNumber + Path.GetExtension(file.FileName);

                    string invoicePdfPath = Path.Combine(wwwRootPath, @"invoice");

                    if (!string.IsNullOrEmpty(model.InvoiceUrl))
                    {
                        var oldimgPath = Path.Combine(wwwRootPath, model.InvoiceUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldimgPath))
                        {
                            System.IO.File.Delete(oldimgPath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(invoicePdfPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    model.InvoiceUrl = @"\invoice\" + fileName;
                }


                if (model.Id == 0)
                {
                    _unitOfWork.Invoice.Add(model);
                }
                else
                {
                    _unitOfWork.Invoice.Update(model);

                }
                _unitOfWork.Save();
                TempData["success"] = "Invoice  was created successfully";

                return RedirectToAction("Index", "Invoice");
            }
            else
            {
                var licensePlate = _unitOfWork.Autovehicule.GetAll().ToList();
                var licensePLateVM = _mapper.Map<List<AutovehiculeVM>>(licensePlate);

                invoiceDetailsVM.Autovehicule = licensePLateVM;
                return View(invoiceDetailsVM);
            }
        }

        #region APICALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Invoice> invoiceList = _unitOfWork.Invoice.GetAll(includeProperties: "Autovehicule").ToList();
            List<Autovehicule> autovehiculesList = _unitOfWork.Autovehicule.GetAll().ToList();

            foreach (var item in invoiceList)
            {
                item.Autovehicule.AddRange(autovehiculesList);
            }

            var invoiceVM = _mapper.Map<List<InvoiceVM>>(invoiceList);

            return Json(new { data = invoiceVM });
        }

        [HttpDelete]

        public IActionResult Delete(int? id)
        {
            var invoiceToBeDeleted = _unitOfWork.Invoice.Get(o => o.Id == id);

            if (invoiceToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            if (!string.IsNullOrEmpty(invoiceToBeDeleted.InvoiceUrl))
            {
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, invoiceToBeDeleted.InvoiceUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _unitOfWork.Invoice.Remove(invoiceToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Deleted Successfully" });
        }

        #endregion
    }
}

