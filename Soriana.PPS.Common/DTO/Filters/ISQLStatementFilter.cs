namespace Soriana.PPS.Common.DTO.Filters
{
    public interface ISQLStatementFilter
    {
        #region Public Properties
        string ConditionClause { get; set; }
        string OrderClause { get; set; }
        #endregion
    }
}
