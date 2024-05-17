using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using TestP.Data.Model;
using TestP.Data.Model.Enum;

namespace TestP.Helper
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            
            Map(m => m.FirstName).Name("FirstName");
            Map(m => m.Surname).Name("Surname");
            Map(m => m.Age).Name("Age").TypeConverter<Int32Converter>().Optional();
            Map(m => m.Sex).Name("Sex").TypeConverter<SexEnumConverter>().Optional();
            Map(m => m.Number).Name("Mobile").TypeConverter<Int32Converter>().Optional();
            Map(m => m.Active).Name("Active").TypeConverter<BooleanConverter>().Optional();
        }

    }
}
