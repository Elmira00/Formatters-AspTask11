using Formatters_AspTask11.Dtos;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace Formatters_AspTask11.Formatters
{
    public class TextCsvOutputFormatter : TextOutputFormatter
    {
        public TextCsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var sb = new StringBuilder();
            if (context.Object is IEnumerable<StudentDto> list)
            {
                foreach (var item in list)
                {
                    FormatCSVCard(sb, item);
                }
            }
            else if (context.Object is StudentDto student)
            {
                FormatCSVCard(sb, student);
            }
            await response.WriteAsync(sb.ToString());
        }

        private void FormatCSVCard(StringBuilder sb, StudentDto item)
        {
            sb.AppendLine($"Id - Fullname -  SeriaNo - Age - Score");
            sb.AppendLine($"{item.Id} - {item.Fullname} - {item.SeriaNo} - {item.Age} - {item.Score}");
        }
    }


}
