using BookingApp.Repositories;
using BookingApp.Models;

namespace BookingApp.Services
{
    public interface IParticipantService
    {
        Task<List<User>> GetAllParticipantsAsync();
    }

    public class ParticipantService : IParticipantService
    {
        private readonly IUserRepository _userRepository;

        public ParticipantService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Lấy tất cả participants
        public async Task<List<User>> GetAllParticipantsAsync()
        {
            return await _userRepository.GetAllParticipantsAsync();
        }
    }
}
