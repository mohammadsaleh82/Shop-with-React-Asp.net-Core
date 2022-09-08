using Core.DTOs.BasketDTO;
using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entites;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class BasketService: IBasketService
    {
       private ShopContext _context;
        public BasketService(ShopContext context)
        {
            _context = context;
        }

        public async Task AddItem(Product product, int Quantity, int BasketId)
        {
           var Basket=await _context.Baskets.Include(i => i.Items)
                .ThenInclude(p => p.Product).SingleOrDefaultAsync(b=>b.Id==BasketId);
            if (Basket == null)return;
            if (Basket.Items.All(item => item.Product.Id != product.Id))
            {
                Basket.Items.Add(new BasketItem { ProductId = product.Id, Quantity = Quantity });
            }

            var exisItem=Basket.Items.FirstOrDefault(item => item.ProductId == product.Id);
            if (exisItem != null) exisItem.Quantity += Quantity;

            _context.Baskets.Update(Basket);
            _context.SaveChanges();
            
        }

        public async Task<BasketViewModel> CreateBasket()
        {
            var basket = new Basket()
            {
                BuyerId = Guid.NewGuid().ToString()
            };
            await _context.Baskets.AddAsync(basket);
            _context.SaveChanges();
            return new BasketViewModel
            {
                Id=basket.Id,
                BuyerId=basket.BuyerId
            };
        }

        public async Task<BasketViewModel> GetBasketByBuyerId(string BuyerId)
        {
            var basket = await _context.Baskets.Include(i => i.Items)
                .ThenInclude(p => p.Product).Include(p=>p.Items)
                .SingleOrDefaultAsync(b => b.BuyerId == BuyerId);

            if (basket == null) return null;

            return new BasketViewModel
            {
                BuyerId = basket.BuyerId,
                Id = basket.Id,
                Items = basket.Items.Select(p => new BasketItemViewModel
                {
                    ProductId=p.ProductId,
                    Name=p.Product.Name,
                    Brand=p.Product.Brand,
                    PictureUrl=p.Product.PictureUrl,
                    Price=p.Product.Price,
                    Quantity=p.Quantity,
                    Type=p.Product.Type
                }).ToList()
            };

        }

        public async Task Remove(int BasketId, int Quantity, int Productid)
        {
            var Basket = await _context.Baskets.FindAsync(BasketId);
            if (Basket == null) return;
            var item=Basket.Items.FirstOrDefault(item=>item.ProductId==Productid);
            if (item == null) return;
            item.Quantity -= Quantity;
            if(item.Quantity == 0) Basket.Items.Remove(item);
            _context.Baskets.Update(Basket);
            await _context.SaveChangesAsync();
        }
    }
}
