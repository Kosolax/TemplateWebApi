namespace Template.WebApi
{
    using Microsoft.AspNetCore.Authorization;

    using Template.Entities.Enum;

    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params RoleType[] roles)
        {
            this.Roles = string.Join(",", roles);
        }
    }
}