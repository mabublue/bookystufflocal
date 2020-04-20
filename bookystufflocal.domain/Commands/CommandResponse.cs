using System;
using System.Collections.Generic;
using System.Linq;
using bookystufflocal.domain.DomainLayer.BaseModels;

namespace bookystufflocal.domain.Commands
{
    public class CommandResponse
    {
        public CommandResponse()
        {
            ValidationErrors = new List<ValidationError>();
        }

        public CommandResponse(IEnumerable<ValidationError> validationErrors)
        {
            ValidationErrors = validationErrors.ToList();
        }

        //If this collection has members then there was a problem!
        public IList<ValidationError> ValidationErrors { get; set; }
        public object Data { get; set; }
        public bool IsSuccess => !ValidationErrors.Any();
        public string ValidationErrorsString => string.Join(",", ValidationErrors.Select(x => $"{x.Field}: {x.Message}"));
        public bool HasErrors => ValidationErrors.Any();

        public void Match(Action<object> onSuccessFunc, Action<IEnumerable<ValidationError>> onFailureFunc)
        {
            if (IsSuccess)
                onSuccessFunc(Data);
            else
                onFailureFunc(ValidationErrors);
        }

        public TResult Match<TResult>(Func<object, TResult> onSuccessFunc, Func<IEnumerable<ValidationError>, TResult> onFailureFunc)
        {
            return IsSuccess ? onSuccessFunc(Data) : onFailureFunc(ValidationErrors);
        }
    }
}
