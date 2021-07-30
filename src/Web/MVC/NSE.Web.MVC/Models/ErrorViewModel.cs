using System;

namespace NSE.Web.MVC.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string Message { get; set; }
        public Exception Exception { get; set; }
        public int Code { get; set; }
        public bool ShowCode => Code != 0;
    }
}
