using System;
using JetBrains.Annotations;
using ProductManagement.Enum;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ProductManagement
{
    public class HistoryAction : AuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        [NotNull]
        public string Name { get; set; }
        public HistoryActionType Action { get; set; }

        public HistoryAction()
        {
            //Default constructor is needed for ORMs.
        }

        public HistoryAction(
            Guid id,
            Guid? customerId,
            HistoryActionType action,
            [NotNull] string name)
        {

            this.Id = id;
            this.TenantId = customerId;
            this.Action = action;
            SetName(Check.NotNullOrWhiteSpace(name, nameof(name)));
        }

        public HistoryAction SetName([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            if (name.Length >= ProductConsts.MaxNameLength)
            {
                throw new ArgumentException($"History name can not be longer than {ProductConsts.MaxNameLength}");
            }

            Name = name;
            return this;
        }
    }
}
