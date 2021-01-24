using System.Collections.Generic;
using System.Linq;

namespace Domain.Validations
{
    public class Notification : INotification
    {
        private readonly Dictionary<string, IList<string>> Errors;

        public Notification()
        {
            Errors = new Dictionary<string, IList<string>>();
        }

        public bool HasErrors()
        {
            return Errors.Count > 0;
        }

        public void AddError(string field, string error)
        {
            if(!Errors.ContainsKey(field))
            {
                Errors[field] = new List<string>();
            }

            Errors[field].Add(error);
        }

        public void Merge(INotification anotherNotification)
        {
            foreach (var error in anotherNotification.GetErrors())
            {
                var field = error.Key;
                var errorsForField = error.Value;
                errorsForField.ToList().ForEach(x => AddError(field, x));
            }
        }
        
        public IDictionary<string, IList<string>> GetErrors()
        {
            return Errors;
        }
    }
}
