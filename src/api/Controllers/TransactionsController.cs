using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

using api.Helpers;
using api.Interfaces;
using api.Mappers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly IOutletRepository _outletRepository;
        private readonly IUserService _userService;
        public TransactionsController(
            ITransactionsRepository transactionsRepository,
            IOutletRepository outletRepository,
            IUserService userService
            )
        {
            _transactionsRepository = transactionsRepository;
            _outletRepository = outletRepository;
            _userService = userService;
        }

        [HttpGet]
        // [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transactions = await _transactionsRepository.GetAllAsync(query);

            var transactionsDto = transactions.Select(t => t.ToTransactionDto()).ToList();

            return Ok(transactionsDto);
        }

    }
}