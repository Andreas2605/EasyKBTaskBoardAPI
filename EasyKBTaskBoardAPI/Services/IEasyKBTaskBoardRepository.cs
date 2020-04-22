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
        void UpdateAccount(Account account);
        void DeleteAccount(Account account);
        void AddBoardForAccount(int accountId, Board board);
        void UpdateBoardForAccount(int accountId, Board board);
        void DeleteBoard(Board board);
        void AddColumnToBoard(int boardId, Column column);
        void UpdateColumnToBoard(int boardId, Column column);
        void AddTaskToColumn(int columnId, Entities.Task task);
        void DeleteTask(Entities.Task task);
        bool Save();
    }
}
