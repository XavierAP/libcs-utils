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

		/// <summary>Called at least once as the <see cref="VisualElement"/> is first displayed;
		/// then every time device orientation is flipped.</summary>
		public event Action<Orientation>? SetOrChanged;

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
				SetOrChanged?.Invoke(value);
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
