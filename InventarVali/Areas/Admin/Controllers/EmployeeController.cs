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
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            List<Employees> employeesList = _unitOfWork.Employee.GetAll().ToList();
            var employeesVM = _mapper.Map<List<EmployeesVM>>(employeesList);
            return View(employeesVM);
        }

        public IActionResult Upsert(int? id)
        {

            EmployeesVM employeesVM = new();

            if (id == 0 || id == null)
            {
                //Create
                return View(employeesVM);
            }
            else
            {
                //Update

                var employee = _unitOfWork.Employee.Get(o => o.Id == id);
                employeesVM = _mapper.Map<EmployeesVM>(employee);
                return View(employeesVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(EmployeesVM employeeVM, int? id)
        {
            var employee = _mapper.Map<Employees>(employeeVM);
           
            if (employee.Email == null)
            {
                employee.Email = "n/a";
            }

            employee.FullName = employee.FirstName + " " + employee.LastName;

            if (ModelState.IsValid)
            {
                if (employee.Id == 0)
                {
                    _unitOfWork.Employee.Add(employee);
                }
                else
                {
                    _unitOfWork.Employee.Update(employee);
                }
                _unitOfWork.Save();
                TempData["Success"] = "The employee was created successfully";
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                return View();
            }

        }

        #region VALIDATORS
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyEmail( string email,int id)
        {
            var employee = _unitOfWork.Employee.Get(x => x.Email == email && x.Id != id);
            if (employee != null) 
            {
                return Json($"This {email} is already taken");
            }

            return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyName (string firstName,string lastName,int id)
        {

            string fullName = firstName + " " + lastName; 
            var employeeModel = _unitOfWork.Employee.Get(x => x.FullName == fullName && x.Id !=id);
            if (employeeModel != null) 
            {
                return Json($"This name {fullName} is already taken ");
            }
            if (firstName == lastName)
            {
                return Json($" First Name cannot be the same as Last Name");
            }

            return Json(true);
        }
        #endregion

        #region API CALLS
        [HttpGet]
        public IActionResult Getall()
        {
            List<Employees> objEmployeeList = _unitOfWork.Employee.GetAll().ToList();
            var employees = _mapper.Map<List<EmployeesVM>>(objEmployeeList);
            return Json(new { data = employees });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var employeeToBeDeleted = _unitOfWork.Employee.Get(o => o.Id == id);
            if (employeeToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Employee.Remove(employeeToBeDeleted);
            _unitOfWork.Save();
            TempData["success"] = "Employee was deleted successfully";

            return Json(new { success = true, message = "Deleted Successfully" });
        }

        #endregion
    }
}
