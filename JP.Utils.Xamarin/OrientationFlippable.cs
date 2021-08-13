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
		readonly VisualElement element;

		public event Action<Orientation>? Flipped;

		public OrientationFlipBehavior(VisualElement element)
		{
			this.element = element;
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
			if(element.Width > element.Height)
				PageOrientation = Orientation.Landscape;
			else
				PageOrientation = Orientation.Portrait;
		}
	}
}
