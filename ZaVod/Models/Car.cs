using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaVod.Models
{
    class Car
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ProductionShopId { get; set; }
        public int ModelId { get; set; }
        public int StatusId { get; set; }
        public int Price { get; set; }
    }
}
