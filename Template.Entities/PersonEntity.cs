namespace Template.Entities
{
    using Template.Entities.Enum;

    public class PersonEntity : BaseEntity
    {
        public string Email { get; set; }

        public int Id { get; set; }

        public string Password { get; set; }

        public RoleType Role { get; set; }
    }
}