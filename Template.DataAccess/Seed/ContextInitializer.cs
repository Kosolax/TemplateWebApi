namespace Template.DataAccess.Seed
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ContextInitializer
    {
        public async Task Seed(TemplateContext context, bool isProduction)
        {
            List<IContextSeed> listSeed = new List<IContextSeed>
            {
                new PersonSeed(context),
            };

            foreach (IContextSeed contextSeed in listSeed)
            {
                await contextSeed.Execute(isProduction);
            }
        }
    }
}