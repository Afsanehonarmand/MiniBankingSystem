namespace MiniBankingSystem.App.Demo
{
    using global::MiniBankingSystem.Application.Services;

    namespace MiniBankingSystem.ConsoleApp.Demo;

    public class BankingDemoExecutor
    {
        private readonly BankingService _bankingService;

        public BankingDemoExecutor(BankingService bankingService)
        {
            _bankingService = bankingService;
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("💳 Mini Banking System - DEMO");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Show Transactions");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

                var option = Console.ReadLine();

                try
                {
                    switch (option)
                    {
                        case "1":
                            CreateAccount();
                            break;
                        case "2":
                            Deposit();
                            break;
                        case "3":
                            Withdraw();
                            break;
                        case "4":
                            Transfer();
                            break;
                        case "5":
                            ShowTransactions();
                            break;
                        case "6":
                            running = false;
                            break;
                        default:
                            Console.WriteLine("❌ Invalid option.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❗ Error: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private void CreateAccount()
        {
            Console.Write("Owner name: ");
            var owner = Console.ReadLine();
            Console.Write("Initial balance: ");
            var balance = decimal.Parse(Console.ReadLine() ?? "0");
            var account = _bankingService.CreateAccount(owner!, balance);
            Console.WriteLine($"✅ Account created. ID: {account.Id}");
        }

        private void Deposit()
        {
            var id = ReadGuid("Account ID: ");
            var amount = ReadDecimal("Amount: ");
            _bankingService.Deposit(id, amount);
            Console.WriteLine("✅ Deposit successful.");
        }

        private void Withdraw()
        {
            var id = ReadGuid("Account ID: ");
            var amount = ReadDecimal("Amount: ");
            _bankingService.Withdraw(id, amount);
            Console.WriteLine("✅ Withdrawal successful.");
        }

        private void Transfer()
        {
            var fromId = ReadGuid("From Account ID: ");
            var toId = ReadGuid("To Account ID: ");
            var amount = ReadDecimal("Amount: ");
            _bankingService.Transfer(fromId, toId, amount);
            Console.WriteLine("✅ Transfer successful.");
        }

        private void ShowTransactions()
        {
            var id = ReadGuid("Account ID: ");
            var transactions = _bankingService.GetAccountStatement(id);

            if (!transactions.Any())
            {
                Console.WriteLine("📭 No transactions found.");
                return;
            }

            Console.WriteLine("📜 Transaction History:");
            foreach (var t in transactions)
            {
                Console.WriteLine($"{t.Date:u} | {t.Type} | Amount: {t.Amount} | Target: {t.TargetAccountId}");
            }
        }

        private Guid ReadGuid(string prompt)
        {
            Console.Write(prompt);
            return Guid.TryParse(Console.ReadLine(), out var result) ? result : throw new FormatException("Invalid GUID format.");
        }

        private decimal ReadDecimal(string prompt)
        {
            Console.Write(prompt);
            return decimal.TryParse(Console.ReadLine(), out var result) ? result : throw new FormatException("Invalid decimal format.");
        }
    }

}
