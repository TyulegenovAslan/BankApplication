using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public delegate void AccountStateHandler(object sendler, AccountEventArgs e);

    public abstract class Account : IAccount
    {
        /// <summary>
        /// Событие, возникающие при выводе денег
        /// </summary>
        protected internal event AccountStateHandler Withdrawed;
        /// <summary>
        /// Событие, возникающие при добавлении на счет
        /// </summary>
        protected internal event AccountStateHandler Added;
        /// <summary>
        /// Событие, возникающие при открытии счета
        /// </summary>
        protected internal event AccountStateHandler Opened;
        /// <summary>
        /// Событие, возникающие при закрытии счета
        /// </summary>
        protected internal event AccountStateHandler Closed;
        /// <summary>
        /// Событие, возникающие при начислении процента
        /// </summary>
        protected internal event AccountStateHandler Calculated;


        /// <summary>
        /// Время с момента открытия счета
        /// </summary>
        protected int days = 0;
        static int counter = 0;
        /// <summary>
        /// Текущая сумма на счету
        /// </summary>
        public double Sum { get; private set; }
        /// <summary>
        /// Процент начислений
        /// </summary>
        public double Perсentage { get; private set; }
        /// <summary>
        /// Уникальный идентификатор счета
        /// </summary>
        public int Id { get; private set; }

        public Account(double sum, double perсentage)
        {
            Sum = sum;
            Perсentage = perсentage;
            Id = ++counter;
        }

        /// <summary>
        /// Вызон событий
        /// </summary>
        /// <param name="e"></param>
        /// <param name="handler"></param>
        private void CallEvent(AccountEventArgs e, AccountStateHandler handler)
        {
            if (e != null)
            {
                handler?.Invoke(this, e);
            }
        }

        #region Вызов отдельных событий
        /// <summary>
        /// Вызов события при открытии счета
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnOpened(AccountEventArgs e)
        {
            CallEvent(e, Opened);
        }
        /// <summary>
        /// Вызов события при закрытии счета
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnClosed(AccountEventArgs e)
        {
            CallEvent(e, Closed);
        }
        /// <summary>
        /// Вызов события при выводе денег
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnWithdrawed(AccountEventArgs e)
        {
            CallEvent(e, Withdrawed);
        }
        /// <summary>
        /// Вызов события при зачислении денег
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnAdded(AccountEventArgs e)
        {
            CallEvent(e, Added);
        }
        /// <summary>
        /// Вызов события при  начилении процента
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCalculated(AccountEventArgs e)
        {
            CallEvent(e, Calculated);
        }

        #endregion

        /// <summary>
        /// Метод зачиления на счет 
        /// </summary>
        /// <param name="sum"></param>
        public virtual void Put(double sum)
        {
            Sum += sum;
            OnAdded(new AccountEventArgs($"На счет поступило: {sum} ", sum));
        }

        /// <summary>
        ///  Метод снятия со счета, возвращает сколько снято со счета
        /// </summary>
        /// <param name="sum"></param>
        /// <returns></returns>
        public virtual double Withdraw(double sum)
        {
            double result = 0;

            if(Sum >= sum)
            {
                result = sum;
                Sum -= sum;
                OnWithdrawed(new AccountEventArgs($"Сумма: {sum} снята со счета {Id}", sum));
            }
            else
            {
                OnWithdrawed(new AccountEventArgs($"Недостаточно средств на счете {Id}", 0));
            }
            return result;
        }

        /// <summary>
        /// Открытие счета
        /// </summary>
        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs($"Открыт новый счет: {Id}", Sum));
        }
        /// <summary>
        /// Закрытие счета
        /// </summary>
        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs($"Счет {Id} закрыт. Итоговая сумма: {Sum}", Sum));
        }
        /// <summary>
        /// Начисление процентов
        /// </summary>
        protected internal void IncrementDays()
        {
            days++;
        }
        /// <summary>
        /// Начисление процентов
        /// </summary>
        protected internal virtual void Calculate()
        {
            double increment = Sum * Perсentage / 100;
            Sum = Sum + increment;
            OnCalculated(new AccountEventArgs($"Начислены проценты в размере {increment}", increment));
        }
    }
}
