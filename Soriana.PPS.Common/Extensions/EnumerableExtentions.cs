using Soriana.PPS.Common.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soriana.PPS.Common.Extensions
{
    public static class EnumerableExtentions
    {
        #region Public Methods
        public static string FromEnumerableStringToString(this IEnumerable<string> enumerableString)
        {
            if (enumerableString == null) return string.Empty;
            StringBuilder concatenatedString = new StringBuilder();
            foreach (string item in enumerableString)
            {
                if (item == null) continue;
                concatenatedString.Append(item);
                if (enumerableString.ToList().IndexOf(item) < (enumerableString.Count() - 1))
                {
                    concatenatedString.Append(CharactersConstants.COMMA_CHAR);
                    concatenatedString.Append(CharactersConstants.ESPACE_CHAR);
                }
            }
            return concatenatedString.ToString();
        }
        #endregion
    }
}
