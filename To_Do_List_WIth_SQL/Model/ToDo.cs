using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_WIth_SQL.Exceptions;

namespace To_Do_List_WIth_SQL.Model
{
    internal class ToDo
    {
        public int Id {  get; set; }
        private string name;
        public string Name {
            get
            {  return name; }
                 set { 
                if( value == null || value == "")
                {
                    throw new TitleNullOrEmtyException("Пустое значение");
                }
                name = value;
            } }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public ToDo() { }

        public override string ToString()
        {
            return $"{Id} - {Name}  - {Deadline}";
        }

        public string ToDescriptionString()
        {
            return Description;
        }
    }
}
