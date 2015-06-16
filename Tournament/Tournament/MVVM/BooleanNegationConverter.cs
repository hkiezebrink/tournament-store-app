using System;
using Windows.UI.Xaml.Data;

namespace Tournament.MVVM
{

    // This class is used when a tournament object is selected in the gridview. 
    // To determine which tournament object is currently selected.
    public class BooleanNegationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Boolean b = (Boolean)value;
            return !b;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
