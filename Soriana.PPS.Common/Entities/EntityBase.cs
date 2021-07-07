using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Mapping;
using System;

namespace Soriana.PPS.Common.Entities
{
    public class EntityBase
    {
        #region Public Properties
        [SourceNames(ColumnNameConstants.CREATED_DATE)]
        public DateTime CreatedDate { get; set; }

        [SourceNames(ColumnNameConstants.UPDATED_DATE)]
        public DateTime? UpdatedDate { get; set; }

        [SourceNames(ColumnNameConstants.DELETED_DATE)]
        public DateTime? DeletedDate { get; set; }

        [SourceNames(ColumnNameConstants.IS_ACTIVE)]
        public bool IsActive { get; set; }
        #endregion
    }
}
