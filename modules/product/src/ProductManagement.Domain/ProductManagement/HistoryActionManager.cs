using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Enum;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace ProductManagement
{
    public class HistoryActionManager : DomainService
    {
        private readonly IRepository<HistoryAction, Guid> _historyActionRepository;

        public HistoryActionManager(IRepository<HistoryAction, Guid> historyActionRepository)
        {
            _historyActionRepository = historyActionRepository;
        }

        public async Task<HistoryAction> CreateAsync(
            Guid? customerId,
            [NotNull] string description,
            HistoryActionType action = HistoryActionType.Seaching)
        {

            return await _historyActionRepository.InsertAsync(
                new HistoryAction(
                    GuidGenerator.Create(),
                    customerId,
                    action,
                    description
                )
            );
        }
    }
}
