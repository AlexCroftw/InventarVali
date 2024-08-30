using InventarVali.DataAccess.Repository;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using InventarVali.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;


namespace InventarVali.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public IActionResult Delete(int id) 
        {
                if (id == 0 || id == null)
                {
                    return NotFound();
                }
                Employees obj = _unitOfWork.Employee.Get(o => o.Id == id);
                if (obj == null)
                {
                    return NotFound();
                }
                return View(obj);
        }

        [HttpPost,ActionName("Delete")]

        public IActionResult DeleteEmployee(int id) 
        {
            Employees obj = _unitOfWork.Employee.Get(o => o.Id == id);

            if (id == 0 || id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Employee.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "This employee was deleted successfully";
                return RedirectToAction("Index", "Employee");
            }
            return View();
     
        }

        #region API CALLS
        [HttpGet]
        public IActionResult Getall()
        {
            List<Employees> objEmployeeList = _unitOfWork.Employee.GetAll().ToList();
            return Json(new { data = objEmployeeList });
        }
        #endregion
    }
}
