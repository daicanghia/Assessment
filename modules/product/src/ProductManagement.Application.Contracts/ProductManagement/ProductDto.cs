using System;
using Volo.Abp.Application.Dtos;

namespace ProductManagement
{
    public class ProductDto : AuditedEntityDto<Guid>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string ImageName { get; set; }

        public decimal Price { get; set; }

        public int StockCount { get; set; }
        public string Branch { get; set; }
        public string Colour { get; set; }
    }
}