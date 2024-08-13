using InventarVali.DataAccess.Data;
using InventarVali.DataAccess.Repository.IRepository;
using InventarVali.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventarVali.Controllers
{
    public class GoodsController : Controller
    {
        private readonly IGoodsRepository _goodsRepo;
        public GoodsController(IGoodsRepository goodsRepo)
        {
            _goodsRepo = goodsRepo;
        }
        public IActionResult Index()
        {
            List<Goods> objGoodsList = _goodsRepo.GetAll().ToList();
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
                _goodsRepo.Add(obj);
                _goodsRepo.Save();
                TempData["success"] = "Goods created successfully";

                return RedirectToAction("Index","Goods");
            }
            else 
            {
                return View();
            }
        }
        public IActionResult Edit(int id) 
        {
            if(id == null || id == 0) 
            {
                return NotFound(); 
            }
            Goods? obj = _goodsRepo.Get(o => o.Id == id);

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
                _goodsRepo.Update(obj);
                _goodsRepo.Save();
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

            Goods obj = _goodsRepo.Get(o => o.Id == id);

            if (obj == null) 
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteGoods(int?id) 
        {
            Goods obj = _goodsRepo.Get(o => o.Id == id);
            if (obj == null) 
            {
                return NotFound();
            }

            _goodsRepo.Remove(obj);
            _goodsRepo.Save();
            TempData["success"] = "Goods deleted successfully";

            return RedirectToAction("Index", "Goods");
        }
    }
}
