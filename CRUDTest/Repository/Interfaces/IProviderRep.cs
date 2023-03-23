using CRUDTest.Models;

namespace CRUDTest.Interfaces
{
    public interface IProviderRep
    {
        public ProviderDB GetProviderById(int id);
        public IEnumerable<ProviderDB> GetProviders { get; }
        public List<int> GetProvidersIdByNames(List<string> providerNames);
    }
}
