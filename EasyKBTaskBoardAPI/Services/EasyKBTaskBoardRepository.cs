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
            _context.Accounts.Add(account);
        }

        public void UpdateAccount(Account account)
        {

        }

        public void DeleteAccount(Account account)
        {
            _context.Accounts.Remove(account);
        }

        public void AddBoardForAccount(int accountId, Board board)
        {
            var account = GetAccount(accountId);
            account.Boards.Add(board);
        }

        public void UpdateBoardForAccount(int accountId, Board board)
        {

        }

        public void DeleteBoard(Board board)
        {
            _context.Boards.Remove(board);
        }

        public void AddColumnToBoard(int boardId, Column column)
        {
            var board = _context.Boards.Where(c => c.Id == boardId).FirstOrDefault();
            board.Columns.Add(column);
        }

        public void UpdateColumnToBoard(int boardId, Column column)
        {
           
        }

        public void AddTaskToColumn(int columnId, Entities.Task task)
        {
            var column = _context.Columns.Where(c => c.Id == columnId).FirstOrDefault();
            column.Tasks.Add(task);
        }

        public void DeleteTask(Entities.Task task)
        {
            _context.Tasks.Remove(task);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}
