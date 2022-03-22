using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace ProductManagement
{
    public class Product : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// A unique value for this product.
        /// ProductManager ensures the uniqueness of it.
        /// It can not be changed after creation of the product.
        /// </summary>
        [NotNull]
        public string Code { get; private set; }

        [NotNull]
        public string Name { get; private set; }

        public decimal Price { get; private set; }
        public string Branch { get; set; }
        public string Colour { get; set; }

        public int StockCount { get; private set; }

        public string ImageName { get; private set; }

        private Product()
        {
            //Default constructor is needed for ORMs.
        }

        internal Product(
            Guid id,
            [NotNull] string code, 
            [NotNull] string name, 
            decimal price = 0, 
            int stockCount = 0,
            string imageName = null,
            string branch = null,
            string colour = null)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));

            if (code.Length >= ProductConsts.MaxCodeLength)
            {
                throw new ArgumentException($"Product code can not be longer than {ProductConsts.MaxCodeLength}");
            }

            Id = id;
            Code = code;
            SetName(Check.NotNullOrWhiteSpace(name, nameof(name)));
            SetPrice(price);
            SetImageName(imageName);
            SetBranch(branch);
            SetColour(colour);
            SetStockCountInternal(stockCount, triggerEvent: false);
        }

        public Product SetName([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            if (name.Length >= ProductConsts.MaxNameLength)
            {
                throw new ArgumentException($"Product name can not be longer than {ProductConsts.MaxNameLength}");
            }

            Name = name;
            return this;
        }
        public Product SetBranch([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            if (name.Length >= ProductConsts.MaxBranchLength)
            {
                throw new ArgumentException($"Branch length can not be longer than {ProductConsts.MaxBranchLength}");
            }

            this.Branch = name;
            return this;
        }

        public Product SetColour([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            if (name.Length >= ProductConsts.MaxColourLength)
            {
                throw new ArgumentException($"Colour length can not be longer than {ProductConsts.MaxColourLength}");
            }

            this.Colour = name;
            return this;
        }

        public Product SetImageName([CanBeNull] string imageName)
        {
            if (imageName == null)
            {
                return this;
            }

            if (imageName.Length >= ProductConsts.MaxImageNameLength)
            {
                throw new ArgumentException($"Product image name can not be longer than {ProductConsts.MaxImageNameLength}");
            }

            ImageName = imageName;
            return this;
        }

        public Product SetPrice(decimal price)
        {
            if (price < 0)
            {
                throw new ArgumentException($"{nameof(price)} can not be less than 0.0!");
            }

            Price = price;
            return this;
        }

        public Product SetStockCount(int stockCount)
        {
            return SetStockCountInternal(stockCount);
        }

        private Product SetStockCountInternal(int stockCount, bool triggerEvent = true)
        {
            if (StockCount < 0)
            {
                throw new ArgumentException($"{nameof(stockCount)} can not be less than 0!");
            }

            if (StockCount == stockCount)
            {
                return this;
            }

            if (triggerEvent)
            {
                AddDistributedEvent(
                    new ProductStockCountChangedEto(
                        Id,
                        StockCount,
                        stockCount
                    )
                );
            }

            StockCount = stockCount;
            return this;
        }
    }
}
