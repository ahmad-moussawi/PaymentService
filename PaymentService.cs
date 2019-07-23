namespace App.Services
{
    public class PaymentService
    {

        private readonly AppDbContext db;
        private readonly BaseCurrencyProvider baseCurrencyProvider;
        public PaymentService(AppDbContext db, BaseCurrencyProvider baseCurrencyProvider)
        {
            this.db = db;
            this.baseCurrencyProvider = baseCurrencyProvider;
        }

        public Payment Create(double amount, string description)
        {
            var payment = new Payment{
                Amount = amount,
                Desctiption = description,
                Currency = baseCurrencyProvider.Get(),
            };

            db.Payments.Add(payment);

            db.SaveChanges();

            return Payment;
        }

    }
}