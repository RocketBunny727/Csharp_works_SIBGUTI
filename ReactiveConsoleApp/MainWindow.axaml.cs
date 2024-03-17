using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Reactive.Linq;

namespace ReactiveConsoleApp
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<int> _collection = new ObservableCollection<int>();
        private TextBlock _outputText;

        public MainWindow()
        {
            InitializeComponent();
            _collection.CollectionChanged += CollectionChangedHandler;

            var observable = CollectionChangeFactory.CreateObservableCollectionChanges(_collection);
            observable.Subscribe(args =>
            {
                LogChangesToFile(args);
            });

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _outputText = this.FindControl<TextBlock>("OutputText");
        }

        private void CollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Обработчик события изменения коллекции
            Console.WriteLine($"Collection changed: {e.Action}");
        }

        private void AddItem_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Random random = new Random();
            int newItem = random.Next(100);
            _collection.Add(newItem);
        }

        private void LogChangesToFile(NotifyCollectionChangedEventArgs args)
        {
            // Логирование изменений в файл
            string logMessage = $"{DateTime.Now}: {args.Action} - {args.NewItems?[0]}";
            File.AppendAllText("~/../../../../collection_changes.log", logMessage + Environment.NewLine);

            // Обновляем текстовый блок на экране
            _outputText.Text += logMessage + Environment.NewLine;
        }
    }

    public class CollectionChangeFactory
    {
        public static IObservable<NotifyCollectionChangedEventArgs> CreateObservableCollectionChanges<T>(
            ObservableCollection<T> collection)
        {
            return Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                    h => collection.CollectionChanged += h,
                    h => collection.CollectionChanged -= h)
                .Select(pattern => pattern.EventArgs);
        }
    }
}
