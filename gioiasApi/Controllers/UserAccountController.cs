﻿using gioiasApi.Interfaces;
using gioiasApi.Models;
using gioiasApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Razor.Hosting;

namespace gioiasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountController : Controller
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountController(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAccount>>> GetUserAccounts()
        {
            return Ok(await _userAccountRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var userAccount = await _userAccountRepository.GetById(id);

            if (userAccount == null)
            {
                UserAccount nullUserAccount = new UserAccount();
                return NotFound(nullUserAccount);
            }

            return Ok(userAccount);
        }

        [HttpGet("query/{name}")]
        public async Task<ActionResult> GetByAproximatedName(string name)
        {
            var userAccount = await _userAccountRepository.GetByAproximatedName(name);

            if (userAccount == null)
            {
                UserAccount nullUserAccount = new UserAccount();
                return NotFound(nullUserAccount);
            }

            return Ok(userAccount);
        }

        [HttpPost]
        public async Task<ActionResult> AddUserAccount(UserAccount userAccount)
        {
            _userAccountRepository.Create(userAccount);
            if(await _userAccountRepository.SaveAllAsync())
            {
                return Ok(userAccount);
            }

            return BadRequest("Error at create a new student.");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUserAccount(UserAccount userAccount)
        {
            _userAccountRepository.Edit(userAccount);
            if(await _userAccountRepository.SaveAllAsync())
            {
                return Ok(userAccount);
            }

            return BadRequest("Error in operation.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAccount(int id)
        {
            var userAccount = await _userAccountRepository.GetById(id);

            if(userAccount == null)
            {
                return NotFound("Student not found.");
            }

            _userAccountRepository.Delete(userAccount);

            if(await _userAccountRepository.SaveAllAsync())
            {
                return Ok(userAccount);
            }

            return BadRequest("Error in operation.");
        }        
    }
}
