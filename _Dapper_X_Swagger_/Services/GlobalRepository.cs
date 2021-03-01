using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Dapper_X_Swagger_.Services;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace _Dapper_X_Swagger_.Services
{
    public class GlobalRepository : IGlobalRepository
    {
        private readonly string ConnectionString = "default";
        private readonly IConfiguration _config;

        public GlobalRepository(IConfiguration config)
        {
            _config = config;
        }
        public void Dispose()
        {
            //This will be used for disposed of actions
        }

        /// <summary>
        /// This will be used for  fetching of data
        /// </summary>
        /// <returns></returns>
        public T Fetch<T>(string query, DynamicParameters param, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                using IDbConnection db = new SqlConnection(_config.GetConnectionString(ConnectionString));
                return db.Query<T>(query, param, commandType: commandType).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This will be used for  fetching list of data
        /// </summary>
        /// <returns></returns>
        public List<T> FetchAll<T>(string query, DynamicParameters param, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(ConnectionString));
            return db.Query<T>(query, param, commandType: commandType).ToList();
        }


        /// <summary>
        /// This Global Crud can be used for Create, Read, Update, Delete
        /// </summary>
        /// <returns></returns>
        public T GlobalCrud<T>(string query, DynamicParameters param, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(ConnectionString));
            try
            {
                if (db.State == ConnectionState.Closed) db.Open();
                using var transaction = db.BeginTransaction();
                try
                {
                    db.Execute(query, param, commandType: commandType, transaction: transaction);
                    result = param.Get<T>("return");
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open) db.Close();
            }
            return result;
        }

        public int Execute(string query, DynamicParameters param, CommandType commandType = CommandType.StoredProcedure)
        {
            throw new NotImplementedException(); // No action added. It will be used for future development
        }
    }
}
