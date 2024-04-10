using Core.Crud.Filters.Handlers;

namespace Core.Crud.Filters
{
    [RelatedHandler(typeof(NumberFilterHandler))]
    public class NumberFilter : CountableFilterBase<double>
    {
        public NumberFilter()
        {
            
        }
    }

    [RelatedHandler(typeof(CountableBetweenFilterHandler<double>))]
    public class NumberBetweenFilter : CountableBetweenFilterBase<double>
    {
        public NumberBetweenFilter()
        {
            
        }
    }
}