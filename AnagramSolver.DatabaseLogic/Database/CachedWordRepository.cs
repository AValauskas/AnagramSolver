using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Contracts.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AnagramSolver.Data.Database
{
    public class CachedWordRepository: ICachedWordRepository
    {
        private readonly SqlConnection _sqlConnection;
        public CachedWordRepository()
        {
            var connectionString = Settings.ConnectionString;
            _sqlConnection = new SqlConnection(connectionString);
        }
        public async Task<List<CachedWord>> GetByWord(string word)
        {
            _sqlConnection.Open();
            var sqlQueryByRange = "Select * from CachedWord where";
            SqlCommand command = new SqlCommand(sqlQueryByRange, _sqlConnection);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@Word", word));
            SqlDataReader dr = await command.ExecuteReaderAsync();
            var cahcedWords = GenerateCachedWordsList(dr);
            _sqlConnection.Close();
            return cahcedWords;
        }
        public async Task<int> AddCachedWord(string word)
        {

            _sqlConnection.Open();
            var sqlQueryByRange = "INSERT INTO CachedWord (Word) VALUES (@Word)";
            SqlCommand command = new SqlCommand(sqlQueryByRange, _sqlConnection);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@Word", word));
            var result = await command.ExecuteNonQueryAsync();
            _sqlConnection.Close();

            return result;
        }
        public async Task<bool> AddCachedWord_Word(int wordId , int cachedWordID)
        {

            _sqlConnection.Open();
            var sqlQueryByRange = "INSERT INTO CachedWord_Word (WordId, CachedWordId) VALUES (@WordId, @CachedWordId)";
            SqlCommand command = new SqlCommand(sqlQueryByRange, _sqlConnection);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@WordId", wordId));
            command.Parameters.Add(new SqlParameter("@CachedWordId", cachedWordID));
            await command.ExecuteNonQueryAsync();
            _sqlConnection.Close();

            return true;
        }
        public async Task<List<CachedWord>> GetAnagrams(string word)
        {
            _sqlConnection.Open();
            var sqlQueryByRange = "SELECT Word.Word FROM CachedWord " +
                                    "INNER JOIN CachedWord_Word ON CachedWord.Id = CachedWord_Word.CachedWordId" +
                                    " INNER JOIN Word ON wordID = Word.Id " +
                                    "WHERE CachedWord.Word = @Word";
            SqlCommand command = new SqlCommand(sqlQueryByRange, _sqlConnection);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@Word", word));
            SqlDataReader dr = await command.ExecuteReaderAsync();
            var cahcedWords = GenerateCachedWordsList(dr);
            _sqlConnection.Close();
            return cahcedWords;
        }

        private List<CachedWord> GenerateCachedWordsList(SqlDataReader dataReader)
        {
            var words = new List<CachedWord>();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    words.Add(new CachedWord()
                    {
                        Word = dataReader["word"].ToString(),
                        Id = int.Parse(dataReader["id"].ToString())
                    });
                }
            }
            return words;
        }

        
    }
}
