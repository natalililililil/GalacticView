using Entities.Models;
using Shared.DataTransferObjects;

namespace Contracts
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAllNewsAsync(bool trackChanges);
        Task<News> GetNewsAsync(Guid newsId, bool trackChanges);
        void CreateNews(News news);
        Task<IEnumerable<News>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteNews(News news);
    }
}
