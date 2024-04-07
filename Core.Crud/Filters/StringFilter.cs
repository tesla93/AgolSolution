using Core.Crud.Filters.Handlers;

namespace Core.Crud.Filters
{
    [RelatedHandler(typeof(StringFilterHandler))]
    public class StringFilter: FilterInfoBase<string>
    {
        public StringFilterMatchMode MatchMode { get; set; }
    }

    public enum StringFilterMatchMode
    {
        Contains,
        StartsWith,
        EndsWith,
        Equals
    }
}