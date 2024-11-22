using ApiApplication1.Commands;
using ApiApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(SampleContext context) : ControllerBase
    {
        [HttpPost("Login")]
        public IActionResult Login(LoginCommand command)
        {
            //var user = context.Users.FirstOrDefault(user => user.Username == command.Username && user.Password == command.Password);
            //var sqlQuery = "SELECT TOP 1 * FROM Users WHERE Username = '" + command.Username + "' AND Password = '" + command.Password + "'";
            //var sqlQuery = $"SELECT TOP 1 * FROM Users WHERE Username = '{command.Username}' AND Password = '{command.Password}'";
            //var user = context.Users.FromSqlRaw(sqlQuery).FirstOrDefault();

            var user = context.Users
                .FromSql($"SELECT TOP 1 * FROM Users WHERE Username = '{command.Username}' AND Password = '{command.Password}'")
                .FirstOrDefault();

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok($"Hello {user.Username}!");
        }


        [HttpPost("LoginAdo")]
        public IActionResult LoginAdo(LoginCommand command)
        {
            using var sqlCommand = context.Database.GetDbConnection().CreateCommand();
            //sqlCommand.CommandText = $"SELECT TOP 1 Username FROM Users WHERE Username = '{command.Username}' AND Password = '{command.Password}'";
            sqlCommand.CommandText = "SELECT TOP 1 Username FROM Users WHERE Username = '@username' AND Password = '@password'";

            var usernameParameter = sqlCommand.CreateParameter();
            usernameParameter.ParameterName = "@username";
            usernameParameter.Value = command.Username;
            sqlCommand.Parameters.Add(usernameParameter);

            var passwordParameter = sqlCommand.CreateParameter();
            passwordParameter.ParameterName = "@password";
            passwordParameter.Value = command.Password;
            sqlCommand.Parameters.Add(passwordParameter);

            context.Database.OpenConnection();

            var username = sqlCommand.ExecuteScalar();
            if (username == null)
            {
                return Unauthorized();
            }

            return Ok($"Hello {username}!");
        }
    }
}
