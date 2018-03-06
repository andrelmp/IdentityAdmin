using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentityAdmin.Core.Validators
{
    internal static class Validator
    {
        public static void IfNullOrWhiteSpace(string source, string message, List<OperationError> errors)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                errors.Add(new OperationError(message));
            }
        }

        public static void IfEmpty<Tsource>(IEnumerable<Tsource> source, string message, List<OperationError> errors)
            where Tsource : class
        {
            if (!source.Any())
            {
                errors.Add(new OperationError(message));
            }
        }
    }
}
