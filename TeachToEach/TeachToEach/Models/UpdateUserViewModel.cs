using TeachToEach.Domain.ViewModels.User;

namespace TeachToEach.Models
{
    public class UpdateUserViewModel
    {
        public int id { get; set; }

        public UserViewModel userViewModel { get; set; }
    }
}
