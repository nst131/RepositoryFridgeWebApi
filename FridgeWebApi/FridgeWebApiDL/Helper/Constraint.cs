namespace FridgeWebApiDL.Helper
{
    public static class Constraint
    {
        public const string PrimaryKey = "Primary Key";
        public const string Unique = nameof(Unique);
        public const string Check = nameof(Check);
        public const string Default = nameof(Default);
        public const string Null = nameof(Null);
        public const string NotNull = "Not Null";
        public const string Identity = nameof(Identity);
        public static string ForeignKey(string nameTable, string Id) => $"Foreign Key References {nameTable}({Id})";
    }
}
