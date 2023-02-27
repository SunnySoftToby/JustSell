
using JustSell.DataAccess;
using JustSell.Models;
using JustSell.Repository.IRepository;

namespace JustSell.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private AppDbContext _db;
        public ProductRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Product obj)
        {
            var productFromDb = _db.Products.FirstOrDefault(x => x.Id == obj.Id);
            if (productFromDb != null) 
            {
                productFromDb.Name = obj.Name;
                productFromDb.Description = obj.Description;    
                productFromDb.Price = obj.Price;
                productFromDb.DiscountPrice= obj.DiscountPrice;
                productFromDb.CategoryId= obj.CategoryId;
                if (obj.ImageUrl != null)
                {
                    productFromDb.ImageUrl = obj.ImageUrl;

                }
            }
        }
    }
}
