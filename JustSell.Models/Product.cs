using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JustSell.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[DisplayName("商品種類")]
		public int CategoryId { get; set; }

		[ValidateNever]
		[ForeignKey("CategoryId")]
		public Category Category { get; set; }


		[Required]
		public string Name { get; set; }
		public string Description { get; set; }

		[Required]
		[Range(1, 10000)]
		[DisplayName("建議售價")]
		public int Price { get; set; }

		[Range(1, 10000)]
		[DisplayName("特價")]
		public int? DiscountPrice { get; set; }

		[ValidateNever]
		public string? ImageUrl { get; set; }
	}
}
