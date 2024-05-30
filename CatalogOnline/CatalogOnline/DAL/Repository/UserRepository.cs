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

        public List<User> GetAllUsers()
        {
            return _context.User.ToList();
        }   

        public List<User> GetAllStudents()
        {
            return _context.User.Where(u => u.role == "student").ToList();
        }

        public async Task<bool> AddUser(User user)
        {
            _context.User.Add(user);
            var res = await _context.SaveChangesAsync();
            return res > 0;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var existingUser = _context.User.Local.FirstOrDefault(u => u.user_id == user.user_id);
            if (existingUser != null)
            {
                _context.Entry(existingUser).State = EntityState.Detached;
            }

            _context.User.Update(user);
            return await _context.SaveChangesAsync() > 0;
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

        public async Task<bool> UserExists(int userId)
        {
            return await _context.User.AnyAsync(u => u.user_id == userId);
        }

    }
}
