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
            List<Invoice> invoiceList = _unitOfWork.Invoice.GetAll(includeProperties:"Autovehicule").ToList();
            List<Autovehicule> autovehiculesList = _unitOfWork.Autovehicule.GetAll().ToList();

            foreach (var item in invoiceList) 
            {
                item.Autovehicule.AddRange(autovehiculesList);
            }

            var invoiceVM = _mapper.Map<List<InvoiceVM>>(invoiceList);

            return View(invoiceVM);
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
                var invoice = _mapper.Map<InvoiceVM>(invoiceDetailsVM);

                invoiceDetailsVM = invoice;

                return View(invoiceDetailsVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(InvoiceVM invoiceDetailsVM, IFormFile file) 
        {
            if (ModelState.IsValid) 
            {
                var autovehiculeList = _unitOfWork.Autovehicule.GetAll().ToList();
                var model = _mapper.Map<Invoice>(invoiceDetailsVM);
                model.Autovehicule.AddRange(autovehiculeList);
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                string fileName = model.InvoiceNumber + Path.GetExtension(file.FileName);

                model.TotalPrice = 0;
                //foreach (var item in model.Autovehicule) 
                //{
                //    item.PriceDKV = model.Price;
                //}

                    if (file != null && Path.GetExtension(file.FileName) == ".pdf")
                    {
                        string invoicePdfPath = Path.Combine(wwwRootPath, @"invoice");

                        if (!string.IsNullOrEmpty(model.InvoiceUrl))
                        {
                            //Delete old file
                            var oldimgPath = Path.Combine(wwwRootPath, model.InvoiceUrl.TrimStart('\\'));

                            if (System.IO.File.Exists(oldimgPath))
                            {
                                System.IO.File.Delete(oldimgPath);
                            }
                        }

                        //Create File Img
                        using (var fileStream = new FileStream(Path.Combine(invoicePdfPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        model.InvoiceUrl = @"\invoice\" + fileName;
                    }
                   
                    else
                    {
                        TempData["error"] = "Incorect File Format, File can only be PDF and Image can only be PNG or JPG";
                        return RedirectToAction("Index", "Invoice");
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

                return RedirectToAction("Index", "Autovehicule");
            }
            else
            {
                var licensePlate = _unitOfWork.Autovehicule.GetAll().ToList();
                var licensePLateVM = _mapper.Map<List<AutovehiculeVM>>(licensePlate);

                invoiceDetailsVM.Autovehicule= licensePLateVM;
                return View(invoiceDetailsVM);
            }
        }


    }
}

