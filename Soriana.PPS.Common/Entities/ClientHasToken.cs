using Dapper.Contrib.Extensions;
using Soriana.PPS.Common.Constants;

namespace Soriana.PPS.Common.Entities
{
    [Table(DatabaseSchemaConstants.TABLE_NAME_CLIENT_HAS_TOKEN)]
    public sealed class ClientHasToken : EntityBase
    {
        #region Public Properties
        [ExplicitKey()]
        public long ClientID { get; set; }

        [ExplicitKey()]
        public string ClientToken { get; set; }

        public int BinCode { get; set; }

        public string PaymentMethod { get; set; }

        public string Bank { get; set; }

        public string TypeOfCard { get; set; }

        public bool PersistToken { get; set; }
        #endregion
    }
}
