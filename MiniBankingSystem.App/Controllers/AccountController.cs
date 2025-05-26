using Microsoft.AspNetCore.Mvc;
using MiniBankingSystem.App.Models.Requests;
using MiniBankingSystem.Application.Interfaces;

namespace MiniBankingSystem.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IBankingService _bankingService;

    public AccountController(IBankingService bankingService)
    {
        _bankingService = bankingService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
    {
        try
        {
            var account = await _bankingService.CreateAccountAsync(request.Owner, request.InitialBalance);
            return Ok(account);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/deposit")]
    public async Task<IActionResult> Deposit(Guid id, [FromBody] AmountRequest request)
    {
        try
        {
            await _bankingService.DepositAsync(id, request.Amount);
            return Ok("Deposit successful");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/withdraw")]
    public async Task<IActionResult> Withdraw(Guid id, [FromBody] AmountRequest request)
    {
        try
        {
            await _bankingService.WithdrawAsync(id, request.Amount);
            return Ok("Withdrawal successful");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
    {
        try
        {
            await _bankingService.TransferAsync(request.FromAccountId, request.ToAccountId, request.Amount);
            return Ok("Transfer successful");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}/transactions")]
    public async Task<IActionResult> GetTransactions(Guid id)
    {
        try
        {
            var transactions = await _bankingService.GetAccountStatementAsync(id);
            return Ok(transactions);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAccounts()
    {
        var accounts = await _bankingService.GetAllAccountsAsync();
        return Ok(accounts);
    }
}
