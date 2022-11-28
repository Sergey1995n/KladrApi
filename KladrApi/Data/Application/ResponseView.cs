using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KladrApi
{
    public enum RequestState
    {
        Failed = 0,
        Success = 1,
    }


    public class ResponseView
    {

        public ResponseView(dynamic item, RequestState state = RequestState.Success, string msg = "")
        {
            this.data = item;
            this.state = state;
            this.message = msg;
        }

        public dynamic data { get; set; }
        public RequestState state { get; set; }
        public string message { get; set; }

    }

}
