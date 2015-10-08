using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDocumentTest
{

    /// <summary>
    /// MyMessage
    /// </summary>
    public struct MyMessage
    {

        DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        string _text;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        string _datestr;

        public string DateStr
        {
            get { return _datestr; }
            set { _datestr = value; }
        }

    }
}
