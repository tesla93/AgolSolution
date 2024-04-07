namespace Core.Crud.Filters.Handlers
{
    public class NumberFilterHandler : CountableFilterHandler<double>
    {
        public NumberFilterHandler(NumberFilter filter) : base(filter)
        {
        }
    }
}
