using System;
using Volo.Abp.Application.Dtos;

namespace ProductManagement
{
    public class ProductQueryDto: PagedAndSortedResultRequestDto
    {
        public string Branch { get; set; }

        public string Name { get; set; }

        public decimal? Price { get; set; }

    }
}