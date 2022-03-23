using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace ProductManagement.EntityFrameworkCore
{
    [ConnectionStringName("ProductManagement")]
    public class ProductManagementDbContext : AbpDbContext<ProductManagementDbContext>, IProductManagementDbContext
    {
        public static string TablePrefix { get; set; } = ProductManagementConsts.DefaultDbTablePrefix;

        public static string Schema { get; set; } = ProductManagementConsts.DefaultDbSchema;

        public DbSet<Product> Products { get; set; }
        public DbSet<HistoryAction> HistoryActions { get; set; }

        public ProductManagementDbContext(DbContextOptions<ProductManagementDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureProductManagement(options =>
            {
                options.TablePrefix = TablePrefix;
                options.Schema = Schema;
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.LogTo(item =>
    {
        Debug.WriteLine(item);
    });
    }
}