namespace CobblePedia.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.UI.Dispatching;

    internal class AsyncPokemonLoader
    {
        private const int BatchSize = 50;
        private readonly DispatcherQueue dispatcher;

        public AsyncPokemonLoader(DispatcherQueue dispatcherQueue)
        {
            dispatcher = dispatcherQueue;
        }

        public async Task LoadPokemonAsync(
            ObservableCollection<SearchableObjectViewModel> collection,
            IEnumerable<SearchableObjectViewModel> source)
        {
            var batches = source
                .Select((item, index) => new { item, index })
                .GroupBy(x => x.index / BatchSize)
                .Select(g => g.Select(x => x.item).ToList());

            foreach (var batch in batches)
            {
                await Task.Delay(10);

                dispatcher.TryEnqueue(() =>
                {
                    foreach (var item in batch)
                    {
                        collection.Add(item);
                    }
                });
            }
        }
    }
}
