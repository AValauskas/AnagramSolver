using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.EF.DatabaseFirst.Models;
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
        public async Task<IEnumerable<CachedWord>> GetByWord(string word)
        {
            _sqlConnection.Open();
            var sqlQuery = "Select * from CachedWord where Word = @Word";
            SqlCommand command = new SqlCommand(sqlQuery, _sqlConnection);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@Word", word));
            SqlDataReader dr = await command.ExecuteReaderAsync();
            var cahcedWords = GenerateCachedWordsList(dr);
            _sqlConnection.Close();
            return cahcedWords;
        }
        public async Task<CachedWord> AddCachedWord(string word)
        {

            _sqlConnection.Open();
            var sqlQueryinsert = "INSERT INTO CachedWord (Word) output INSERTED.ID VALUES (@Word)";
            SqlCommand command = new SqlCommand(sqlQueryinsert, _sqlConnection);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@Word", word));
            object obj = await command.ExecuteScalarAsync();
            var Id = int.Parse(obj.ToString());
            _sqlConnection.Close();
            var cachedWord = new CachedWord() { Word = word, Id = Id };
            return cachedWord;
        }
        public async Task<bool> AddCachedWord_Word(Word word , CachedWord cachedWord)
        {

            _sqlConnection.Open();
            var sqlQueryinsert = "INSERT INTO CachedWord_Word (WordId, CachedWordId) VALUES (@WordId, @CachedWordId)";
            SqlCommand command = new SqlCommand(sqlQueryinsert, _sqlConnection);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@WordId", word.Id));
            command.Parameters.Add(new SqlParameter("@CachedWordId", cachedWord.Id));
            await command.ExecuteNonQueryAsync();
            _sqlConnection.Close();

            return true;
        }
        public async Task<IEnumerable<Word>> GetAnagrams(string word)
        {
            _sqlConnection.Open();
            var sqlQuery = "SELECT Word.Word, Word.Id, Word.Category, Word.SortedWord FROM CachedWord " +
                                    "INNER JOIN CachedWord_Word ON CachedWord.Id = CachedWord_Word.CachedWordId" +
                                    " INNER JOIN Word ON wordID = Word.Id " +
                                    "WHERE CachedWord.Word = @Word";
            SqlCommand command = new SqlCommand(sqlQuery, _sqlConnection);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@Word", word));
            SqlDataReader dr = await command.ExecuteReaderAsync();
            var cahcedWords = GenerateWordsList(dr);
            _sqlConnection.Close();
            return cahcedWords;
        }

        private IEnumerable<CachedWord> GenerateCachedWordsList(SqlDataReader dataReader)
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

        private IEnumerable<Word> GenerateWordsList(SqlDataReader dataReader)
        {
            var words = new List<Word>();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    words.Add(new Word()
                    {
                        Id = int.Parse(dataReader["Id"].ToString()),
                        Word1 = dataReader["word"].ToString(),
                        Category = dataReader["Category"].ToString(),
                        SortedWord = dataReader["SortedWord"].ToString()
                    });
                }
            }
            return words;
        }


    }
}
