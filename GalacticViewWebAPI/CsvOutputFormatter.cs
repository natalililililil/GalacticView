﻿using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Shared.DataTransferObjects;
using System.Text;

namespace GalacticViewWebAPI
{
    public class CsvOutputFormatter: TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            if (typeof(PlanetDto).IsAssignableFrom(type) ||
           typeof(IEnumerable<PlanetDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();
            if (context.Object is IEnumerable<PlanetDto>)
            {
                foreach (var planet in (IEnumerable<PlanetDto>)context.Object)
                {
                    FormatCsv(buffer, planet);
                }
            }
            else
            {
                FormatCsv(buffer, (PlanetDto)context.Object);
            }

            await response.WriteAsync(buffer.ToString());
        }
        private static void FormatCsv(StringBuilder buffer, PlanetDto planet)
        {
            buffer.AppendLine($"{planet.Id},\"{planet.Name},\"{planet.FullPlanetInfo}\"");
        }
    }  
}
