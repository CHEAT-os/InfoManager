using API.Models.DTOs.UserDTO;
using API.Models.Entity;

namespace API.Repository.IRepository
{
    public interface IUserRepository
    {
        ICollection<AppUser> GetUsers();
        AppUser GetUser(string id);
        bool IsUniqueUser(string userName);
        Task<UserLoginResponseDTO> Login(UserLoginDTO userLoginDTO);
        Task<UserLoginResponseDTO> Register(UserRegistrationDTO userRegistrationDTO);
    }
}
