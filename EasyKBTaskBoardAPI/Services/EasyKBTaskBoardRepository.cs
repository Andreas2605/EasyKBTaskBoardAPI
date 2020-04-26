using EasyKBTaskBoard.API.Contexts;
using EasyKBTaskBoard.API.Entities;
using Microsoft.EntityFrameworkCore;
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
            return _context.Accounts.OrderBy(a => a.FirstName).ToList();
        }

        public Account GetAccount(int accountId)
        {
            return _context.Accounts.Include(a => a.Boards).Where(a => a.Id == accountId).FirstOrDefault();
        }

        public bool AccountExists(int accountId)
        {
            return (_context.Accounts.Any(a => a.Id == accountId));
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

        public Board GetBoard(int boardId)
        {
            return _context.Boards.Include(b => b.Account).Include(b => b.Columns).Where(b => b.Id == boardId).FirstOrDefault();
        }

        public IEnumerable<Board> GetBoardsForAccount(int accountId)
        {
            return _context.Boards.Where(a => a.AccountId == accountId);
        }

        public void AddBoard(Board board)
        {
            _context.Boards.Add(board);
        }

        public void UpdateBoard(Board board)
        {

        }

        public void DeleteBoard(Board board)
        {
            _context.Boards.Remove(board);
        }

        public IEnumerable<Column> GetColumnsForBoard(int boardId)
        {
            return _context.Columns.Include(b => b.Tasks).Where(b => b.BoardId == boardId).ToList();
        }

        public Column GetColumnForBoard(int boardId, int columnId)
        {
            return _context.Columns.Include(b => b.Tasks).Where(b => b.BoardId == boardId && b.Id == columnId).FirstOrDefault();
        }

        public void AddColumnToBoard(int boardId, Column column)
        {
            var board = _context.Boards.Where(b => b.Id == boardId).FirstOrDefault();
            board.Columns.Add(column);
        }

        public void UpdateColumnForBoard(int boardId, Column column)
        {
           
        }

        public void DeleteColumn(Column column)
        {
            _context.Columns.Remove(column);
        }

        public Entities.Task GetTask(int taskId)
        {
            return _context.Tasks.Include(t => t.Members).Where(t => t.Id == taskId).FirstOrDefault();
        }

        public void AddTask(Entities.Task task)
        {
            _context.Tasks.Add(task);
        }

        public void UpdateTask(Entities.Task task)
        {

        }

        public void DeleteTask(Entities.Task task)
        {
            _context.Tasks.Remove(task);
        }

        public void AddAccountToTask(int accountId, int taskId)
        {
            var account = GetAccount(accountId);
            var task = GetTask(taskId);
            task.Members.Add(account);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
