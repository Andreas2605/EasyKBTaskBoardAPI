using EasyKBTaskBoard.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKBTaskBoard.API.Services
{
    public interface IEasyKBTaskBoardRepository
    {
        IEnumerable<Account> GetAccounts();
        Account GetAccount(int accountId);
        bool AccountExists(int accountId);
        Board GetBoardForAccount(int accountId, int boardId);
        IEnumerable<Board> GetBoardsForAccount(int accountId);
        void AddAccount(Account account);
        void DeleteAccount(int accountId);
        void AddBoardForAccount(int accountId, Board board);
        void DeleteBoard(Board board);
        void AddColumnToBoard(Column column);
        void UpdateColumnForBoard(int boardId, Column column);
        void AddTaskToBoard(int boardId, Entities.Task task);
        void DeleteTask(Entities.Task task);
        bool Save();
    }
}
