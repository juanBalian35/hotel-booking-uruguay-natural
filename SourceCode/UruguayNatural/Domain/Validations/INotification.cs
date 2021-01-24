using System.Collections.Generic;

namespace Domain.Validations
{
    public interface INotification
    {
        void AddError(string field, string error);
        bool HasErrors();
        void Merge(INotification anotherNotification);
        IDictionary<string, IList<string>> GetErrors();
    }
}