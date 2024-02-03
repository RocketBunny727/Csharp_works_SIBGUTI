using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main
{

    public interface INotifyer
    {
        void Notify(decimal balance);
    }

    class Account
    {
        private decimal _balance;
        private List<INotifyer> _notifyers;

        public Account(decimal balance)
        {
            _balance = balance;
            _notifyers = new List<INotifyer>();
        }

        public void AddNotifyer(INotifyer _notifyer)
        {
            _notifyers.Add(_notifyer);
        }

        public void ChangeBalance(decimal value)
        {
            _balance = value;
            Notification();
        }

        private void Notification()
        {
            foreach (var notifyer in _notifyers)
            {
                notifyer.Notify(_balance);
            }
        }

        public decimal Balance
        {
            get
            {
                return _balance;
            }
        }
    }

    class SMSLowBalanceNotifyer : INotifyer
    {
        private string _phone;
        private decimal _lowBalanceValue;

        public SMSLowBalanceNotifyer(string phone, decimal lowBalanceValue)
        {
            _phone = phone;
            _lowBalanceValue = lowBalanceValue;
        }

        public void Notify(decimal balance)
        {
            if(balance < _lowBalanceValue)
            {
                Console.WriteLine("- SMSLowBalanceNotifyer Message -");
                Console.WriteLine("ATTENTION!!! Your balance is low");
                Console.WriteLine($"Balance: {balance}");
            }
        }
    }

    class EMailBalaneChangedNotifyer : INotifyer
    {
        private string _email;

        public EMailBalaneChangedNotifyer(string email)
        {
            _email = email;
        }

        public void Notify(decimal balance)
        {
            Console.WriteLine("- EMailBalaneChangedNotifyer Message -");
            Console.WriteLine("ATTENTION!!! Your balance is changed");
            Console.WriteLine($"Balance: {balance}");
        }
    }

    internal class Program
    {

        static void Main()
        {
            Console.Write("Enter user`s balance: ");
            decimal balance = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Enter phone number for SMS notification: ");
            string phone = Console.ReadLine();

            Console.Write("Enter low balance value for SMS notification: ");
            decimal lowBalanceValue = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Enter email address for email notification: ");
            string email = Console.ReadLine();

            Account user = new Account(balance);

            user.AddNotifyer(new SMSLowBalanceNotifyer(phone, lowBalanceValue));
            user.AddNotifyer(new EMailBalaneChangedNotifyer(email));

            Console.WriteLine("Enter new balance: ");
            balance = Convert.ToDecimal(Console.ReadLine());

            user.ChangeBalance(balance);
        }
    }
}
