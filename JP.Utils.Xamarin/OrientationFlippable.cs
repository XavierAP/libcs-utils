using System;
using Xamarin.Forms;

namespace JP.Utils
{
	public enum Orientation
	{
		NotSet,
		Portrait,
		Landscape
	}

	public class OrientationFlipBehavior
	{
		readonly VisualElement parent;

		public event Action<Orientation>? Flipped;

		public OrientationFlipBehavior(VisualElement element)
		{
			parent = element;
			element.SizeChanged += OnSizeChanged;
		}
		

		public Orientation PageOrientation
		{
			get => _PageOrientation;

			private set
			{
				if(value == _PageOrientation) return;
				_PageOrientation = value;
				Flipped?.Invoke(value);
			}
		}
		private Orientation _PageOrientation = Orientation.NotSet;


		private void OnSizeChanged(object sender, EventArgs ea)
		{
			if(parent.Width > parent.Height)
				PageOrientation = Orientation.Landscape;
			else
				PageOrientation = Orientation.Portrait;
		}
	}
}
