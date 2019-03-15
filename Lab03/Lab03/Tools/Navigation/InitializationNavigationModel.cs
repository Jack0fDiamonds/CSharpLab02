using System;
using Lab03.Views;

namespace Lab03.Tools.Navigation
{
    internal class InitializationNavigationModel : BaseNavigationModel
    {
        public InitializationNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {

        }

        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.PersonCreation:
                    ViewsDictionary.Add(viewType, new PersonCreationView());
                    break;
                case ViewType.PersonDisplay:
                    ViewsDictionary.Add(viewType, new PersonDisplayView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
