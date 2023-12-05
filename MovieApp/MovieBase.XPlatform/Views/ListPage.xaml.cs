
using MovieBase.Common.Interfaces;

namespace MovieBase.XPlatform.Views;

public partial class ListPage
{
    public ListPage(ListViewModel detailsViewModel)
    {
        InitializeComponent();
        BindingContext = detailsViewModel;
    }
}

