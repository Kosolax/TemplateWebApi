namespace Template.BusinessObject.Validation
{
    using Template.BusinessObject.Validation.Resources;
    using Template.BusinessObject.Validation.Service;
    using Template.Entities;
    using Template.Entities.Enum;

    public class PersonValidation : ValidationService<PersonEntity>
    {
        public override bool Validate(PersonEntity itemToValidate)
        {
            this.Clear();
            this.ValidateEmail(itemToValidate.Email);
            this.ValidatePassword(itemToValidate.Password);
            this.ValidateRole(itemToValidate.Role);

            return this.IsValid;
        }

        private void ValidateEmail(string itemToValidate)
        {
            this.ValidateMailFormat(itemToValidate, nameof(PersonValidationResource.Person_Mail_Format), PersonValidationResource.Person_Mail_Format);
            this.ValidateStringRequired(itemToValidate, nameof(PersonValidationResource.Person_Mail_Required), PersonValidationResource.Person_Mail_Required);
            this.ValidateStringLength(itemToValidate, 100, nameof(PersonValidationResource.Person_Mail_Max_Length), PersonValidationResource.Person_Mail_Max_Length);
        }

        private void ValidatePassword(string itemToValidate)
        {
            this.ValidateStringLength(itemToValidate, 100, nameof(PersonValidationResource.Person_Password_Length), PersonValidationResource.Person_Password_Length);
            this.ValidateStringRequired(itemToValidate, nameof(PersonValidationResource.Person_Password_Required), PersonValidationResource.Person_Password_Required);
            this.ValidateRegex(itemToValidate, @"^(?=.{8,16}$)(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9]).*$", nameof(PersonValidationResource.Person_Password_Regex), PersonValidationResource.Person_Password_Regex);
        }

        private void ValidateRole(RoleType itemToValidate)
        {
            switch (itemToValidate)
            {
                case RoleType.Admin:
                case RoleType.Basic:
                    return;
            }

            this.AddError(nameof(PersonValidationResource.Person_Role), PersonValidationResource.Person_Role);
        }
    }
}