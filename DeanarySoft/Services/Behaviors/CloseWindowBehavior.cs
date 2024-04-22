using System.Windows;

namespace DeanarySoft.Behaviors;

public static class CloseWindowBehavior
{
	public static readonly DependencyProperty IsClosedProperty =
		DependencyProperty.RegisterAttached(
			"IsClosed", typeof(bool), typeof(CloseWindowBehavior),
			new UIPropertyMetadata(false, OnIsClosedChanged));

	public static bool GetIsClosed(DependencyObject obj)
	{
		return (bool)obj.GetValue(IsClosedProperty);
	}

	public static void SetIsClosed(DependencyObject obj, bool value)
	{
		obj.SetValue(IsClosedProperty, value);
	}

	private static void OnIsClosedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is Window window && (bool)e.NewValue)
		{
			window.Close();
		}
	}
}