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
        void AddAccount(Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(Account account);
        Board GetBoard(int boardId);
        IEnumerable<Board> GetBoardsForAccount(int accountId);
        void AddBoard(Board board);
        void UpdateBoard(Board board);
        void DeleteBoard(Board board);
        IEnumerable<Column> GetColumnsForBoard(int boardId);
        Column GetColumnForBoard(int boardId, int columnId);
        void AddColumnToBoard(int boardId, Column column);
        void UpdateColumnForBoard(int boardId, Column column);
        void DeleteColumn(Column column);
        Entities.Task GetTask(int taskId);
        void AddTask(Entities.Task task);
        void UpdateTask(Entities.Task task);
        void DeleteTask(Entities.Task task);
        void AddAccountToTask(int accountId, int taskId);
        bool Save();
    }
}
