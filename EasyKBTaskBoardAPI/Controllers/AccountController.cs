using AutoMapper;
using EasyKBTaskBoard.API.Entities;
using EasyKBTaskBoard.API.Models;
using EasyKBTaskBoard.API.Services;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{accountId}", Name = "GetAccount")]
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

        [HttpPost]
        public ActionResult CreateAccount([FromBody] AccountForCreationDto account)
        {
            var accountEntity = _mapper.Map<Account>(account);

            _easyKBTaskBoardRepository.AddAccount(accountEntity);
            _easyKBTaskBoardRepository.Save();

            var accountToReturn = _mapper.Map<AccountDto>(accountEntity);

            return CreatedAtRoute(
                "GetAccount",
                new { accountId = accountToReturn.Id },
                accountToReturn);
        }

        [HttpPatch("{accountId}")]
        public ActionResult PartiallyUpdateAccount(int accountId, [FromBody] JsonPatchDocument<AccountForUpdateDto> patchDoc)
        {
            var accountEntity = _easyKBTaskBoardRepository.GetAccount(accountId);

            if (accountEntity == null)
            {
                return NotFound();
            }

            var accountToPatch = _mapper.Map<AccountForUpdateDto>(accountEntity);

            patchDoc.ApplyTo(accountToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(accountToPatch, accountEntity);

            _easyKBTaskBoardRepository.UpdateAccount(accountEntity);
            _easyKBTaskBoardRepository.Save();

            return NoContent();
        }

        [HttpDelete("{accountId}")]
        public ActionResult DeleteAccount(int accountId)
        {
            var accountToDelete = _easyKBTaskBoardRepository.GetAccount(accountId);

            if (accountToDelete == null)
            {
                return NotFound();
            }

            _easyKBTaskBoardRepository.DeleteAccount(accountToDelete);
            _easyKBTaskBoardRepository.Save();

            return NoContent();
        }
    }
}
