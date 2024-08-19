using InventarVali.DataAccess.Repository;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventarVali.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AutovehiculeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AutovehiculeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Autovehicule> objAutovehiculeslist = _unitOfWork.Autovehicule.GetAll().ToList();
            return View(objAutovehiculeslist);
        }

        public IActionResult CreateAutovehicule() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAutovehicule(Autovehicule obj) 
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Autovehicule.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Autovehicule was created successfully";

                return RedirectToAction("Index", "Autovehicule");
            }

            else 
            {
                return View();
            }
        }

        public IActionResult Edit(int id ) 
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Autovehicule obj = _unitOfWork.Autovehicule.Get(x => x.Id == id);
            if (obj == null) 
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Edi(Autovehicule obj) 
        {
            
            if (ModelState.IsValid) 
            {
                _unitOfWork.Autovehicule.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "The Autovehicule was created successfully";

                return RedirectToAction("Index", "Autovehicule");
            }
            return View();
        }

        public IActionResult Delete(int id) 
        {
            if (id == null || id == 0) 
            {
                return NotFound();
            }

            Autovehicule obj = _unitOfWork.Autovehicule.Get(x => x.Id == id);

            if (obj == null) 
            {
                return NotFound();
            }

            return View(obj);
        }
        [HttpPost]
        public IActionResult DeleteVehicule(int id) 
        {
            Autovehicule obj = _unitOfWork.Autovehicule.Get(x => x.Id == id);

            if (id == null || id == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Autovehicule.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "The Autovehicule  was deleted successfully";

                return RedirectToAction("Index", "Autovehicule");
            }
            else
            {
                return View();
            }
        }
    }
}
