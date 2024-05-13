using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;

namespace CatalogOnline.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public User GetUserByName(string name)
        {
            
            return _context.User.FirstOrDefault(p => p.legal_name.Trim().ToUpper() == name.Trim().ToUpper());
        
        }

        public List<User> GetUsers()
        {
            return _context.User.OrderBy(p => p.user_id).ToList();
        }
    }
}
