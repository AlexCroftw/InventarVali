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
                return RedirectToAction("Index","Goods");
            }
            else 
            {
                return View();
            }
        }
    }
}
