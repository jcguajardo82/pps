using AutoMapper;
using Soriana.PPS.Common.Constants;
using System;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class ToTimeFromStringConverter : ITypeConverter<string, TimeSpan>
    {
        #region Public Methods
        public TimeSpan Convert(string source, TimeSpan destination, ResolutionContext context)
        {
            destination = new DateTime(DateTimeConstants.DEFAULT_YEAR, DateTimeConstants.DEFAULT_MONTH, DateTimeConstants.DEFAULT_DAY, DateTimeConstants.DEFAULT_HOUR, DateTimeConstants.DEFAULT_MINUTE, DateTimeConstants.DEFAULT_SECOND, DateTimeConstants.DEFAULT_MILISECOND, DateTimeKind.Local).TimeOfDay;
            if (!string.IsNullOrEmpty(source) && DateTime.TryParse(source, out DateTime dateResult))
                destination = dateResult.TimeOfDay;
            return destination;
        }
        #endregion
    }
}
