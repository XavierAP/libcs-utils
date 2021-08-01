using Xamarin.Forms;

namespace JP.Utils
{
	public class CenteredDialogPage : ContentPage
	{
		readonly StackLayout layout;

		public CenteredDialogPage()
		{
			Content = new ScrollView
			{
				Content = layout = new StackLayout
				{
					HorizontalOptions = LayoutOptions.Center,
				},
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
			};
		}

		public void AddElement(View elem) => layout.Children.Add(elem);
	}
}
