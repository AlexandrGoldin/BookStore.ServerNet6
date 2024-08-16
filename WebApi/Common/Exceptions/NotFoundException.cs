namespace WebApi.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, int? Id)
        : base($"Object: {name} = {Id} not found.") { }      
    }
}
