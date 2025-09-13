using System.Text;

namespace PointsBet_Backend_Online_Code_Test
{
    public class StringFormatter
    {
        public static string ToCommaSeparatedList(string[] items, string quote)
        {
            if (items == null || items.Length == 0)
            {
                return "";
            }

            StringBuilder qry = new StringBuilder(string.Format("{0}{1}{0}", quote, items[0]));

            for (int i = 1; i < items.Length; i++)
            {
                qry.Append(string.Format(", {0}{1}{0}", quote, items[i]));
            }

            return qry.ToString();
        }
    }
}
