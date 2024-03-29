﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List_WIth_SQL.Model
{
    internal class ToDo
    {
        public int Id {  get; set; }
        public string Name { get; set; }
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
