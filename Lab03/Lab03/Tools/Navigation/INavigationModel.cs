namespace Lab03.Tools.Navigation
{
    internal enum ViewType
    {
        PersonCreation,
        PersonDisplay
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
