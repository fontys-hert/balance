namespace Balance.DataAccess.Dto
{
    public class TransactionDto
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
