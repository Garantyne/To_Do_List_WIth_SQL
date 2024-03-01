using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List_WIth_SQL.Exceptions
{
    internal class TitleNullOrEmtyException: NullReferenceException
    {
        public TitleNullOrEmtyException(string ex) : base(ex) { }
    }
}
