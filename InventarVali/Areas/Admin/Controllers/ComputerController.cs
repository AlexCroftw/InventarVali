using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventarVali.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ComputerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ComputerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Computer> obj = _unitOfWork.Computer.GetAll().ToList();
            return View(obj);
        }

        public IActionResult CreateComputer() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateComouter(Computer obj) 
        {
            if (obj == null) 
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Computer.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Computer was added successfully";
                return RedirectToAction("Index","Computer");
            }
            
            return View();
            
        }

        public IActionResult Edit(int id) 
        {
            if (id == 0 || id == null) 
            {
                return NotFound();
            }
            Computer obj = _unitOfWork.Computer.Get(o => o.Id == id);
            if (obj == null) 
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Computer obj) 
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Computer.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "The Computer was updated successfully";
                return RedirectToAction("Index","Computer");
            }
            return View();
        }

        public IActionResult Delete(int id) 
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Computer obj = _unitOfWork.Computer.Get(o => o.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteComputer(int id) 
        {
            Computer obj = _unitOfWork.Computer.Get(o => o.Id == id);
            if (id == 0 || id == null) 
            {
                return NotFound();
            }
            if (ModelState.IsValid) 
            {
                _unitOfWork.Computer.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "The Computer was deleted successfully";
                return RedirectToAction("Index", "Computer");
            }
            return View();
        }
    }
}
