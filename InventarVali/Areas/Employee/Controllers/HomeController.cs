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

            combinedData.Autovehicule = _mapper.Map<List<AutovehiculeVM>>(autovehiculeList);
            combinedData.Computer = _mapper.Map<List<ComputerVM>>(computerList);

            return View(combinedData);
        }


        public IActionResult DetailsAutovehicule(AutovehiculeVM autovehicleVM, int id)
        {
            var autovehicule = _unitOfWork.Autovehicule.Get(x => x.Id == id, includeProperties: "Employees");
            autovehicleVM = _mapper.Map<AutovehiculeVM>(autovehicule);

            return View(autovehicleVM);
        }
        public IActionResult DetailsComputer(ComputerVM computerVM, int id)
        {
            var computer = _unitOfWork.Computer.Get(x => x.Id == id, includeProperties: "Employees");
            computerVM = _mapper.Map<ComputerVM>(computer);

            return View(computerVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        #region APICALLS

        [HttpGet]
        public IActionResult GetAllAutovehicule()
        {
            CombinedDataViewModel combinedData = new CombinedDataViewModel();
            var autovehiculeList = _unitOfWork.Autovehicule.GetAll(includeProperties: "Employees").ToList();
            combinedData.Autovehicule = _mapper.Map<List<AutovehiculeVM>>(autovehiculeList);

            return Json(new { data = combinedData.Autovehicule });
        }




        [HttpGet]
        public IActionResult GetAllComputers()
        {
            CombinedDataViewModel combinedData = new CombinedDataViewModel();
            var computerList = _unitOfWork.Computer.GetAll(includeProperties: "Employees").ToList();
            combinedData.Computer = _mapper.Map<List<ComputerVM>>(computerList);

            return Json(new { data = combinedData.Computer });
        }
    }
    #endregion
}
