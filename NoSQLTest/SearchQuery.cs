using System;
using System.Collections.Generic;
using System.Text;

namespace NoSQLTest
{
    public class SearchQuery
    {
        public string AttributeInnerLabel { get; set; }
        public string Value { get; set; }

        public SearchQuery(string label, string value)
        {
            AttributeInnerLabel = label;
            Value = value;
        }
    }
}
