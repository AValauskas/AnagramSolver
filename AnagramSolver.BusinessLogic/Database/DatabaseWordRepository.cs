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

        public List<Anagram> GetAllWords()
        {
            _sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select * from Word", _sqlConnection);
            SqlDataReader dr = command.ExecuteReader();
            List<Anagram> words = new List<Anagram>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    words.Add(new Anagram() { Word = dr["word"].ToString(), LanguagePart = dr["Category"].ToString(), 
                        SortedWord = dr["SortedWord"].ToString() });
                }
            }
            _sqlConnection.Close();
            return words;
        }

        public int GetTotalWordsCount()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, List<Anagram>> GetWords()
        {
            _sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select * from Word", _sqlConnection);
            SqlDataReader dr = command.ExecuteReader();
            List<Anagram> words = new List<Anagram>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    words.Add(new Anagram() { Word = dr["word"].ToString(), LanguagePart = dr["Category"].ToString(),
                        SortedWord = dr["SortedWord"].ToString() });
                }
            }
            _sqlConnection.Close();
            return null;
        }

        public List<Anagram> GetWordsByRange(int pageIndex, int range)
        {
            throw new NotImplementedException();
        }

    }
}
