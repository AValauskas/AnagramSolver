using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Data.Database
{
    public class UserLogRepository : IUserLogRepository
    {
        private readonly SqlConnection _sqlConnection;
        public UserLogRepository()
        {
            var connectionString = Settings.ConnectionString;
            _sqlConnection = new SqlConnection(connectionString);
        }
        public async Task CreateLog(UserLog log)
        {
            _sqlConnection.Open();
            var sqlQuery = "INSERT INTO UserLog (UserIp,SearchedWord,Time,Anagrams)  VALUES (@Ip,@Word,@Time,@Anagrams)";
            SqlCommand command = new SqlCommand(sqlQuery, _sqlConnection);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@Word", log.Word));
            command.Parameters.Add(new SqlParameter("@Ip", log.UserIp));
            command.Parameters.Add(new SqlParameter("@Time", log.Time));
            command.Parameters.Add(new SqlParameter("@Anagrams", log.Anagrams));
            await command.ExecuteNonQueryAsync();
            _sqlConnection.Close();
        }

        public async Task<List<UserLog>> GetLogs()
        {
            _sqlConnection.Open();
            var sqlQuery = "SELECT * from UserLog";
            SqlCommand command = new SqlCommand(sqlQuery, _sqlConnection);
            command.CommandType = CommandType.Text;
            SqlDataReader dr = await command.ExecuteReaderAsync();
            var cahcedWords = GenerateLogList(dr);
            _sqlConnection.Close();
            return cahcedWords;
        }

        private List<UserLog> GenerateLogList(SqlDataReader dataReader)
        {
            var words = new List<UserLog>();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    words.Add(new UserLog()
                    {
                        Anagrams = dataReader["Anagrams"].ToString(),
                        Word = dataReader["SearchedWord"].ToString(),
                        UserIp = dataReader["UserIp"].ToString(),
                        Time = DateTime.Parse(dataReader["Time"].ToString())
                    });
                }
            }
            return words;
        }
       
    }
}
