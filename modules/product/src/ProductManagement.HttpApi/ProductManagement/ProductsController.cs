using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.MultiTenancy;

namespace ProductManagement
{
    [RemoteService]
    [Area("productManagement")]
    [Route("api/productManagement/products")]
    public class ProductsController : AbpController, IProductAppService
    {
        private readonly IProductAppService _productAppService;
        private readonly ICurrentTenant _currentTenant;

        public ProductsController(IProductAppService productAppService, ICurrentTenant currentTenant)
        {
            _productAppService = productAppService;
            _currentTenant = currentTenant;

            //TODO: this is temporary, tenant is the customer login => need to to implement audentication using OAunth 2
            _currentTenant.Change(new Guid("f1c453d2-0727-33a9-1e72-3a01b12bbd29"));
        }

        [HttpGet]
        [Route("")]
        public Task<PagedResultDto<ProductDto>> GetListPagedAsync(ProductQueryDto input)
        {
            return _productAppService.GetListPagedAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<ProductDto> GetAsync(Guid id)
        {
            return _productAppService.GetAsync(id);
        }

        [HttpPost]
        public Task<ProductDto> CreateAsync(CreateProductDto input)
        {
            return _productAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto input)
        {
            return _productAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        public Task DeleteAsync(Guid id)
        {
            return _productAppService.DeleteAsync(id);
        }
    }
}
