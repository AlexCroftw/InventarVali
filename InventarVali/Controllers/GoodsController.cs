using InventarVali.Data;
using InventarVali.Models;
using InventarVali.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace InventarVali.Controllers
{
    public class GoodsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public GoodsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Goods> objGoodsList = _db.Goods.ToList();
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
                _db.Goods.Add(obj);
                _db.SaveChanges();
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
            Goods? obj = _db.Goods.FirstOrDefault(x => x.Id == id);

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
                _db.Goods.Update(obj);
                _db.SaveChanges();
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

            Goods obj = _db.Goods.FirstOrDefault(x => x.Id == id);

            if (obj == null) 
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteGoods(int?id) 
        {
            Goods obj = _db.Goods.FirstOrDefault(x => x.Id == id);
            if (obj == null) 
            {
                return NotFound();
            }

            _db.Goods.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Goods deleted successfully";

            return RedirectToAction("Index", "Goods");
        }
    }
}
