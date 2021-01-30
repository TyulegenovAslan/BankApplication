using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{

    public class AccountEventArgs
    {
        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; private set; }
        /// <summary>
        /// Сумма, на которую изменился счет
        /// </summary>
        public double Sum { get; private set; }

        public AccountEventArgs(string message, double sum)
        {
            Message = message;
            Sum = sum;
        }
    }

}
