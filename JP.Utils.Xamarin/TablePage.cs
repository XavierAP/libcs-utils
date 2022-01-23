using System;
using Xamarin.Forms;

namespace JP.Utils
{
	public class TablePage : ContentPage
	{
		int
			iColumnCurrent = 0,
			iColumnLast = 0,
			nButtons = 0;
		
		readonly Grid
			table,
			buttonsLayout;
		
		readonly RowDefinition rowLayout = new RowDefinition { Height = GridLength.Auto };
		
		public int RowCount => table.RowDefinitions.Count;

		public TablePage()
		{
			var wholeLayout = new StackLayout();
			wholeLayout.Children.Add(CreateTableScrollView(out table));
			wholeLayout.Children.Add(buttonsLayout = CreateButtonsLayout());
			this.Content = wholeLayout;
		}

		public void AddRow()
		{
			table.RowDefinitions.Add(rowLayout);
			iColumnCurrent = 0;
		}

		public void AddCellToCurrentRow(View cellView)
		{
			CheckTableSpaceWhenAddingCell(out var irow);
			table.Children.Add(cellView, iColumnCurrent++, irow);
		}

		public Button AddButtonAtBottom(string text, EventHandler clicked)
		{
			var btn = new Button { Text = text };
			btn.Clicked += clicked;

			buttonsLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
			buttonsLayout.Children.Add(btn, nButtons++, 0);
			
			return btn;
		}

		public void DeleteLastRow()
		{
			if(RowCount < 1) throw new InvalidOperationException("There are no rows to delete.");

			var lastIndex = table.Children.Count - 1;
			var lastIndexToRemain = lastIndex - iColumnLast - 1;
			for(var i = lastIndex; i > lastIndexToRemain; --i)
				table.Children.RemoveAt(i);

			var rows = table.RowDefinitions;
			rows.RemoveAt(rows.Count - 1);
		}

		private static ScrollView CreateTableScrollView(out Grid table) => new ScrollView
		{
			Content = table = new Grid(),
			Orientation = ScrollOrientation.Both,
			VerticalOptions = LayoutOptions.FillAndExpand,
		};

		private static Grid CreateButtonsLayout()
		{
			var buttonsLayout = new Grid();
			buttonsLayout.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			return buttonsLayout;
		}

		private void CheckTableSpaceWhenAddingCell(out int iRowCurrent)
		{
			iRowCurrent = table.RowDefinitions.Count - 1;

			if(iRowCurrent < 0)
				throw new InvalidOperationException("Must add a row before adding more cells.");
			if(iColumnCurrent >= iColumnLast)
			{
				++iColumnLast;
				table.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
			}
		}
	}
}
