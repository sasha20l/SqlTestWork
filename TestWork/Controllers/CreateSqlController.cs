using System;
using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc;

namespace TestWork.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CreateSqlController : Controller
    {
        static string connectionString = "Data Source=:memory:";
        public SQLiteConnection connection = new SQLiteConnection(connectionString);

        public object Performer { get; private set; }

        public void CreateSql(SQLiteCommand command) // Создание таблицы Sql
        {
            command.CommandText = "DROP TABLE IF EXISTS TableSQL";
            command.ExecuteNonQuery();
            command.CommandText = @"CREATE TABLE TableSQL(DocumentNumber INTEGER
                        , Performer VARCHAR(2000), ApprovalState VARCHAR(2000))";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO TableSQL(DocumentNumber, Performer, ApprovalState) VALUES(1, 'Иванов И.И.','Согласовано')";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO TableSQL(DocumentNumber, Performer, ApprovalState) VALUES(1, 'Петров П.П.','Ожидается решение')";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO TableSQL(DocumentNumber, Performer, ApprovalState) VALUES(1, 'Сидоров С.С.','Ожидается решение')";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO TableSQL(DocumentNumber, Performer, ApprovalState) VALUES(2, 'Ульянов В.Б.','Согласовано')";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO TableSQL(DocumentNumber, Performer, ApprovalState) VALUES(2, 'Иванов И.И.','Ожидается решение')";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO TableSQL(DocumentNumber, Performer, ApprovalState) VALUES(3, 'Петров П.П.','Согласовано')";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO TableSQL(DocumentNumber, Performer, ApprovalState) VALUES(4, 'Петров П.П.','Ожидается решение')";
            command.ExecuteNonQuery();
        }

        [HttpGet("Return_TableSQL")]// Возвращает таблицу TableSQL
        public IActionResult ReturnTableSQL()
        {
            using (connection)
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    CreateSql(command);
                    string readQuery = "SELECT * FROM TableSQL ";
                    var returnArray = new TableSQL[6];
                    command.CommandText = readQuery;
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        var counter = 0;
                        while (reader.Read())
                        {
                            string value = reader.GetValue(1).ToString();
                            returnArray[counter] = new TableSQL
                            {
                                DocumentNumber = reader.GetInt32(0),
                                Performer = reader.GetValue(1).ToString(),
                                ApprovalState = reader.GetValue(2).ToString(),

                            }; 
                            counter++;
                        }
                    }
                    return Ok(returnArray);
                }
            }
        }

        [HttpGet("Return_Task1")]
        public IActionResult ReturnSqlLiteTask1()
        {
            using (connection)
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    CreateSql(command);
                    string readQuery = "SELECT TableSQL.DocumentNumber, TableSQL.Performer FROM TableSQL WHERE TableSQL.ApprovalState = 'Ожидается решение' ORDER BY TableSQL.DocumentNumber";
                    var returnArray = new Task1[4];
                    command.CommandText = readQuery;
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        var counter = 0;
                        while (reader.Read())
                        {
                            returnArray[counter] = new Task1
                            {
                                DocumentNumber = reader.GetInt32(0),
                                Performer = reader.GetValue(1).ToString(),

                            };
                            counter++;
                        }
                    }
                    return Ok(returnArray);
                }
            }
        }

        
    }
}


