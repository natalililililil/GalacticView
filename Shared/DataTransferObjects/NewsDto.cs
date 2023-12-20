using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record NewsDto
    {
        public Guid Id { get; init; }
        public string URL { get; init; }
        public string Title { get; init; }
        public string? Subtitle { get; init; }
        public string NewsContent { get; init; }

    }
}
