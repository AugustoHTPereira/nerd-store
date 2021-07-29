using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Models.Base
{
    public class APIResponseBase
    {
        [JsonPropertyName("isValid")]
        public bool IsValid { get; set; }

        [JsonPropertyName("errors")]
        public object Errors { get; set; }

        [JsonPropertyName("data")]
        public virtual object Data { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    public class APIResponseBase<TData> : APIResponseBase
        where TData : class
    {

        [JsonPropertyName("data")]
        public new TData Data { get; set; }
    }
}
