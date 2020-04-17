using EasyKBTaskBoard.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKBTaskBoard.API.Services
{
    public interface IEasyKBTaskBoardRepository
    {
        IEnumerable<Account> GetAccounts()
        Account GetAccount(int userId);
        bool Save();
    }
}
