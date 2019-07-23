namespace App.Services
{
    public class PaymentService
    {

        private readonly AppDbContext db;
        private readonly MultiapprovalService multiapproval;
        private readonly BaseCurrencyProvider baseCurrencyProvider;


        public PaymentService(AppDbContext db, MultiapprovalService multiapproval, BaseCurrencyProvider baseCurrencyProvider)
        {
            this.db = db;
            this.multiapproval = multiapproval;
            this.baseCurrencyProvider = baseCurrencyProvider;
        }

        public Payment Create(double amount, string description)
        {
            if(! this.multiapproval.CanCreate<Payment>())
            {
                throw new InvalidOperationException("You can't create a payment");
            }

            var payment = new Payment{
                Amount = amount,
                Desctiption = description,
                Currency = baseCurrencyProvider.Get(),
            };

            db.Payments.Add(payment);

            this.multiapproval.QueueForApproval(payment);

            db.SaveChanges();

            return Payment;
        }

    }
}