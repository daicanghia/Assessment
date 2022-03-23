using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;

namespace ProductManagement
{
    //[Authorize(ProductManagementPermissions.Products.Default)]
    public class ProductAppService : ApplicationService, IProductAppService
    {
        private readonly ProductManager _productManager;
        private readonly HistoryActionManager _historyActionManager;
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IRepository<HistoryAction, Guid> _historyActionRepository;
        private readonly ICurrentTenant _currentTenant;

        public ProductAppService(ProductManager productManager, HistoryActionManager historyActionManager, IRepository<Product, Guid> productRepository, IRepository<HistoryAction, Guid> historyActionRepository, ICurrentTenant currentTenant)
        {
            _productManager = productManager;
            _historyActionManager = historyActionManager;
            _productRepository = productRepository;
            _historyActionRepository = historyActionRepository;
            _currentTenant = currentTenant;
        }

        public async Task<PagedResultDto<ProductDto>> GetListPagedAsync(ProductQueryDto input)
        {
            //Add history action
            await _historyActionManager.CreateAsync(_currentTenant?.Id, JsonConvert.SerializeObject(input), Enum.HistoryActionType.Seaching);

            await NormalizeMaxResultCountAsync(input);

            //Queriables
            var queries = await _productRepository.GetQueryableAsync();

            //Genearate predicate
            var predicate = PredicateBuilder.New<Product>(true);
            if (!string.IsNullOrEmpty(input.Name))
            {
                predicate = predicate.And(i => i.Name.ToLower().Contains(input.Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(input.Branch))
            {
                predicate = predicate.And(i => i.Branch.ToLower().Contains(input.Branch.ToLower()));
            }
            if (!string.IsNullOrEmpty(input.Name))
            {
                predicate = predicate.And(i => i.Name.ToLower().Contains(input.Name.ToLower()));
            }
            if (input.Price.HasValue)
            {
                predicate = predicate.And(i => i.Price >= input.Price);
            }

            //Get product base predicate 
            //Sort & pagging
            var products = await queries.Where(predicate).Select(product => product)
                .OrderBy(input.Sorting ?? "Name")
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();

            var totalCount = await _productRepository.GetCountAsync();

            var dtos = ObjectMapper.Map<List<Product>, List<ProductDto>>(products);

            return new PagedResultDto<ProductDto>(totalCount, dtos);
        }

        public async Task<ProductDto> GetAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id);

            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        //[Authorize(ProductManagementPermissions.Products.Create)]
        public async Task<ProductDto> CreateAsync(CreateProductDto input)
        {
            var product = await _productManager.CreateAsync(
                input.Code,
                input.Name,
                input.Branch,
                input.Colour,
                input.Price,
                input.StockCount,
                input.ImageName
            );

            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        //[Authorize(ProductManagementPermissions.Products.Update)]
        public async Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto input)
        {
            var product = await _productRepository.GetAsync(id);

            product.SetName(input.Name);
            product.SetPrice(input.Price);
            product.SetStockCount(input.StockCount);
            product.SetImageName(input.ImageName);

            var updateProduct =  await _productRepository.UpdateAsync(product);

            return ObjectMapper.Map<Product, ProductDto>(updateProduct);
        }

        //[Authorize(ProductManagementPermissions.Products.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }

        private async Task NormalizeMaxResultCountAsync(PagedAndSortedResultRequestDto input)
        {
            var maxPageSize = (await SettingProvider.GetOrNullAsync(ProductManagementSettings.MaxPageSize))?.To<int>();
            if (maxPageSize.HasValue && input.MaxResultCount > maxPageSize.Value)
            {
                input.MaxResultCount = maxPageSize.Value;
            }
        }
    }
}
