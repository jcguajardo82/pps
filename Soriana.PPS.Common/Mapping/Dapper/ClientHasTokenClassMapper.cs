using DapperExtensions.Mapper;
using Soriana.PPS.Common.Entities;

namespace Soriana.PPS.Common.Mapping.Dapper
{
    public sealed class ClientHasTokenClassMapper : ClassMapper<ClientHasToken>
    {
        #region Constructors
        public ClientHasTokenClassMapper()
        {
            Map(cht => cht.ClientID).Key(KeyType.Assigned);
            Map(cht => cht.ClientToken).Key(KeyType.Assigned);
            AutoMap();
        }
        #endregion
    }
}
