using API.Entities;
using API.Interfaces;
using AutoMapper;


namespace API.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<bool> SaveAsync();
        bool HasChanges();
        IMapper Mapper { get; }

        IFileRepository FileRepository { get; }

        IUserRepository UserRepository { get; }
        ILikesRepository LikesRepository { get; }
        IMessageRepository MessageRepository { get; } 


    }
}