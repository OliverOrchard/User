using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api
{

    public class Response<T>
    {
        public T Data { get; set; }

        public Response()
        {
        }

        public Response(T data)
        {
            this.Data = data;
        }
    }
}
