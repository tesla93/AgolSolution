using Core.Crud.Filters.Handlers;

namespace Core.Crud.Filters
{
    /// <summary>
    /// Ternary logic:
    /// true => Where(item => item.Property)
    /// false =>  Where(item => !item.Property)
    /// null => Where(item => true) 
    /// </summary>
    [RelatedHandler(typeof(BooleanFilterHandler))]
    public class BooleanFilter : FilterInfoBase<bool?>
    {
        public BooleanFilter()
        {
            
        }
    }
}
