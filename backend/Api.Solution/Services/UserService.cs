using Api.Solution.Data;
using Api.Solution.Models;
using Api.Solution.Services.Base;

namespace Api.Solution.Services
{
    public class UserService : CrudService<User>
    {
        public UserService(AppDbContext context) : base(context)
        {
        }
    }
}
