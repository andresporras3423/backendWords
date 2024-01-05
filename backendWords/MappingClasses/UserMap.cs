using backendWords.Models;

namespace backendWords.MappingClasses
{
    public class UserMap
    {
        public string? id { get; set; }
        public string? username { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? password_confirmation { get; set; }

        public User generateUser()
        {
            User user_ = new User();
            user_.Username = username;
            user_.Email = email;
            user_.PasswordDigest = password;
            user_.Id = Convert.ToInt32(id);
            return user_;
        }
    }
}
