using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace NSE.Core.Controller.Base
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        private IList<KeyValuePair<string, string>> _errors { get; set; }

        protected IReadOnlyCollection<KeyValuePair<string, string>> Errors => _errors.ToArray();

        public ApiController()
        {
            _errors = new List<KeyValuePair<string, string>>();
        }

        protected void AddError(string key, string value)
        {
            _errors.Add(new KeyValuePair<string, string>(key, value));
        }

        protected void ClearErrors()
        {
            _errors.Clear();
        }

        protected bool IsValid => !_errors.Any();

        protected IActionResult ApiResponse(string message = null, object data = null)
        {
            var response = CreateResponse(message, data);
            if (!response.IsValid)
                return BadRequest(response);

            return Ok(response);
        }

        protected IActionResult ApiResponse(IEnumerable<IdentityError> errors)
        {
            errors.ToList().ForEach(x => AddError(x.Code.StartsWith("Password") ? "Password" : "Identity", x.Description));
            return BadRequest(CreateResponse());
        }

        protected IActionResult ApiResponse(ModelStateDictionary modelState)
        {
            modelState.ToList().ForEach(x => x.Value.Errors.ToList().ForEach(y => AddError(x.Key, y.ErrorMessage)));
            return UnprocessableEntity(CreateResponse());
        }

        private ApiResponse CreateResponse(string message = null, object data = null)
        {
            return new ApiResponse
            {
                IsValid = IsValid,
                Errors = _errors.ToArray(),
                Data = data,
                Message = message
            };
        }

        protected Guid UserId => Guid.Parse(User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value!);
    }

    public class ApiResponse
    {
        public bool IsValid { get; set; }
        public KeyValuePair<string, string>[] Errors { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
