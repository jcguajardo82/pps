using System;
using System.Collections.Generic;
using System.Linq;

namespace Soriana.PPS.Common.Mapping
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class SourceNamesAttribute : Attribute
    {
        #region Private Fields
        private IList<string> _ColumnNames { get; set; }
        private bool _IsCustomClass { get; set; } = false;
        private bool _ConcatInnerPropertyName { get; set; } = false;
        private bool _IsCustomClassSerializedAsJson { get; set; } = false;
        #endregion
        #region Public Properties
        public IList<string> ColumnNames { get { return _ColumnNames; } set { _ColumnNames = value; } }
        public bool IsCustomClass { get { return _IsCustomClass; } set { _IsCustomClass = value; } }
        public bool ConcatInnerPropertyName { get { return _ConcatInnerPropertyName; } set { _ConcatInnerPropertyName = value; } }
        public bool IsCustomClassSerializedAsJson { get { return _IsCustomClassSerializedAsJson; } set { _IsCustomClassSerializedAsJson = value; } }
        #endregion
        #region Public Constructors
        public SourceNamesAttribute()
        {
            _ColumnNames = new List<string>();
        }
        public SourceNamesAttribute(params string[] columnNames)
        {
            _ColumnNames = columnNames.ToList();
        }
        public SourceNamesAttribute(bool isCustomClass = false, params string[] columnNames)
        {
            _ColumnNames = columnNames.ToList();
            _IsCustomClass = isCustomClass;
        }
        public SourceNamesAttribute(bool isCustomClass = false, bool isCustomClassSerializedAsJson = false, bool concatInnerPropertyName = false, params string[] columnNames)
        {
            _ColumnNames = columnNames.ToList();
            _IsCustomClass = isCustomClass;
            _ConcatInnerPropertyName = concatInnerPropertyName;
            _IsCustomClassSerializedAsJson = isCustomClassSerializedAsJson;
        }
        #endregion
    }
}
