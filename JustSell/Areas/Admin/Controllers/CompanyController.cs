using JustSell.Models;
using JustSell.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace JustSell.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CompanyController : Controller
	{
		public readonly IUnitOfWork _unitOfWork;

		public CompanyController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Upsert(int? id)
		{
			Company company = new();

			if (id == null || id == 0) 
			{
				return View(company);
			}

			company = _unitOfWork.Company.GetFirstOrDefault(x => x.Id == id, includeProperties:"Company");

			return View(company);
			
			
		    
			
		}
	}
}
