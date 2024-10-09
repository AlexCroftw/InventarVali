using AutoMapper;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using InventarVali.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InventarVali.Areas.Employee.Controllers
{
    [Area("Employee")]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public HomeController(ILogger<HomeController> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            CombinedDataViewModel combinedData = new CombinedDataViewModel();
            var autovehiculeList = _unitOfWork.Autovehicule.GetAll(includeProperties: "Employees").ToList();
            var computerList = _unitOfWork.Computer.GetAll(includeProperties: "Employees").ToList();  
            combinedData.Autovehicule = autovehiculeList;
            combinedData.Computer = computerList;
           
            return View(combinedData);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult GetAllAutovehicule()
        {
            CombinedDataViewModel combinedData = new CombinedDataViewModel();
            var autovehiculeList = _unitOfWork.Autovehicule.GetAll(includeProperties: "Employees").ToList();
            combinedData.Autovehicule = autovehiculeList;

            return Json(new { data = combinedData.Autovehicule });
        }

        [HttpGet]
        public IActionResult GetAllComputers()
        {
            CombinedDataViewModel combinedData = new CombinedDataViewModel();
            var computerList = _unitOfWork.Computer.GetAll(includeProperties: "Employees").ToList();
            combinedData.Computer = computerList;

            return Json(new { data = combinedData.Computer });
        }
    }

}
