using AutoMapper;
using InventarVali.DataAccess.Data;
using InventarVali.DataAccess.Repository;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using InventarVali.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InventarVali.Areas.Employee.Controllers
{
    [Area("Employee")]

    ///TODO 
    ///Add a file export button/script using JS
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
            List<CombinedDataViewModel> combinedData = new List<CombinedDataViewModel>();
            var autovehiculeList = _unitOfWork.Autovehicule.GetAll(includeProperties: "Employees").ToList();
            var computerList = _unitOfWork.Computer.GetAll(includeProperties: "Employees").ToList();
            var combinedDataAuatovehicule = _mapper.Map<List<CombinedDataViewModel>>(autovehiculeList);
            var combinedDataComputer = _mapper.Map <List<CombinedDataViewModel>>(computerList);
            combinedData.AddRange(combinedDataAuatovehicule);
            combinedData.AddRange(combinedDataComputer);

            //Test Email Sending



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
        public IActionResult GetAll()
        {
            List<CombinedDataViewModel> combinedData = new List<CombinedDataViewModel>();
            var autovehiculeList = _unitOfWork.Autovehicule.GetAll(includeProperties: "Employees").ToList();
            var computerList = _unitOfWork.Computer.GetAll(includeProperties: "Employees").ToList();
            var combinedDataAuatovehicule = _mapper.Map<List<CombinedDataViewModel>>(autovehiculeList);
            var combinedDataComputer = _mapper.Map<List<CombinedDataViewModel>>(computerList);
            combinedData.AddRange(combinedDataAuatovehicule);
            combinedData.AddRange(combinedDataComputer);

            return Json(new { data = combinedData });
        }
    }

}
