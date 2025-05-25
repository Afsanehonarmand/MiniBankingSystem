// See https://aka.ms/new-console-template for more information
using MiniBankingSystem.Application.Services;
using MiniBankingSystem.Infrastructure.Repositories;

Console.WriteLine("Hello, World!");

var accountRepository = new InMemoryAccountRepository();
var bankingService = new BankingService(accountRepository);
var demo = new BankingDemoExecutor(bankingService);

demo.Run();
