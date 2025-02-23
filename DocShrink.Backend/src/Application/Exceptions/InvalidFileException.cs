namespace Application.Exceptions;

public class InvalidFileException : Exception
{
    public InvalidFileException(string message) : base(message) { }
}

public class CompressionFailureException : Exception
{
  public CompressionFailureException(string message, Exception innerException) : base(message, innerException) {}
}
