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
            var invoiceVM = _mapper.Map<List<InvoiceVM>>(invoiceList);

            return View(invoiceVM);
        }

        public IActionResult Upsert(int? id) 
        {

            InvoiceDetailsVM invoiceDetailsVM = new()
            {
                AutovehiculeList = _unitOfWork.Autovehicule.GetAll().Select(u => new SelectListItem
                {
                    Text = u.LicensePlate,
                    Value = u.Id.ToString()
                }),
                Invoice = new InvoiceVM()
            };


            if (id == null || id == 0)
            {
                //Create
                return View(invoiceDetailsVM);
            }
            else
            {
                var invoice = _mapper.Map<InvoiceVM>(invoiceDetailsVM);

                invoiceDetailsVM.Invoice = invoice;

                return View(invoiceDetailsVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(InvoiceDetailsVM invoiceDetailsVM, IFormFile file) 
        {
            if (ModelState.IsValid) 
            {
                var model = _mapper.Map<Invoice>(invoiceDetailsVM.Invoice);
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                string fileName = model.InvoiceNumber + Path.GetExtension(file.FileName);

                    if (file != null && Path.GetExtension(file.FileName) != ".pdf")
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
                        return RedirectToAction("Index", "Autovehicule");
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
                TempData["success"] = "Autovehicule was created successfully";

                return RedirectToAction("Index", "Autovehicule");
            }
            else
            {
                invoiceDetailsVM.AutovehiculeList = _unitOfWork.Autovehicule.GetAll().Select(u => new SelectListItem
                {
                    Text = u.LicensePlate,
                    Value = u.Id.ToString()
                });
                return View(invoiceDetailsVM);
            }
        }


    }
}

