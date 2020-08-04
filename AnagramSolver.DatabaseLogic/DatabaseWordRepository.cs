using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AnagramSolver.Data
{
    public class DatabaseWordRepository : IWordRepository
    {
        private readonly SqlConnection _sqlConnection;
        public DatabaseWordRepository()
        {
            var connectionString = Settings.ConnectionString;
            _sqlConnection = new SqlConnection(connectionString);

        }

        public bool AddWordToDataSet(string word, string languagePart)
        {
            //TODO Check if not exist

            var sortedWord = String.Concat(word.ToLower().OrderBy(c => c));
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _sqlConnection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "WordInsert";
            cmd.Parameters.Add(new SqlParameter("@Word", word));
            cmd.Parameters.Add(new SqlParameter("@Category", languagePart));
            cmd.Parameters.Add(new SqlParameter("@SortedWord", sortedWord));
            cmd.ExecuteNonQuery();
            _sqlConnection.Close();
            return true;
        }

        public List<WordModel> GetAllWords()
        {
            _sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select * from Word", _sqlConnection);
            SqlDataReader dr = command.ExecuteReader();
            List<WordModel> words = GenerateWordsList(dr);
            _sqlConnection.Close();
            return words;
        }

        public int GetTotalWordsCount()
        {
            int count = 0;
            _sqlConnection.Open();
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Word", _sqlConnection);
            object obj = command.ExecuteScalar();
            count = int.Parse(obj.ToString());
            _sqlConnection.Close();
            return count;
        }

        public int GetWordsCountBySerachedWord(string searchedWord)
        {
            searchedWord += "%";
            int count = 0;
            _sqlConnection.Open();
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Word Where word like @Word", _sqlConnection);
            command.Parameters.Add(new SqlParameter("@Word", searchedWord));
            object obj = command.ExecuteScalar();
            count = int.Parse(obj.ToString());
            _sqlConnection.Close();
            return count;
        }

        public Dictionary<string, List<WordModel>> GetWords()
        {
            throw new NotImplementedException();
        }

        public List<WordModel> GetWordsByRange(int pageIndex, int range)
        {
            var firstWordIndex = (pageIndex - 1) * range;
            var secondWordIndex = (pageIndex) * range;
            _sqlConnection.Open();
            var sqlQueryByRange = "Select * from Word where Id > " + firstWordIndex + " and Id <= " + secondWordIndex;
            SqlCommand command = new SqlCommand(sqlQueryByRange, _sqlConnection);
            SqlDataReader dr = command.ExecuteReader();
            List<WordModel> words = GenerateWordsList(dr);
            _sqlConnection.Close();
            return words;
        }

        public List<WordModel> SearchWordsByRangeAndFilter(int pageIndex, int range, string searchedWord)
        {
            searchedWord += "%";
            var firstWordIndex = (pageIndex - 1) * range;
            var secondWordIndex = (pageIndex) * range;
            _sqlConnection.Open();

            var sqlQueryByRange = "Select * " +
                "FROM( SELECT *, ROW_NUMBER() OVER(ORDER BY ID) AS RowNum " +
                "FROM Word Where word like @Word) as MyDerivedTable" +
                " WHERE MyDerivedTable.RowNum BETWEEN @IndexFrom AND @IndexTo";

            SqlCommand command = new SqlCommand(sqlQueryByRange, _sqlConnection);
            command.Parameters.Add(new SqlParameter("@Word", searchedWord));
            command.Parameters.Add(new SqlParameter("@IndexFrom", firstWordIndex));
            command.Parameters.Add(new SqlParameter("@IndexTo", secondWordIndex));
            SqlDataReader dr = command.ExecuteReader();
            List<WordModel> words = GenerateWordsList(dr);
            _sqlConnection.Close();
            return words;



        }
        public List<WordModel> FindSingleWordAnagrams(string sortedWord)
        {
            _sqlConnection.Open();
            var sqlQuery = "Select * from Word where SortedWord ='" + sortedWord + "'";
            SqlCommand command = new SqlCommand(sqlQuery, _sqlConnection);
            SqlDataReader dr = command.ExecuteReader();
            List<WordModel> words = GenerateWordsList(dr);
            _sqlConnection.Close();
            return words;
        }

        public List<WordModel> SearchWords(string word)
        {
            word += "%";
            _sqlConnection.Open();

            var sqlQuery = "Select * from Word where Word like '" + word + "'";
            SqlCommand command = new SqlCommand(sqlQuery, _sqlConnection);
            SqlDataReader dr = command.ExecuteReader();
            List<WordModel> words = GenerateWordsList(dr);
            _sqlConnection.Close();
            return words;
        }

        private List<WordModel> GenerateWordsList(SqlDataReader dataReader)
        {
            var words = new List<WordModel>();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    words.Add(new WordModel()
                    {
                        Id = dataReader["Id"].ToString(),
                        Word = dataReader["word"].ToString(),
                        LanguagePart = dataReader["Category"].ToString(),
                        SortedWord = dataReader["SortedWord"].ToString()
                    });
                }
            }
            return words;
        }
           
    }
}
