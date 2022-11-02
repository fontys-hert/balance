namespace Balance.Web.Models
{
    public class TransactionListViewModel
    {
        public List<TransactionListItemViewModel> Transactions { get; set; }

        public TransactionListViewModel()
        {
            Transactions = new();
        }
    }
}
