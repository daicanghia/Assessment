using System;
using Volo.Abp.DependencyInjection;

namespace ProductManagement
{
    public class ProductManagementTestData : ISingletonDependency
    {
        public string ProductCode1 { get; } = "ProductCode1";

        public string ProductName1 { get; } = "ProductName1";

        public decimal ProductPrice1 { get; } = 20;

        public int ProductStockCount1 { get; } = 100;

        public string ProductCode2 { get; } = "ProductCode2";

        public string ProductName2 { get; } = "ProductName2";

        public decimal ProductPrice2 { get; } = 30;

        public int ProductStockCount2 { get; } = 110;

        public string ProductBranch1 { get; } = "VN";

        public string ProductColour1 { get; } = "red";
        public string ProductBranch2 { get; } = "US";

        public string ProductColour2 { get; } = "white";
    }
}
