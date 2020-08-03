using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AnagramSolver.BusinessLogic.Database
{
    public class DatabaseWordRepository : IWordRepository
    {
        private readonly SqlConnection _sqlConnection;
        public DatabaseWordRepository()
        {
            var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=AnagramSolver;";//config.GetConnectionString("Development");
            _sqlConnection = new SqlConnection(connectionString);
            
        }
        public bool AddWord(string sortedWord, string word, string languagePart)
        {
            throw new NotImplementedException();
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
            List<WordModel> words = new List<WordModel>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    words.Add(new WordModel() { Word = dr["word"].ToString(), LanguagePart = dr["Category"].ToString(), 
                        SortedWord = dr["SortedWord"].ToString() });
                }
            }
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

        public Dictionary<string, List<WordModel>> GetWords()
        {
            _sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select * from Word", _sqlConnection);
            SqlDataReader dr = command.ExecuteReader();
            List<WordModel> words = new List<WordModel>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    words.Add(new WordModel() {
                        Id = dr["Id"].ToString(),
                        Word = dr["word"].ToString(),
                        LanguagePart = dr["Category"].ToString(),
                        SortedWord = dr["SortedWord"].ToString() });
                }
            }
            _sqlConnection.Close();
            return null;
        }

        public List<WordModel> GetWordsByRange(int pageIndex, int range)
        {
            var firstWordIndex = (pageIndex - 1) * range;
            var secondWordIndex = (pageIndex) * range;
            _sqlConnection.Open();
            var sqlQueryByRange = " SELECT* FROM(SELECT*, ROW_NUMBER() OVER (Word BY Id) as row FROM Word) a WHERE row > " + firstWordIndex + " and row <= " + secondWordIndex;
           // var sqlQueryByRange = "Select * from Word BETWEEN " + firstWordIndex + " AND " + secondWordIndex;
            SqlCommand command = new SqlCommand(sqlQueryByRange, _sqlConnection);
            SqlDataReader dr = command.ExecuteReader();
            List<WordModel> words = new List<WordModel>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    words.Add(new WordModel()
                    {
                        Id = dr["Id"].ToString(),
                        Word = dr["word"].ToString(),
                        LanguagePart = dr["Category"].ToString(),
                        SortedWord = dr["SortedWord"].ToString()
                    });
                }
            }
            _sqlConnection.Close();
            return null;
        }

    }
}
