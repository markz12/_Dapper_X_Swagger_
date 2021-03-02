using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _Dapper_X_Swagger_.Services
{
    public class ResponseAPI<T>
    {
        public int code { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
}
