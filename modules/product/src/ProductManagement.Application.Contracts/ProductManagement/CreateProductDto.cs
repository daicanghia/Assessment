using System.ComponentModel.DataAnnotations;

namespace ProductManagement
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(ProductConsts.MaxCodeLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(ProductConsts.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(ProductConsts.MaxBranchLength)]
        public string Branch { get; set; }

        [Required]
        [StringLength(ProductConsts.MaxColourLength)]
        public string Colour { get; set; }

        [StringLength(ProductConsts.MaxImageNameLength)]
        public string ImageName { get; set; }

        public decimal Price { get; set; }

        public int StockCount { get; set; }
    }
}