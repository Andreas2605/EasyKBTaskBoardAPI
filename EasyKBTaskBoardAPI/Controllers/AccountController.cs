using AutoMapper;
using EasyKBTaskBoard.API.Models;
using EasyKBTaskBoard.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKBTaskBoard.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IEasyKBTaskBoardRepository _easyKBTaskBoardRepository;
        private readonly IMapper _mapper;

        public AccountController(IEasyKBTaskBoardRepository easyKBTaskBoardRepository, IMapper mapper)
        {
            _easyKBTaskBoardRepository = easyKBTaskBoardRepository;
            _mapper = mapper;
        }

        [HttpGet("{accountId}")]
        public ActionResult GetAccount(int accountId)
        {
            var account = _easyKBTaskBoardRepository.GetAccount(accountId);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AccountDto>(account));
        }

        [HttpGet]
        public ActionResult GetAccounts()
        {
            var accountEntities = _easyKBTaskBoardRepository.GetAccounts();

            return Ok(_mapper.Map<IEnumerable<AccountDto>>(accountEntities));
        }
    }
}
