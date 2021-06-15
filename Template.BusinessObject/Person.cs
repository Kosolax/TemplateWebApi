namespace Template.BusinessObject
{
    using Template.BusinessObject.Validation;
    using Template.Entities;
    using Template.Entities.Enum;

    public class Person : BaseBusinessObject<PersonEntity>
    {
        public Person()
        {
            this.ValidationService = new PersonValidation();
        }

        public Person(PersonEntity entity)
            : base(entity)
        {
            this.Id = entity.Id;
            this.Email = entity.Email;
            this.Password = entity.Password;
            this.Role = entity.Role;
        }

        public string Email { get; set; }

        public int Id { get; set; }

        public string Password { get; set; }

        public RoleType Role { get; set; }

        public override PersonEntity CreateEntity()
        {
            return new PersonEntity
            {
                Id = this.Id,
                Email = this.Email,
                Password = this.Password,
                Role = this.Role,
            };
        }
    }
}