using System.Globalization;
namespace WorkoutTracker;

// TODO: can these be found in the framework?

public sealed class BoolToOppositeBoolConverter : IValueConverter
{
  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is bool boolVal)
      return !boolVal;
    else
      throw new ArgumentException(nameof(value));
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}
