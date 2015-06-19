namespace Tournament.EnumerationSlider
{
    using System;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// This class is used by the enumeration slider.
    /// And converts CamelCase to words.
    /// </summary>
    /// <example>'CamelCase' becomes 'Camel case'. </example>
    internal class AsWordsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return value.ToString().AsWords();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}