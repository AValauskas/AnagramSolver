using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Data.Database
{
    public class TableHandler : ITableHandler
    {

        private readonly SqlConnection _sqlConnection;
        public TableHandler()
        {
            var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=AnagramSolverCFDB; Integrated Security = True;";
            _sqlConnection = new SqlConnection(connectionString);
        }
        public async Task CleanTables(string table)
        {
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _sqlConnection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TableCleaner";
            cmd.Parameters.Add(new SqlParameter("@tableName", table));
            await cmd.ExecuteNonQueryAsync();
            _sqlConnection.Close();
           
        }
    }
}
