
namespace Common
{
    public static class ModelConstants
    {
        public static class Person
        {
            public const int NameMaxLength = 30;
            public const int FamilyMaxLength = 30;
        }

        public static class Shared
        {
            public const string SmallDatetimeColumnType = "smalldatetime"; // smalldatetime: From January 1, 1900 to June 6, 2079 with an accuracy of 1 minutes
        }
    }
}
