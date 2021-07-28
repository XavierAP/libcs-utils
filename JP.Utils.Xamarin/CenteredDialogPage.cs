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

		public Label
		AddLabel(string text)
			=> AddElement(new Label
			{
				Text = text
			});

		public Entry
		AddEntry(string hintText, Keyboard keyboard)
			=> AddElement(new Entry
			{
				Placeholder = hintText,
				Keyboard = keyboard,
			});

		public Button
		AddButton(string text)
			=> AddElement(new Button
			{
				Text = text,
			});

		private T AddElement<T>(T elem)
			where T : View
		{
			layout.Children.Add(elem);
			return elem;
		}
	}
}
