using AutoMapper;
using Dapper;
using Soriana.PPS.Common.Data;
using Soriana.PPS.Common.DTO.Filters;
using Soriana.PPS.Common.DTO.PaymentProcessor;
using Soriana.PPS.Common.Entities;
using Soriana.PPS.Common.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soriana.PPS.DataAccess.Repository
{
    public sealed class ItemRepository : RepositoryBase, IItemRepository
    {
        #region Private Fields
        private readonly IRepositoryRead<Item> _RepositoryRead;
        private readonly IRepositoryCreate<Item> _RepositoryCreate;
        #endregion
        #region Constructors
        public ItemRepository(IRepositoryCreate<Item> repositoryCreate,
                                IRepositoryRead<Item> repositoryRead,
                                IUnitOfWork unitOfWork,
                                IMapper mapper) : base(unitOfWork: unitOfWork, mapper: mapper)
        {
            _RepositoryRead = repositoryRead;
            _RepositoryCreate = repositoryCreate;
        }
        #endregion
        #region Public Methods
        #region RepositoryRead
        public async Task<Item> GetByAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.GetByAsync(searchFilter);
        }

        public async Task<IList<Item>> GetListAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.GetListAsync(searchFilter);
        }

        public async Task<IList<Item>> GetListPagedAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.GetListPagedAsync(searchFilter);
        }

        public async Task<int> RecordCountAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.RecordCountAsync(searchFilter);
        }

        public async Task<IList<ItemResult>> GetGroceryItems()
        {
            IEnumerable<Item> items = await UnitOfWork.Connection.GetListAsync<Item>(new { ItemType = MerchandiseTypeEnum.GROCERY, IsActive = true }, null, null);
            IList<ItemResult> itemResults = new List<ItemResult>();
            foreach (Item item in items)
                itemResults.Add(new ItemResult() { Barcode = item.BarCode });
            return itemResults;
        }

        public async Task<IList<ItemResult>> GetNonGroceryItems()
        {
            IEnumerable<Item> items = await UnitOfWork.Connection.GetListAsync<Item>(new { ItemType = MerchandiseTypeEnum.NONGROCERY, IsActive = true }, null, null);
            IList<ItemResult> itemResults = _Mapper.Map<IList<ItemResult>>(items);
            return itemResults;
        }
        #endregion
        #region RepositoryWrite
        public async Task<long?> InsertAsync(Item entity)
        {
            return await _RepositoryCreate.InsertAsync(entity);
        }

        public async Task<IList<long?>> InsertListAsync(IList<Item> listEntities)
        {
            return await _RepositoryCreate.InsertListAsync(listEntities);
        }
        #endregion
        #endregion
    }
}
