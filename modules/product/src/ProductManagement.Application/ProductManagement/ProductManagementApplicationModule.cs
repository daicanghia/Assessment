using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace ProductManagement
{
    [DependsOn(
        typeof(ProductManagementDomainModule),
        typeof(ProductManagementApplicationContractsModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpAspNetCoreMvcModule)
        )]
    public class ProductManagementApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<ProductManagementApplicationAutoMapperProfile>(validate: true);
            });
        }
    }
}
