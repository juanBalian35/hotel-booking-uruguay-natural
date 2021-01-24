using System.Collections.Generic;
using System.Linq;

namespace Domain.Validations
{
    public abstract class Validatable
    {
        private readonly Dictionary<string, List<IValidation>> Validations;

        protected Validatable()
        {
            Validations = new Dictionary<string, List<IValidation>>();
        }

        public bool IsValid(string excludeFields = "")
        {
            return !Validate(excludeFields).HasErrors();
        }

        public INotification Validate(string excludeFields = "")
        {
            var fieldsToExcludeSplitted = excludeFields.Split(',');
            var notification = new Notification();
            foreach (var field in Validations.Keys.Where(field => !fieldsToExcludeSplitted.Contains(field)))
            {
                ValidateField(field, notification);
            }

            return notification;
        }

        protected void AddValidation(string field, IValidation validation)
        {
            if(!Validations.ContainsKey(field))
            {
                Validations[field] = new List<IValidation>();
            }

            Validations[field].Add(validation);
        }

        protected void RemoveFieldValidations(string field)
        {
            Validations[field] = new List<IValidation>();
        }
        
        private void ValidateField(string field, INotification notification)
        {
            var value = GetPropertyValue(this, field);

            foreach (var validation in Validations[field].Where(validation => !validation.Validate(value)))
            {
                notification.AddError(field, validation.GetError());
            }
        }
        
        private object GetPropertyValue(object src, string propName)
        {
            if(!propName.Contains("."))
            {
                var prop = src.GetType().GetProperty(propName);
                return prop != null ? prop.GetValue(src, null) : null;
            }
            
            var splittedName = propName.Split(new[] { '.' }, 2);
            return GetPropertyValue(GetPropertyValue(src, splittedName[0]), splittedName[1]);
        }
    }
}
