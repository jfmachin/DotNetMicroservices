using Shopping.Aggregator.Models.Entities;

namespace Shopping.Aggregator.Services {
    public interface ICatalogService {
        Task<IEnumerable<CatalogDTO>> GetCatalog();
        Task<IEnumerable<CatalogDTO>> GetCatalogByCategory(string category);
        Task<CatalogDTO> GetCatalog(string id);
    }
}
