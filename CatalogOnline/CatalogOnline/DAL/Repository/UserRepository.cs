using CatalogOnline.DAL.DBO;
using CatalogOnline.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

        public User GetUserById(int id)
        {
            return _context.User.Find(id);
        }

        public void AddUser(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.User.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = _context.User.Find(id);
            if (user != null)
            {
                _context.User.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
