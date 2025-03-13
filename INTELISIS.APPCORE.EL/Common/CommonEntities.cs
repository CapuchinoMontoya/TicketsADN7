namespace INTELISIS.APPCORE.EL.Common
{
    public class MonthName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
    }


    public class LibraryManager
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class KeyValueModel
    {
        public string Key { get; set; }
        public dynamic Value { get; set; }
        public dynamic ExtraValue { get; set; } = null;
    }


    public static class ConsultasLibraries
    {
        public static readonly string TypeJS = "JS";
        public static readonly string TypeCSS = "CSS";
        public static readonly string DataTables = "DataTables";
        public static readonly string DataTablesMin = "DataTablesMin";
        public static readonly string SheetJs = "SheetJs";
        public static readonly string ZingChart = "ZingChart";
        public static readonly string DateRangePicker = "DateRangePicker";
    }
}
