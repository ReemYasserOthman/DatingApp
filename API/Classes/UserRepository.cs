using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Classes
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await context.Users
            .Include(p=> p.Photos)
            .ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
           return await context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
           return await context.Users
           .Include(p=> p.Photos)
           .SingleOrDefaultAsync(u=>u.UserName == username);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            context.Entry(user).State = EntityState.Modified;
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
           return await context.Users
           .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
           .ToListAsync();
            
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
           return await context.Users.Where(u=> u.UserName == username)
           .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
           .FirstOrDefaultAsync();
        }
    }
}