using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List_WIth_SQL.Model
{
    internal class ToDoRepository : IToDoRepository
    {
        public SqlConnection Connection { get; set; }

        public ToDoRepository(SqlConnection connect) {
            Connection = connect;
        }

        public void AddToDo(ToDo toDo)
        {
            
            string stringConnection = $"insert into ToDos VALUES (@name, @description,@deadline)";
            using (SqlCommand Command = new SqlCommand(stringConnection, Connection))
            {
                Command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = toDo.Name;
                Command.Parameters.Add("@description", System.Data.SqlDbType.NVarChar).Value = toDo.Description;
                Command.Parameters.Add("@deadline", System.Data.SqlDbType.Date).Value = toDo.Deadline;
                Command.ExecuteNonQuery();
            }
            
        }

        public void DeleteById(int id)
        {
            string stringConnection = $"delete from ToDos where id = @id";
            using (SqlCommand Command = new SqlCommand(stringConnection, Connection))
            {
                Command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                Command.ExecuteNonQuery();
            }
        }

        public List<ToDo> GetAll()
        {
            List<ToDo> toDos = new List<ToDo>();
            using (SqlCommand Command = new SqlCommand("select * FROM ToDos", Connection))
            {
                using (SqlDataReader Reader = Command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        ToDo toDo = InitialazeValueEntity(Reader);                        
                        toDos.Add(toDo);
                    }
                }
            }
            return toDos;
        }

        public ToDo GetById(int id)
        {
            ToDo result = new ToDo();
            string stringConnection = $"select * from ToDos where id = @id";
            using (SqlCommand Command = new SqlCommand(stringConnection, Connection))
            {
                Command.Parameters.Add("@id",System.Data.SqlDbType.Int).Value = id;
                using(SqlDataReader Reader = Command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        result = InitialazeValueEntity( Reader);
                    }
                }
            } 
            return result;
        }

        public void UpdateToDo(ToDo toDo, int id)
        {
            string stringConnection = $"update ToDos set " +
                $"name = @name, description = @description, deadline = @deadline where  id = @id";
            using (SqlCommand Command = new SqlCommand(stringConnection, Connection))
            {
                Command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = toDo.Id;
                Command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = toDo.Name;
                Command.Parameters.Add("@description", System.Data.SqlDbType.NVarChar).Value = toDo.Description;
                Command.Parameters.Add("@deadline", System.Data.SqlDbType.Date).Value = toDo.Deadline;
                Command.ExecuteNonQuery();
            }
        }

        private ToDo InitialazeValueEntity( SqlDataReader Reader)
        {
            ToDo todo = new ToDo();
            todo.Id = Reader.GetInt32(0);
            todo.Name = Reader.GetString(1);
            todo.Description = Reader.GetString(2);
            todo.Deadline = Reader.GetDateTime(3);
            return todo;
        }
    }
}
