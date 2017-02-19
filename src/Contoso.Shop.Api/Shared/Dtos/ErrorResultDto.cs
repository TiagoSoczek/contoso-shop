using System.Collections.Generic;

namespace Contoso.Shop.Api.Shared.Dtos
{
    public class ErrorResultDto
    {
        public string Error { get; set; }
        public string StackTrace { get; set; }
        public IReadOnlyDictionary<string, string[]> Issues { get; set; }
    }
}