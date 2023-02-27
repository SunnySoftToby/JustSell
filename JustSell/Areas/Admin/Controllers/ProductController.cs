using JustSell.Models.ViewModels;
using JustSell.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JustSell.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _hostEnvironment;

		public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_hostEnvironment = hostEnvironment;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Upsert(int? id)
		{
			ProductVM productVM = new()
			{
				Product = new(),
				CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
				{
					Text = x.Name,
					Value = x.Id.ToString()
				})
			};

			if (id == null || id == 0)
			{
				return View(productVM);
			}
			else
			{
				productVM.Product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
				return View(productVM);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Upsert(ProductVM productVM, IFormFile? file)
		{
			if (ModelState.IsValid)
			{
				string wwwRootPath = _hostEnvironment.WebRootPath;
				if (file != null)
				{
					string fileName = Guid.NewGuid().ToString();
					var uploads = Path.Combine(wwwRootPath, @"images\products");
					var extension = Path.GetExtension(file.FileName);

					if (productVM.Product.ImageUrl != null)
					{
						var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
						if (System.IO.File.Exists(oldImagePath))
						{
							System.IO.File.Delete(oldImagePath);
						}
					}
					using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
					{
						file.CopyTo(fileStreams);
					}
					productVM.Product.ImageUrl = @"\images\products\" + fileName + extension;
				}
				if (productVM.Product.Id == 0)
				{
					_unitOfWork.Product.Add(productVM.Product);
				}
				else
				{
					_unitOfWork.Product.Update(productVM.Product);
				}
				_unitOfWork.Save();
				TempData["success"] = "新增產品成功";
				return RedirectToAction("Index");
			}
			return View(productVM);
		}

		[HttpDelete]
		public IActionResult Delete(int? id)
		{
			var productFromDb = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);

			if (productFromDb == null)
			{
				return Json(new { success = false, message = "刪除失敗" });
			}

			var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, productFromDb.ImageUrl.TrimStart('\\'));
			if (System.IO.File.Exists(oldImagePath))
			{
				System.IO.File.Delete(oldImagePath);
			}
			_unitOfWork.Product.Remove(productFromDb);
			_unitOfWork.Save();
			return Json(new { success = true, message = "刪除成功" });
		}


		#region API calls
		[HttpGet]
		public IActionResult GetAll()
		{
			var productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
			return Json(new { data = productList });
		}

		#endregion
	}
}
