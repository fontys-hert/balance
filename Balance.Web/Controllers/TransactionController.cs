using Balance.DataAccess;
using Balance.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Balance.Web.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionRepository _repository;

        public TransactionController()
        {
            _repository = new TransactionRepository();
        }

        public IActionResult Index()
        {
            TransactionListViewModel viewModel = new TransactionListViewModel();

            var transactions = _repository.GetAll();

            foreach (var dto in transactions)
            {
                viewModel.Transactions.Add(new TransactionListItemViewModel
                {
                    Amount = dto.Amount,
                    CreatedAt = dto.CreatedAt,
                    Name = dto.Name
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

            _repository.Insert(new DataAccess.Dto.TransactionAddDto
            {
                Name = name,
                Amount = amount.Value
            });

            return RedirectToAction("Index");
        }
    }
}
