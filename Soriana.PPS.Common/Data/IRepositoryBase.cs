namespace Soriana.PPS.Common.Data
{
    public interface IRepositoryBase
    {
        #region Public Methods
        T CreateRepository<T, C, R, U, D>(C create, R read, U update, D delete);
        void SetCommand(string command);
        void ClearCommand();
        void SetParameters(object parameters);
        void ClearParameters();
        #endregion
    }
}
