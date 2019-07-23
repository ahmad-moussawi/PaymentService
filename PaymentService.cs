namespace App.Services
{
    public class PaymentService
    {

        private readonly AppDbContext db;
        public PaymentService(AppDbContext db, MultiapprovalService multiapproval)
        {
            this.db = db;
            this.multiapproval = multiapproval;
        }

        public Payment Create(double amount, string description)
        {
            if(! this.multiapproval.CanCreate<Payment>())
            {
                throw new InvalidOperationException("You can't create a payment");
            }

            var payment = new Payment{
                Amount = amount,
                Currency = description
            };

            db.Payments.Add(payment);

            this.multiapproval.QueueForApproval(payment);
`
            db.SaveChanges();

            return Payment;
        }

    }
}