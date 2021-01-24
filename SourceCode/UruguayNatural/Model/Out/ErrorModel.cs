using System.Collections.Generic;
using Domain.Validations;

namespace Model.Out
{
    public class ErrorModel
    {
        public IDictionary<string, IList<string>> Errors { get; set; }

        public ErrorModel(INotification notification)
        {
            Errors = notification.GetErrors();
        }
    }
}
