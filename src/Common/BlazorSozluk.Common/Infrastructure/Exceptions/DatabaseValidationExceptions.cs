using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Infrastructure.Exceptions;
public class DatabaseValidationExceptions : Exception
{
    public DatabaseValidationExceptions()
    {
    }

    public DatabaseValidationExceptions(string? message) : base(message)
    {
    }

    public DatabaseValidationExceptions(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DatabaseValidationExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
