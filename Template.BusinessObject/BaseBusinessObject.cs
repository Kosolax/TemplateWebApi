namespace Template.BusinessObject
{
    using Template.BusinessObject.Validation.Service;
    using Template.Entities;

    public class BaseBusinessObject<T>
        where T : IBaseEntity
    {
        public BaseBusinessObject()
        {
        }

        public BaseBusinessObject(IBaseEntity entity)
        {
        }

        public ValidationService<T> ValidationService { get; set; }

        public virtual T CreateEntity()
        {
            return default;
        }
    }
}