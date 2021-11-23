using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Lesson.FileExplorer.Converter
{
	public class BytesHumanizerConverter : IValueConverter
	{
		static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
		static string SizeSuffix(long value)
		{
			if (value < 0)
			{
				return "-" + SizeSuffix(-value);
			}

			var i = 0;
			var dValue = (decimal)value;
			while (Math.Round(dValue / 1024) >= 1)
			{
				dValue /= 1024;
				i++;
			}

			return string.Format("{0:n1} {1}", dValue, SizeSuffixes[i]);
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is int intVal)
			{
				return SizeSuffix(intVal);
			}
			if (value is long longVal)
			{
				return SizeSuffix(longVal);
			}

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
