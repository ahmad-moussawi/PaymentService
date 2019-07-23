namespace App.Services
{
    public class PaymentService
    {

        private readonly AppDbContext db;
        public PaymentService(AppDbContext db)
        {
            this.db = db;
        }

        public Payment Create(double amount, string description)
        {
            var payment = new Payment{
                Amount = amount,
                Currency = description
            };

            db.Payments.Add(payment);

            db.SaveChanges();

            return Payment;
        }

    }
}