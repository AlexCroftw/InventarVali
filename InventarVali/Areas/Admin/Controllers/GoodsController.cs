using InventarVali.DataAccess.Data;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventarVali.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GoodsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public GoodsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Goods> objGoodsList = _unitOfWork.Goods.GetAll().ToList();
            return View(objGoodsList);
        }
        public IActionResult CreateGoods()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateGoods(Goods obj)
        {
            if (obj.Name == obj.Type)
            {
                ModelState.AddModelError("name", "Name cannot be the same as Type");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Goods.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Goods created successfully";

                return RedirectToAction("Index", "Goods");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Goods? obj = _unitOfWork.Goods.Get(o => o.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Goods obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Goods.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Goods updated successfully";
            }
            return RedirectToAction("Index", "Goods");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Goods obj = _unitOfWork.Goods.Get(o => o.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteGoods(int? id)
        {
            Goods obj = _unitOfWork.Goods.Get(o => o.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Goods.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Goods deleted successfully";

            return RedirectToAction("Index", "Goods");
        }
    }
}
