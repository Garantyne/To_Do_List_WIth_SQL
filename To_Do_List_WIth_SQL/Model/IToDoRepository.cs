using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List_WIth_SQL.Model
{
    internal interface IToDoRepository
    {
        List<ToDo> GetAll();

        ToDo GetById(int id);

        void DeleteById(int id);

        void UpdateToDo(ToDo toDo, int i);

        void AddToDo(ToDo toDo);
    }
}
