using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;
using TestP.Data.Model.Enum;

namespace TestP.Helper
{
    public class SexEnumConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (Enum.TryParse(typeof(Sex), text, true, out var result))
            {
                return result;
            }
            return Sex.Undefined;
        }
    }
}
