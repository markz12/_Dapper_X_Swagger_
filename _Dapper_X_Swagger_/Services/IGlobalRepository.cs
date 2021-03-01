using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace _Dapper_X_Swagger_.Services
{
    interface IGlobalRepository : IDisposable
    {
        T Fetch<T>(string query, DynamicParameters param, CommandType commandType = CommandType.StoredProcedure);
        List<T> FetchAll<T>(string query, DynamicParameters param, CommandType commandType = CommandType.StoredProcedure);
        int Execute(string query, DynamicParameters param, CommandType commandType = CommandType.StoredProcedure);
        T GlobalCrud<T>(string query, DynamicParameters param, CommandType commandType = CommandType.StoredProcedure);
    }
}
