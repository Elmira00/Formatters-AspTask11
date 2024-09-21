using Formatters_AspTask11.Dtos;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace Formatters_AspTask11.Formatters
{
    public class TextCsvInputFormatter : TextInputFormatter
    {
        public TextCsvInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        protected override bool CanReadType(Type type)
        => type == typeof(StudentAddDto);
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding effectiveEncoding)
        {
            var httpContext = context.HttpContext;
            var serviceProvider = httpContext.RequestServices;
            using var reader = new StreamReader(httpContext.Request.Body, effectiveEncoding);
            var newStudent = new StudentAddDto();
            try
            {
                await ReadLineAsync("Id - Fullname - SeriaNo - Age - Score", reader, context);

                var FullData = await reader.ReadLineAsync();
                var split = FullData.Split(" - ");

                newStudent.Fullname = split[0].Trim();
                newStudent.SeriaNo = split[1].Trim();
                newStudent.Age = Int32.Parse(split[2].Trim());
                newStudent.Score = Int32.Parse(split[3].Trim());

                return await InputFormatterResult.SuccessAsync(newStudent);
            }
            catch
            {
                return await InputFormatterResult.FailureAsync();
            }

        }
        private static async Task<string> ReadLineAsync(string expectedText, StreamReader reader, InputFormatterContext context)
        {
            var line = await reader.ReadLineAsync();

            if (line is null || !line.StartsWith(expectedText))
            {
                var errorMessage = $"Looked for '{expectedText}' and got '{line}'";

                context.ModelState.TryAddModelError(context.ModelName, errorMessage);
                throw new Exception(errorMessage);
            }

            return line;
        }
    }
}
