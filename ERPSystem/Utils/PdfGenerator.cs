using System;
using System.IO;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace ERPSystem.Utils
{
    public class PdfGenerator
    {
        private readonly string _outputFilePath;

        public PdfGenerator(string outputFilePath)
        {
            _outputFilePath = outputFilePath;
        }

        public async Task GeneratePdfAsync(string title, string content)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));
            }

            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException("Content cannot be null or empty.", nameof(content));
            }

            try
            {
                using (var writer = new PdfWriter(_outputFilePath))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var document = new Document(pdf);

                        // Add title
                        document.Add(new Paragraph(title)
                            .SetFontSize(18)
                            .SetBold()
                            .SetMarginBottom(10));

                        // Add content
                        document.Add(new Paragraph(content)
                            .SetFontSize(12));

                        // Ensure that the document is properly closed
                        await Task.CompletedTask;
                    }
                }

                Console.WriteLine("PDF generated successfully.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to generate PDF: {ex.Message}");
                throw;
            }
        }
    }
}
