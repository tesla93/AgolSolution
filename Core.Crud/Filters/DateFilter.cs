using System;
using Core.Crud.Filters.Handlers;
using Core.Crud.Filters.Handlers;

namespace Core.Crud.Filters
{
    [RelatedHandler(typeof(DateFilterHandler))]
    public class DateFilter : CountableFilterBase<DateTime>
    {
    }

    [RelatedHandler(typeof(CountableBetweenFilterHandler<DateTime>))]
    public class DateBetweenFilter : CountableBetweenFilterBase<DateTime>
    {
    }
}