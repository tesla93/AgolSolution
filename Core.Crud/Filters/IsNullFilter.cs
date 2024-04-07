using Core.Crud.Filters.Handlers;

namespace Core.Crud.Filters
{
    [RelatedHandler(typeof(IsNullFilterHandler))]
    public class IsNullFilter: FilterInfoBase
    {
    }
}
