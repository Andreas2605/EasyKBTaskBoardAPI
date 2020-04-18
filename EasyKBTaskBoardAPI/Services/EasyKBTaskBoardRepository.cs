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

        public Account GetAccount(int accountId)
        {
            return _context.Accounts.Where(c => c.Id == accountId).FirstOrDefault();
        }

        public bool AccountExists(int accountId)
        {
            return (_context.Accounts.Any(c => c.Id == accountId));
        }

        public Board GetBoardForAccount(int accountId, int boardId)
        {
            return _context.Boards.Where(p => p.AccountId == accountId && p.Id == boardId).FirstOrDefault();
        }

        public IEnumerable<Board> GetBoardsForAccount(int accountId)
        {
            return _context.Boards.Where(p => p.AccountId == accountId).ToList();
        }

        public void AddAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public void DeleteAccount(int accountId)
        {
            throw new NotImplementedException();
        }

        public void AddBoardForAccount(int accountId, Board board)
        {
            throw new NotImplementedException();
        }

        public void DeleteBoard(Board board)
        {
            throw new NotImplementedException();
        }

        public void AddColumnToBoard(Column column)
        {
            throw new NotImplementedException();
        }

        public void UpdateColumnForBoard(int boardId, Column column)
        {
            throw new NotImplementedException();
        }

        public void AddTaskToBoard(int boardId, Entities.Task task)
        {
            throw new NotImplementedException();
        }

        public void DeleteTask(Entities.Task task)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}
