namespace Template.DataAccess.Seed
{
    using System.Threading.Tasks;

    public interface IContextSeed
    {
        TemplateContext Context { get; set; }

        Task Execute(bool isProduction);
    }
}