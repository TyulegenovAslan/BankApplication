using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public class DepositAccount : Account
    {
        public DepositAccount(double sum, double perсentage) : base(sum, perсentage)
        {

        }

        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs($"открыт новый депозитный счет! Id счета: {this.Id}", this.Sum));
        }

        public override double Withdraw(double sum)
        {
            if (days % 30 == 0)
            {
                return base.Withdraw(sum);
            }
            else
                base.OnWithdrawed(new AccountEventArgs("Вывести средства можно только после 30-ти дневного периода", 0));

            return 0;

        }

        public override void Put(double sum)
        {

            if (days % 30 == 0)
                base.Put(sum);
            else
                base.OnAdded(new AccountEventArgs("На счет можно положить только после 30-ти дневного периода", 0));
        }

        protected internal override void Calculate()
        {
            if (days % 30 == 0)
                base.Calculate();
        }
    }
}
