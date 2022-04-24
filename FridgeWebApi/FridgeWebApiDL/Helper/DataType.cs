namespace FridgeWebApiDL.Helper
{
    public static class DataType
    {
        public const string Int = nameof(Int);
        public static string Text = nameof(Text);
        public static string Nvarchar(int amount) => nameof(Nvarchar) + $"({amount})";
    }
}
