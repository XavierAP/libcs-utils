using System;
using Xamarin.Forms;

namespace JP.Utils
{
	public class TablePage : ContentPage
	{
		State current = State.Empty;
		enum State { Empty, FillingTable, ButtonsSet }

		int
			nColumns,
			iColumnCurrent = 0,
			nButtons = 0;
		
		readonly Grid
			table,
			buttonsLayout;
		
		readonly RowDefinition rowLayout = new RowDefinition { Height = GridLength.Auto };
		
		public bool IsEmpty => table.RowDefinitions.Count < 1;

		public TablePage()
		{
			var wholeLayout = new StackLayout();
			wholeLayout.Children.Add(CreateTableScrollView(out table));
			wholeLayout.Children.Add(buttonsLayout = CreateButtonsLayout());
			this.Content = wholeLayout;
		}

		/// <summary>Call first. Optionally for no headers, call <see cref="SetColumns(int)"/> instead.</summary>
		public void SetHeaders(params string[] headers)
		{
			SetColumns(headers.Length);
			AddRow();
			foreach(var h in headers)
				AddCellToCurrentRow(new Label
				{
					Text = h,
					FontAttributes = FontAttributes.Bold,
				});
		}

		/// <summary>Alternative to <see cref="SetHeaders(string[])"/></summary>
		public void SetColumns(int columnCount)
		{
			if(current > State.Empty) throw new InvalidOperationException("Columns already defined.");

			current = State.FillingTable;
			nColumns = columnCount;

			for(int i = 0; i < columnCount; i++)
				table.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
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
			current = State.FillingTable;
		}

		/// <summary>Call last.</summary>
		public Button AddButtonAtBottom(string text, EventHandler clicked)
		{
			var btn = new Button { Text = text };
			btn.Clicked += clicked;

			buttonsLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
			buttonsLayout.Children.Add(btn, nButtons++, 0);
			
			current = State.ButtonsSet;
			return btn;
		}

		public void DeleteLastRow()
		{
			if(IsEmpty) throw new InvalidOperationException("There are no rows to delete.");

			var lastIndex = table.Children.Count - 1;
			var lastIndexToRemain = lastIndex - nColumns;
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
			if(current < State.FillingTable) throw new InvalidOperationException("Must set columns or headers before adding rows or cells.");
			if(current > State.FillingTable) throw new InvalidOperationException("Cannot add cells after buttons.");

			iRowCurrent = table.RowDefinitions.Count - 1;

			if(iRowCurrent < 0) throw new InvalidOperationException("Must add a row before adding more cells.");
			if(iColumnCurrent >= nColumns) throw new InvalidOperationException("Row already full. You may add a new row to fit more cells.");
		}
	}
}
