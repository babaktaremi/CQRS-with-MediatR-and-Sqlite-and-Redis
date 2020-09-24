#nullable enable
using System;

namespace MediatRCqrs.Application.Common
{
    public class OperationResult<T>  where T:class
    {
        public T Result { get; set; }
        public bool IsSuccess { get; set; }
        public Exception? Exception { get; set; }

        public string? ExceptionMessage => Exception?.Message;

    }

    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public Exception? Exception { get; set; }
        public string? ExceptionMessage => Exception?.Message;
    }
}
