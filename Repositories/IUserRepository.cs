using Microsoft.EntityFrameworkCore;
using BookingApp.Data;
using BookingApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingApp.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllParticipantsAsync();
    }
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllParticipantsAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}