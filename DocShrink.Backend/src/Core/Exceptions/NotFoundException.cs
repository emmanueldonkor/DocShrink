namespace Core.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base()
    {}
    public NotFoundException(string message) : base(message)
    {}
    public NotFoundException(string name, Object key) : base($"Entity \"{name}\" ({key}) was not found.")
    {}
}