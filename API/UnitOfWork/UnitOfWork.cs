using System.Collections;
using API.Data;
using API.Entities;
using API.Interfaces;
using API.Repositores;
using API.SignalR;
using API.SignalR.HubServices;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;



namespace API.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IHubContext<MessageHub> _messageHub;
        private Hashtable _repositories;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        
        public UnitOfWork(DataContext context, IMapper mapper, IHostEnvironment hostEnvironment,
        IHubContext<MessageHub> messageHub)
        {
            _hostEnvironment = hostEnvironment;
            _messageHub = messageHub;            
            _mapper = mapper;
            _context = context;
        }

        public IMapper Mapper => _mapper;

        public IFileRepository FileRepository => new FileRepository(_hostEnvironment);

        //IFileRepository IUnitOfWork.FileRepository => throw new NotImplementedException();

        public IUserRepository UserRepository => new UserRepository(_context, _mapper);

        public ILikesRepository LikesRepository => new LikesRepository(_context);

        public IMessageRepository MessageRepository => new MessageRepository(_context, _mapper);

        public IHubService HubService => new HubService(_messageHub, _mapper);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator
                .CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context, _mapper);

                _repositories.Add(type, repositoryInstance);
            }

            return (Repository<TEntity>)_repositories[type];
        }

        IRepository<TEntity> IUnitOfWork.Repository<TEntity>()
        {
            throw new NotImplementedException();
        }
    }
}