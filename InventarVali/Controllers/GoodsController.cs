using InventarVali.Data;
using InventarVali.Models;
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

    }
}
