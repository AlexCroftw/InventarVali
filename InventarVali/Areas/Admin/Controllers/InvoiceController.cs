using AutoMapper;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using InventarVali.Models.ViewModel;
using InventarVali.Utility;
using InventarVali.Utility.Services;
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
            List<Invoice> invoiceList = _unitOfWork.Invoice.GetAll(includeProperties:"Autovehicule").ToList();
            var invoiceVM = _mapper.Map<List<InvoiceVM>>(invoiceList);

            return View(invoiceVM);
        }
    }
}
