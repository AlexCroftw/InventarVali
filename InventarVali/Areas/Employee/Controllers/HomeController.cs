using AutoMapper;
using InventarVali.DataAccess.Data;
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
            List<Autovehicule> autovehiculeList = _unitOfWork.Autovehicule.GetAll(includeProperties: "Employess").ToList();
            var autovehiculeVM = _mapper.Map<List<AutovehiculeVM>>(autovehiculeList);
            List<Computer> computerList = _unitOfWork.Computer.GetAll(includeProperties: "Employess").ToList();
            var computerVM = _mapper.Map<List<ComputerVM>>(computerList);

            combinedData.Autovehicule = _mapper.Map<List<ComputerDetailsVM>>(autovehiculeVM);

            return View(); 
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
    }
}
