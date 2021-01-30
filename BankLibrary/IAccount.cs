using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    interface IAccount
    {
        //Положить на счет
        void Put(decimal sum);
        //Списать со счета
        decimal Withdrow(decimal sum);

    }
}
