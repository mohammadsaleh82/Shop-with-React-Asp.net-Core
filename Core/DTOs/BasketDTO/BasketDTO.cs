using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.BasketDTO
{
    public class BasketViewModel
    {
        public int Id { get; set; }

        public string BuyerId { get; set; }

        public List<BasketItemViewModel> Items { get; set; }
    }
    public class BasketItemViewModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public long Price { get; set; }
        public string PictureUrl { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }

        public int Quantity { get; set; }
    }
}
