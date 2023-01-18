using michael_villarrubia_pantry_collab_BE.DTOs;

namespace michael_villarrubia_pantry_collab_BE.Services.UserService
{
    public interface IUserService
    {
        Task<User> Register(UserDTO userRequest);

        Task<User> Login(UserDTO userRequest);
    }
}
