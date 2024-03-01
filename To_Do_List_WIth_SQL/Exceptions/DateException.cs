using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List_WIth_SQL.Exceptions
{
    internal class DateException : ArgumentException
    {
        public string DateValue {  get; set; }
        public DateException(string message, string dateValue) : base(message)
        {
            DateValue = dateValue;
        }
    }
}
