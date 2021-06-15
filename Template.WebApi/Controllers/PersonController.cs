namespace Template.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Template.BusinessObject;
    using Template.BusinessObject.Validation.Resources;
    using Template.IBusiness;
    using Template.WebApi.Route;

    [Route(PersonRoute.RoutePrefix)]
    public class PersonController : Controller
    {
        private readonly IPersonBusiness personBusiness;

        public PersonController(IPersonBusiness personBusiness)
        {
            this.personBusiness = personBusiness;
        }

        [HttpPost]
        [Route(PersonRoute.Connection)]
        public async Task<IActionResult> Connection()
        {
            return this.BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Person personToCreate)
        {
            try
            {
                KeyValuePair<bool, Person> result = await this.personBusiness.CreateOrUpdate(personToCreate);
                if (!result.Key)
                {
                    if (result.Value != null)
                    {
                        return this.BadRequest(result.Value.ValidationService.ModelState);
                    }

                    return this.BadRequest(new Dictionary<string, string> { });
                }

                return this.Ok(result.Value);
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerValidationResource.An_Error_Has_Occured), ServerValidationResource.An_Error_Has_Occured } });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Person person = await this.personBusiness.Delete(id);
                if (person != null)
                {
                    return this.Ok(person);
                }

                return this.BadRequest(new Dictionary<string, string> { });
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerValidationResource.An_Error_Has_Occured), ServerValidationResource.An_Error_Has_Occured } });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Person person = await this.personBusiness.Get(id);

                if (person != null)
                {
                    return this.Ok(person);
                }

                return this.NotFound();
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerValidationResource.An_Error_Has_Occured), ServerValidationResource.An_Error_Has_Occured } });
            }
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                return this.Ok(await this.personBusiness.List());
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerValidationResource.An_Error_Has_Occured), ServerValidationResource.An_Error_Has_Occured } });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Person personToUpdate)
        {
            try
            {
                if (personToUpdate != null)
                {
                    personToUpdate.Id = id;
                    KeyValuePair<bool, Person> result = await this.personBusiness.CreateOrUpdate(personToUpdate);
                    if (!result.Key)
                    {
                        if (result.Value != null)
                        {
                            return this.BadRequest(result.Value.ValidationService.ModelState);
                        }
                    }

                    return this.Ok(result.Value);
                }

                return this.BadRequest(new Dictionary<string, string> { });
            }
            catch
            {
                return this.BadRequest(new Dictionary<string, string> { { nameof(ServerValidationResource.An_Error_Has_Occured), ServerValidationResource.An_Error_Has_Occured } });
            }
        }
    }
}