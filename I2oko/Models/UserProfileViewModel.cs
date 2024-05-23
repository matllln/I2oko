using System.Collections.Generic;

namespace I2oko.Models
{
    public class UserProfileViewModel
    {
        public UserModel User { get; set; }
        public List<PostModel> UserPosts { get; set; }
    }
}