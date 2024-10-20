﻿using AutoMapper;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using InventarVali.Models.ViewModel;
using InventarVali.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;


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
            List<Invoice> invoiceList = _unitOfWork.Invoice.GetAll(includeProperties: "AutovehiculeInvoice").ToList();
            var invoiceVM = _mapper.Map<List<InvoiceVM>>(invoiceList);

            List<Autovehicule> autovehiculesList = _unitOfWork.Autovehicule.GetAll().ToList();
            var autovehiculeList = _mapper.Map<List<AutovehiculeVM>>(autovehiculesList);

            foreach (var invoice in invoiceVM)
            {
                invoice.Autovehicule = autovehiculeList;
            }

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
                var invoice = _unitOfWork.Invoice.Get(x => x.Id == id, includeProperties: "AutovehiculeInvoice");
                invoiceDetailsVM = _mapper.Map<InvoiceVM>(invoice);
                invoiceDetailsVM.Autovehicule = licensePLateVM;

                return View(invoiceDetailsVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(InvoiceVM invoiceDetailsVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                var model = new Invoice();

                if (invoiceDetailsVM.Id != 0) 
                {
                    model = _unitOfWork.Invoice.Get(x => x.Id == invoiceDetailsVM.Id, includeProperties: "AutovehiculeInvoice");
                }

                model = _mapper.Map(invoiceDetailsVM, model);

                string wwwRootPath = _webHostEnvironment.WebRootPath;

                model.TotalPrice = invoiceDetailsVM.AutovehiculeInvoice.Sum(x => x.PriceFuel);


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
                var licensePLateVM = _mapper.Map<List<AutovehiculeInvoiceVM>>(licensePlate);

                invoiceDetailsVM.AutovehiculeInvoice = licensePLateVM;
                return View(invoiceDetailsVM);
            }
        }

        #region VALIDATORS
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyInvoiceNumber(InvoiceVM invoiceVM)
        {

            var model = _unitOfWork.Invoice.Get(x => x.InvoiceNumber == invoiceVM.InvoiceNumber && x.Id != invoiceVM.Id);
            if (model != null)
            {
                return Json($" The Invoice number : {invoiceVM.InvoiceNumber} is already in the DataBase");
            }

            return Json(true);
        }
        #endregion

        #region APICALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Invoice> invoiceList = _unitOfWork.Invoice.GetAll(includeProperties: "AutovehiculeInvoice").ToList();
            var invoiceVM = _mapper.Map<List<InvoiceVM>>(invoiceList);

            List<Autovehicule> autovehiculesList = _unitOfWork.Autovehicule.GetAll().ToList();
            var autovehiculeList = _mapper.Map<List<AutovehiculeVM>>(autovehiculesList);

            foreach (var invoice in invoiceVM) 
            {
                invoice.Autovehicule = autovehiculeList;
            }

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

