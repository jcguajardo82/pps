using System;

namespace Soriana.PPS.Common.DTO.Filters
{
    public abstract class SearchFilterBase : ISearchFilter
    {
        #region Public Properties
        public long ById { get; set; }
        public long InitialByIdRange { get; set; }
        public long FinalByIdRange { get; set; }
        public DateTime InitialDateRange { get; set; }
        public DateTime FinalDateRange { get; set; }
        public DateTime InitialTimeRange { get; set; }
        public DateTime FinalTimeRange { get; set; }
        public int PageNumber { get; set; }
        public int RowsNumber { get; set; }
        public bool ApplyById { get; set; }
        public bool ApplyByIdRange { get; set; }
        public bool ApplyDateRange { get; set; }
        public bool ApplyTimeRange { get; set; }
        public string ConditionClause { get; set; }
        public string OrderClause { get; set; }
        public object Parameters { get; set; }
        public bool IsActive { get; set; }
        #endregion
    }
}
