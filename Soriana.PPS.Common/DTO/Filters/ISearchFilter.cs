using System;

namespace Soriana.PPS.Common.DTO.Filters
{
    public interface ISearchFilter : ISQLStatementFilter, IParameterSearch
    {
        #region Public Properties
        long ById { get; set; }
        long InitialByIdRange { get; set; }
        long FinalByIdRange { get; set; }
        DateTime InitialDateRange { get; set; }
        DateTime FinalDateRange { get; set; }
        DateTime InitialTimeRange { get; set; }
        DateTime FinalTimeRange { get; set; }
        int PageNumber { get; set; }
        int RowsNumber { get; set; }
        bool ApplyById { get; set; }
        bool ApplyByIdRange { get; set; }
        bool ApplyDateRange { get; set; }
        bool ApplyTimeRange { get; set; }
        bool IsActive { get; set; }
        #endregion
    }
}
