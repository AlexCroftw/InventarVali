using AutoMapper;
using InventarVali.DataAccess.Repository;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using InventarVali.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;


namespace InventarVali.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            return View(employeesList);
        }
        
        public IActionResult Upsert(int? id)
        {
            EmployeesVM employeesVM = new()
            {
                Employees = new Employees()
            };


            if (id == 0 || id == null)
            {
                //Create
                return View(employeesVM);
            }
            else
            {
                //Update

                employeesVM.Employees = _unitOfWork.Employee.Get(o => o.Id == id);
                return View(employeesVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert (EmployeesVM employeeVM, int? id)
        {

            if (employeeVM.Employees.FirstName == employeeVM.Employees.LastName)
            {
                ModelState.AddModelError("error", "First name cannot be the same as Last name");
                return BadRequest();
            }
            if (employeeVM.Employees.Email ==  null)
            {
                employeeVM.Employees.Email = "n/a";
            }

            if (ModelState.IsValid)
            {
                if (employeeVM.Employees.Id == 0) 
                {
                     _unitOfWork.Employee.Add(employeeVM.Employees);
                }
                else
                {
                    _unitOfWork.Employee.Update(employeeVM.Employees);
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

            return Json(new { success = true, message = "Deleted Successfully" });
        }

        #endregion
    }
}
