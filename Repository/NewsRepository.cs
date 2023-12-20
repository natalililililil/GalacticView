using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class NewsRepository : RepositoryBase<News>, INewsRepository
    {
        public NewsRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<IEnumerable<News>> GetAllNewsAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();

        public async Task<News> GetNewsAsync(Guid newsId, bool trackChanges) =>
            await FindByCondition(с => с.Id.Equals(newsId), trackChanges).SingleOrDefaultAsync();

        public async Task<IEnumerable<News>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        public void CreateNews(News news) => Create(news);

        public void DeleteNews(News news) => Delete(news);
    }
}
