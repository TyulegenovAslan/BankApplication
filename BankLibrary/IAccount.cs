using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    interface IAccount
    {
        /// <summary>
        /// Положить на счет
        /// </summary>
        /// <param name="sum"></param>
        void Put(double sum);
        /// <summary>
        /// Списать со счета
        /// </summary>
        /// <param name="sum"></param>
        /// <returns></returns>
        double Withdraw(double sum);

    }
}
