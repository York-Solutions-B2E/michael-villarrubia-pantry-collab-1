using michael_villarrubia_pantry_collab_BE.DTOs;

namespace michael_villarrubia_pantry_collab_BE.Services.UserService
{
    public class UserService: IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Login(UserDTO userRequest)
        {
            var myUser = await _context.Users.FirstOrDefaultAsync(x => x.Username == userRequest.Username);
            if (myUser == null)
            {
                throw new Exception("User not found");
            }

            if (myUser.Password != userRequest.Password)
            {
                throw new Exception("Invalid Password");
            }

            return myUser;
        }

        public async Task<User> Register(UserDTO userRequest)
        {
            var userExists = await _context.Users.FirstOrDefaultAsync(x => x.Username == userRequest.Username);
            if(userExists == null)
            {
                var user = new User
                {
                    Username = userRequest.Username,
                    Password = userRequest.Password,
                    FamilyId = null,

                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            throw new Exception("Username Taken");
        }
    }
}
