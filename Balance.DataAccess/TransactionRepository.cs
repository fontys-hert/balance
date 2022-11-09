using Balance.DataAccess.Dto;

namespace Balance.DataAccess
{
    public class TransactionRepository
    {
        private readonly string filePath = "transactions.csv";
        private readonly char separator = '\t';

        public void Insert(TransactionAddDto transaction)
        {
            DateTime createdAt = DateTime.Now;
            DateTime updatedAt = DateTime.Now;
            List<string> row = new List<string>
            {
                transaction.Name,
                transaction.Amount.ToString(),
                createdAt.ToString(),
                updatedAt.ToString()
            };

            using StreamWriter writer = new StreamWriter(filePath, true);
            writer.WriteLine(string.Join('\t', row));
        }

        public List<TransactionDto> GetAll()
        {
            List<TransactionDto> transactions = new List<TransactionDto>();

            if (!File.Exists(filePath))
            {
                return new();
            }

            IEnumerable<string> rows = File.ReadAllText(filePath).Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string row in rows)
            {
                IEnumerable<string> rowValues = row.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
                transactions.Add(new TransactionDto
                {
                    Name = rowValues.ElementAt(0),
                    Amount = Convert.ToInt32(rowValues.ElementAt(1)),
                    CreatedAt = Convert.ToDateTime(rowValues.ElementAt(2)),
                });
            }

            return transactions;
        }
    }
}