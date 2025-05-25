namespace MiniBankingSystem.App.Models.Requests
{
    public class CreateAccountRequest
    {
        public string Owner { get; set; } = "";
        public decimal InitialBalance { get; set; }
    }
}
