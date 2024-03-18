using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;

namespace Homework_7
{
    public class CollectionChangedEventArgsFactory
    {
        public static IObservable<NotifyCollectionChangedEventArgs> CreateObservable(
            ObservableCollection<object> collection)
        {
            return Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                handler => collection.CollectionChanged += handler,
                handler => collection.CollectionChanged -= handler)
                .Select(eventPattern => eventPattern.EventArgs);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var collection = new ObservableCollection<object>();

            // Подписываемся на события изменения коллекции
            var observable = CollectionChangedEventArgsFactory.CreateObservable(collection);

            // Метод, который будет логировать изменения в файл
            Action<NotifyCollectionChangedEventArgs> logChangesToFile = args =>
            {
                var changeType = args.Action.ToString();
                var currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                using (var writer = File.AppendText("~/../../../../changes.log"))
                {
                    writer.WriteLine($"[{currentTime}] Collection: {collection.GetType().Name}, Change: {changeType}");
                }
            };

            observable.Subscribe(logChangesToFile);

            Console.WriteLine("Press any key to add an item to the collection. Press 'q' to quit.");

            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Q)
                    break;

                var newItem = $"Item {collection.Count + 1}";
                collection.Add(newItem);
                Console.WriteLine($"Add: {newItem}");
            }

            Console.WriteLine("Changes logged to 'changes.log'. Press any key to exit.");
            Console.ReadKey();
        }
    }
}

