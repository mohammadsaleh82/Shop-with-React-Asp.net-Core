using Core.DTOs.BasketDTO;
using DataLayer.Entites;

namespace Core.Services.Interfaces
{
    public interface IBasketService
    {
        Task AddItem(Product product, int Quantity, int BasketId);

        Task Remove(int BasketId,int Quantity,int Productid);

        Task<BasketViewModel> GetBasketByBuyerId(string BuyerId);

        Task<BasketViewModel> CreateBasket();
    }
}
