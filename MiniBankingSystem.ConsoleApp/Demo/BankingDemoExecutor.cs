using MiniBankingSystem.Application.Interfaces;

namespace MiniBankingSystem.ConsoleApp.Demo
{
    // This class is only used to demonstrate system functionality from the console.
    public class BankingDemoExecutor
    {
        private readonly IBankingService _bankingService;

        public BankingDemoExecutor(IBankingService bankingService)
        {
            _bankingService = bankingService;
        }

        public async Task RunAsync()
        {
            Console.WriteLine("Running Mini Banking System Demo...\n");

            var alice = await _bankingService.CreateAccountAsync("Alice", 1000);
            var bob = await _bankingService.CreateAccountAsync("Bob", 500);
            var charlie = await _bankingService.CreateAccountAsync("Charlie", 300);

            Console.WriteLine($"Created account for Alice: {alice.Id} (Balance: {alice.Balance})");
            Console.WriteLine($"Created account for Bob: {bob.Id} (Balance: {bob.Balance})");
            Console.WriteLine($"Created account for Charlie: {charlie.Id} (Balance: {charlie.Balance})");

            Console.WriteLine("\nPerforming operations...");

            await _bankingService.DepositAsync(alice.Id, 200);       // Alice gets +200
            await _bankingService.WithdrawAsync(bob.Id, 100);        // Bob -100
            await _bankingService.TransferAsync(alice.Id, charlie.Id, 150); // Alice -> Charlie

            Console.WriteLine("\nTransactions complete.\n");


            await ShowTransactionsAsync("Alice", alice.Id);
            await ShowTransactionsAsync("Bob", bob.Id);
            await ShowTransactionsAsync("Charlie", charlie.Id);

            Console.WriteLine("Demo finished. Press any key to exit...");
            Console.ReadKey();
        }

        private async Task ShowTransactionsAsync(string name, Guid accountId)
        {
            var transactions = await _bankingService.GetAccountStatementAsync(accountId);

            Console.WriteLine($"\n{name}'s Transactions:");
            if (!transactions.Any())
            {
                Console.WriteLine("No transactions.");
                return;
            }

            foreach (var t in transactions)
            {
                Console.WriteLine($"{t.Date:u} | {t.Type} | Amount: {t.Amount} | Target: {t.TargetAccountId}");
            }
        }
    }
}
