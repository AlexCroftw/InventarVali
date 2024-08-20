using InventarVali.DataAccess.Repository;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
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

        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateEmployee(Employees obj)
        {
            if (obj.FirstName == obj.LastName)
            {
                ModelState.AddModelError("error", "First name cannot be the same as Last name");
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Employee.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "The employee was created successfully";
                return RedirectToAction("Index", "Employee");
            }
            else 
            {
                return View();
            }
            
        }

        public IActionResult Edit(int id)
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

        [HttpPost]

        public IActionResult Edit(Employees obj) 
        {
            if (!ModelState.IsValid) 
            {
                _unitOfWork.Employee.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "The employee was updated successfully";
                return RedirectToAction("Index", "Employee");
            }
            return View();
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
    }
}
