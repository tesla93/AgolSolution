namespace Core.Crud.Filters
{
    public abstract class FilterInfoBase
    {
        public string PropertyName { get; set; }
        public string? Value { get; set; }
        public string? NotNeedToDelete { get; set; }
        public string? Type { get; set; }
        public int? MatchMode { get; set; }
    }

    public abstract class FilterInfoBase<T> : FilterInfoBase
    {
        public T Value { get; set; }
    }
}