using AutoMapper;
using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class NewsService : INewsService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public NewsService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<NewsDto> CreateNewsAsync(NewsForCreationDto news)
        {
            var newsEntity = _mapper.Map<News>(news);

            _repository.News.CreateNews(newsEntity);
            await _repository.SaveAsync();

            var newsToReturn = _mapper.Map<NewsDto>(newsEntity);
            return newsToReturn;
        }

        public Task DeleteNewsAsync(Guid newsId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<NewsDto>> GetAllNewsAsync(bool trackChanges)
        {
            var allNews = await _repository.News.GetAllNewsAsync(trackChanges);

            var allNewsDto = _mapper.Map<IEnumerable<NewsDto>>(allNews);
            return allNewsDto;
        }

        public Task<IEnumerable<NewsDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<NewsDto> GetNewsAsync(Guid newsId, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
