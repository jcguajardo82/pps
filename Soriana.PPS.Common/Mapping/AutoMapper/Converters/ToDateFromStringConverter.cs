using AutoMapper;
using Soriana.PPS.Common.Constants;
using System;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class ToDateFromStringConverter : ITypeConverter<string, DateTime>
    {
        #region Public Methods
        public DateTime Convert(string source, DateTime destination, ResolutionContext context)
        {
            destination = new DateTime(DateTimeConstants.DEFAULT_YEAR, DateTimeConstants.DEFAULT_MONTH, DateTimeConstants.DEFAULT_DAY, DateTimeConstants.DEFAULT_HOUR, DateTimeConstants.DEFAULT_MINUTE, DateTimeConstants.DEFAULT_SECOND, DateTimeConstants.DEFAULT_MILISECOND, DateTimeKind.Local);
            if (!string.IsNullOrEmpty(source) && DateTime.TryParse(source, out DateTime dateResult))
                destination = new DateTime(dateResult.Year, dateResult.Month, dateResult.Day, 0, 0, 0, 0);
            return destination;
        }
        #endregion
    }
}
