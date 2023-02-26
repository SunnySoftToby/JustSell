using Microsoft.AspNetCore.Mvc;
using JustSell.data;
using JustSell.Models;

namespace JustSell.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;

        public CategoryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {

            return View();
        }


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Category obj)
		{
			if (ModelState.IsValid)
			{
				_db.Categories.Add(obj);
                _db.SaveChanges();
				TempData["success"] = "創建成功";
				return RedirectToAction("Index");
			}
			return View(obj);

		}
	}
}
