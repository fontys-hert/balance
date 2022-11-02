using Balance.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Balance.Web.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            string filePath = "transactions.csv";
            TransactionListViewModel viewModel = new TransactionListViewModel();

            if (!System.IO.File.Exists(filePath))
            {
                return View(viewModel);
            }

            IEnumerable<string> rows = System.IO.File.ReadAllText("transactions.csv").Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string row in rows)
            {
                IEnumerable<string> rowValues = row.Split('\t', StringSplitOptions.RemoveEmptyEntries).ToList();
                viewModel.Transactions.Add(new TransactionListItemViewModel
                {
                    Name = rowValues.ElementAt(0),
                    Amount = Convert.ToInt32(rowValues.ElementAt(1)),
                    CreatedAt = Convert.ToDateTime(rowValues.ElementAt(2)),
                });
            }

            return View(viewModel);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(TransactionAddViewModel addViewModel)
        {
            string name = addViewModel.Name;
            int? amount = addViewModel.Amount;

            if (string.IsNullOrEmpty(name) || amount == null)
            {
                return View();
            }

            DateTime createdAt = DateTime.Now;
            DateTime updatedAt = DateTime.Now;
            List<string> row = new List<string>
            {
                name,
                amount.Value.ToString(),
                createdAt.ToString(),
                updatedAt.ToString()
            };

            using StreamWriter writer = new StreamWriter("transactions.csv", true);
            writer.WriteLine(string.Join('\t', row));

            return RedirectToAction("Index");
        }
    }
}
