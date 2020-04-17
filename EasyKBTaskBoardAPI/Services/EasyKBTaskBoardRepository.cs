using EasyKBTaskBoard.API.Contexts;
using EasyKBTaskBoard.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKBTaskBoard.API.Services
{
    public class EasyKBTaskBoardRepository : IEasyKBTaskBoardRepository
    {
        private readonly EasyKBTaskBoardContext _context;

        public EasyKBTaskBoardRepository(EasyKBTaskBoardContext context)
        {
            _context = context;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _context.Accounts.OrderBy(c => c.FirstName).ToList();
        }

        public Account GetAccount(int userId)
        {
            return _context.Accounts.Where(c => c.Id == userId).FirstOrDefault();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
