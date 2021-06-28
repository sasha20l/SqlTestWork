
using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc;

namespace TestWork.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SqlController : ControllerBase
    {
        [HttpGet]
        public IActionResult TryToSqlLite()
        {
            string cs = "Data Source=:memory:";
            string stm = "SELECT SQLITE_VERSION()";
            using (var con = new SQLiteConnection(cs))
            {
                con.Open();
                using var cmd = new SQLiteCommand(stm, con);
                string version = cmd.ExecuteScalar().ToString();
                return Ok(version);
            }
        }
    }
}
