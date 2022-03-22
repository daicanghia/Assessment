using System;

namespace MsDemo.Shared
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SwaggerOperationAttribute : Attribute
    {
        public SwaggerOperationAttribute(string operationId)
        {
            OperationId = operationId;
        }

        public string OperationId { get; }
    }
}