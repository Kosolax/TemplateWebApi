namespace Template.BusinessObject.Validation.Service
{
    using System;
    using System.Collections.Generic;
    using System.Net.Mail;
    using System.Text.RegularExpressions;

    public abstract class ValidationService<T> : IValidationService<T>
    {
        public ValidationService()
        {
            this.ModelState = new Dictionary<string, string>();
            this.IsValid = true;
        }

        public bool IsValid { get; set; }

        public Dictionary<string, string> ModelState { get; set; }

        public void AddError(string key, string errorMessage)
        {
            if (!this.ModelState.ContainsKey(key))
            {
                this.ModelState.Add(key, errorMessage);
            }
            else
            {
                this.ModelState[key] = errorMessage;
            }

            this.IsValid = this.ModelState.Count == 0;
        }

        public void Clear()
        {
            try
            {
                this.ModelState.Clear();
            }
            catch
            {
                this.ModelState = new Dictionary<string, string>();
            }

            this.IsValid = this.ModelState.Count == 0;
        }

        public abstract bool Validate(T itemToValidate);

        protected void ClearDictionary(bool clear)
        {
            if (clear)
            {
                this.Clear();
            }
        }

        protected bool ValidateByteLower(byte itemToValidate, byte itemToCompare, string propertyName, string resource)
        {
            if (itemToValidate < itemToCompare)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateByteMax(byte itemToValidate, byte maxValue, string propertyName, string resource)
        {
            if (itemToValidate >= maxValue)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateByteMin(byte itemToValidate, byte minValue, string propertyName, string resource)
        {
            if (itemToValidate <= minValue)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateByteRange(byte itemToValidate, byte minValue, byte maxValue, string propertyName, string resource)
        {
            if (itemToValidate < minValue || itemToValidate > maxValue)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateByteUpper(byte itemToValidate, byte itemToCompare, string propertyName, string resource)
        {
            if (itemToValidate > itemToCompare)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateDateRequired(DateTime? itemToValidate, string propertyName, string resource)
        {
            if (!itemToValidate.HasValue)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateDateRequired(DateTime itemToValidate, string propertyName, string resource)
        {
            if (itemToValidate == default)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateDateUlterior(DateTime? previousDate, DateTime? ulteriorDate, string propertyName, string resource)
        {
            if (previousDate.HasValue && ulteriorDate.HasValue)
            {
                return this.ValidateDateUlterior(previousDate.Value, ulteriorDate.Value, propertyName, resource);
            }

            if (ulteriorDate.HasValue)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateDateUlterior(DateTime previousDate, DateTime ulteriorDate, string propertyName, string resource)
        {
            if (DateTime.Compare(ulteriorDate, previousDate) <= 0)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateDecimalLower(decimal itemToValidate, decimal itemToCompare, string propertyName, string resource)
        {
            if (itemToValidate < itemToCompare)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateDecimalMax(decimal itemToValidate, decimal maxValue, string propertyName, string resource)
        {
            if (itemToValidate >= maxValue)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateDecimalMin(decimal itemToValidate, decimal minValue, string propertyName, string resource)
        {
            if (itemToValidate <= minValue)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateDecimalUpper(decimal itemToValidate, decimal itemToCompare, string propertyName, string resource)
        {
            if (itemToValidate > itemToCompare)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateIntLower(int itemToValidate, int itemToCompare, string propertyName, string resource)
        {
            if (itemToValidate < itemToCompare)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateIntMax(int itemToValidate, int maxValue, string propertyName, string resource)
        {
            if (itemToValidate >= maxValue)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateIntMin(int itemToValidate, int minValue, string propertyName, string resource)
        {
            if (itemToValidate <= minValue)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateIntRange(int itemToValidate, int minValue, int maxValue, string propertyName, string resource)
        {
            if (itemToValidate < minValue || itemToValidate > maxValue)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateIntUpper(int itemToValidate, int itemToCompare, string propertyName, string resource)
        {
            if (itemToValidate > itemToCompare)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateLongLower(long itemToValidate, long itemToCompare, string propertyName, string resource)
        {
            if (itemToValidate < itemToCompare)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateLongMax(long itemToValidate, long maxValue, string propertyName, string resource)
        {
            if (itemToValidate >= maxValue)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateLongMin(long itemToValidate, long minValue, string propertyName, string resource)
        {
            if (itemToValidate <= minValue)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateLongRange(long itemToValidate, long minValue, long maxValue, string propertyName, string resource)
        {
            if (itemToValidate < minValue || itemToValidate > maxValue)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateLongUpper(long itemToValidate, long itemToCompare, string propertyName, string resource)
        {
            if (itemToValidate > itemToCompare)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateMailFormat(string itemToValidate, string propertyName, string resource)
        {
            try
            {
                if (!string.IsNullOrEmpty(itemToValidate))
                {
                    MailAddress m = new MailAddress(itemToValidate);
                    return true;
                }

                this.AddError(propertyName, resource);
                return false;
            }
            catch (FormatException)
            {
                this.AddError(propertyName, resource);
                return false;
            }
        }

        protected bool ValidateRegex(string itemToValidate, string regexAsString, string propertyName, string resource)
        {
            if (!string.IsNullOrEmpty(itemToValidate))
            {
                Regex regex = new Regex(regexAsString);

                if (regex.IsMatch(itemToValidate))
                {
                    return true;
                }
            }

            this.AddError(propertyName, resource);
            return false;
        }

        protected bool ValidateShortLower(short itemToValidate, short itemToCompare, string propertyName, string resource)
        {
            if (itemToValidate < itemToCompare)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateShortMax(short itemToValidate, short maxValue, string propertyName, string resource)
        {
            if (itemToValidate >= maxValue)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateShortMin(short itemToValidate, short minValue, string propertyName, string resource)
        {
            if (itemToValidate <= minValue)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateShortRange(short itemToValidate, short minValue, short maxValue, string propertyName, string resource)
        {
            if (itemToValidate < minValue || itemToValidate > maxValue)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateShortUpper(short itemToValidate, short itemToCompare, string propertyName, string resource)
        {
            if (itemToValidate > itemToCompare)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateStringLength(string itemToValidate, ushort length, string propertyName, string resource)
        {
            if (!string.IsNullOrEmpty(itemToValidate) && itemToValidate.Length > length)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateStringMinLength(string itemToValidate, ushort length, string propertyName, string resource)
        {
            if (!string.IsNullOrEmpty(itemToValidate) && itemToValidate.Length < length)
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }

        protected bool ValidateStringRequired(string itemToValidate, string propertyName, string resource)
        {
            if (string.IsNullOrEmpty(itemToValidate))
            {
                this.AddError(propertyName, resource);
                return false;
            }

            return true;
        }
    }
}