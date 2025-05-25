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
        var account = _bankingService.CreateAccount(request.Owner, request.InitialBalance);
        return Ok(account);
    }

    [HttpPost("{id}/deposit")]
    public IActionResult Deposit(Guid id, [FromBody] AmountRequest request)
    {
        _bankingService.Deposit(id, request.Amount);
        return Ok("✅ Deposit successful");
    }

    [HttpPost("{id}/withdraw")]
    public IActionResult Withdraw(Guid id, [FromBody] AmountRequest request)
    {
        _bankingService.Withdraw(id, request.Amount);
        return Ok("✅ Withdrawal successful");
    }

    [HttpPost("transfer")]
    public IActionResult Transfer([FromBody] TransferRequest request)
    {
        _bankingService.Transfer(request.FromAccountId, request.ToAccountId, request.Amount);
        return Ok("✅ Transfer successful");
    }

    [HttpGet("{id}/transactions")]
    public IActionResult GetTransactions(Guid id)
    {
        var transactions = _bankingService.GetAccountStatement(id);
        return Ok(transactions);
    }
}
