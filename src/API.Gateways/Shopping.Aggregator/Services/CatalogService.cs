using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models.Entities;

namespace Shopping.Aggregator.Services {
    public class CatalogService : ICatalogService {
        private readonly HttpClient client;

        public CatalogService(HttpClient client) {
            this.client = client;
        }
        public async Task<IEnumerable<CatalogDTO>> GetCatalog() {
            var catalogs = await client.GetAsync("/api/v1/catalog");
            return await catalogs.ReadContentAs<List<CatalogDTO>>();
        }

        public async Task<CatalogDTO> GetCatalog(string id) {
            var catalog = await client.GetAsync($"/api/v1/catalog/{id}");
            return await catalog.ReadContentAs<CatalogDTO>();
        }

        public async Task<IEnumerable<CatalogDTO>> GetCatalogByCategory(string category) {
            var catalogs = await client.GetAsync($"/api/v1/catalog/GetProductsByCategory/{category}");
            return await catalogs.ReadContentAs<List<CatalogDTO>>();
        }
    }
}
