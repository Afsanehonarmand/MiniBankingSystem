// See https://aka.ms/new-console-template for more information
using MiniBankingSystem.Application.Services;
using MiniBankingSystem.ConsoleApp.Demo;
using MiniBankingSystem.Infrastructure.Repositories;

//Console.WriteLine("Hello, World!");

var service = new BankingService(new InMemoryAccountRepository());
var demo = new BankingDemoExecutor(service);
await demo.RunAsync();
