using System.Globalization;
using System.Windows.Data;
using WPF_CHEAT_os.Utils;

namespace WPF_CHEAT_os.Converter
{
    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
          
            if (value is EstadoPropuesta estado)
            {
                return estado.ToString();
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
          
            if (value is string str)
            {
                if (Enum.TryParse<EstadoPropuesta>(str, out var estado))
                {
                    return estado;
                }
            }
            return null;
        }
    }
}
