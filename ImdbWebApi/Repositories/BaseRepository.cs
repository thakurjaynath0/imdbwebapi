using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImdbWebApi.Exceptions;

namespace ImdbWebApi.Repositories
{
    public class BaseRepository <T> where T : class
    {
        private readonly string _connectionString;

        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;    
        }

        public async Task<IEnumerable<T>> GetAllAsync(string query, object parameters = null)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.QueryAsync<T>(query, parameters);
            }
            catch (CustomException)
            {
                throw new InternalServerException("Error while processing request to database.");
            }
        }

        public async Task<T> GetAsync(string query, object parameters)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.QueryFirstOrDefaultAsync<T>(query, parameters);
            }
            catch (CustomException)
            {
                throw new InternalServerException("Error while processing request to database.");
            }
        }

        public async Task ExecuteQueryAsync(string query, object parameters)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(query, parameters);
            }
            catch (CustomException)
            {
                throw new InternalServerException("Error while processing request to database.");
            }
        }

        public async Task ExecuteStoredProcedureAsync(string sp, object parameters)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sp, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (CustomException)
            {
                throw new InternalServerException("Error while processing request to database.");
            }
        }
    }
}
