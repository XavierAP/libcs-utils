using Xamarin.Forms;

namespace JP.Utils
{
	public class CenteredDialogPage : ContentPage
	{
		readonly StackLayout layout;

		public CenteredDialogPage()
		{
			layout = new StackLayout
			{
				HorizontalOptions = LayoutOptions.Center,
			};
			this.Content = new ScrollView
			{
				Content = layout,
				HorizontalOptions = LayoutOptions.Center,
			};
		}

		public void AddElement(View elem) => layout.Children.Add(elem);
	}
}
