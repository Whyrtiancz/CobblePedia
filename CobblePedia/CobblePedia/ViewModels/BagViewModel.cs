namespace CobblePedia.ViewModels
{
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    public partial class BagViewModel : ObservableObject
    {
        [ObservableProperty] private ItemViewModel item;
        [ObservableProperty] private int quantity;

        public BagViewModel(Bag source)
        {
            item = new ItemViewModel(source.BagId);
            quantity = source.Quantity;

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        internal void SetLanguage(string language)
        {
            item.SetLanguage(language);
        }

    }
}
