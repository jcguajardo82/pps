using Dapper.Contrib.Extensions;
using Soriana.PPS.Common.Constants;

namespace Soriana.PPS.Common.Entities
{
    [Table(DatabaseSchemaConstants.TABLE_NAME_CLIENT_HAS_TOKEN)]
    public class Item : EntityBase
    {
        #region Public Properties
        [ExplicitKey()]
        public long ItemID { get; set; }

        public string BarCode { get; set; }

        public byte ItemType { get; set; }
        #endregion
    }
}
