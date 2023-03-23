using CRUDTest.Interfaces;
using CRUDTest.Models;

namespace CRUDTest.Repository
{
    public class ProviderRepository : IProviderRep
    {
        private ApplicationContext dbcontext;
        public ProviderRepository(ApplicationContext context) 
        {
            dbcontext = context;
        }

        public ProviderDB? GetProviderById(int id) => dbcontext.Provider.FirstOrDefault(p => p.Id == id);

        IEnumerable<ProviderDB> IProviderRep.GetProviders => dbcontext.Provider.ToList();

        public List<int> GetProvidersIdByNames(List<string> providerNames)
        {
            List<int> proidersId = new List<int>();
            foreach (var providerName in providerNames)
            {
                proidersId.Add(dbcontext.Provider.FirstOrDefault(p => p.Name == providerName).Id);
            }
            return proidersId;
        }
    }
}