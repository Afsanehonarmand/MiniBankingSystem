using Microsoft.AspNetCore.Mvc;
using MiniBankingSystem.App.Models.Requests;
using MiniBankingSystem.Application.Services;

namespace MiniBankingSystem.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly BankingService _bankingService;

    public AccountController(BankingService bankingService)
    {
        _bankingService = bankingService;
    }

    [HttpPost]
    public IActionResult CreateAccount([FromBody] CreateAccountRequest request)
    {
        try
        {
            var account = _bankingService.CreateAccount(request.Owner, request.InitialBalance);
            return Ok(account);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/deposit")]
    public IActionResult Deposit(Guid id, [FromBody] AmountRequest request)
    {
        try
        {
            _bankingService.Deposit(id, request.Amount);
            return Ok("Deposit successful");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/withdraw")]
    public IActionResult Withdraw(Guid id, [FromBody] AmountRequest request)
    {
        try
        {
            _bankingService.Withdraw(id, request.Amount);
            return Ok("Withdrawal successful");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("transfer")]
    public IActionResult Transfer([FromBody] TransferRequest request)
    {
        try
        {
            _bankingService.Transfer(request.FromAccountId, request.ToAccountId, request.Amount);
            return Ok("Transfer successful");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}/transactions")]
    public IActionResult GetTransactions(Guid id)
    {
        try
        {
            var transactions = _bankingService.GetAccountStatement(id);
            return Ok(transactions);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
