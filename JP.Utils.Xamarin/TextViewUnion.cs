using System;
using Xamarin.Forms;

namespace JP.Utils
{
	/// <summary>Union-like type that wraps either an <see cref="Entry"/> or a <see cref="Label"/>.</summary>
	public class TextViewUnion
	{
		public string Text
		{
			get => Entry?.Text ??
			       Label?.Text ?? throw WillNeverHappen();

			set { if(Entry != null) Entry.Text = value; }
		}

		public View AsView => Entry as View ?? Label ?? throw WillNeverHappen();
		public Entry? Entry { get; }
		public Label? Label { get; }

		public TextViewUnion(Entry payload) => Entry = payload;
		public TextViewUnion(Label payload) => Label = payload;

		private static InvalidProgramException WillNeverHappen()
			=> new InvalidProgramException("Oops my bad. -- Signed: " + nameof(TextViewUnion));
	}
}
