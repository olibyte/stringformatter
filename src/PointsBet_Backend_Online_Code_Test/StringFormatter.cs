using System.Text;
using System.Linq;

namespace PointsBet_Backend_Online_Code_Test
{
    public class StringFormatter
    {
        public static string ToCommaSeparatedList(string[] items, string quote)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (items.Length == 0)
            {
                return "";
            }

            return string.Join(", ", items.Select(item => $"{quote}{item}{quote}"));
        }
    }
}
