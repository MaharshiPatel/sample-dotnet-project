using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task<int> Main(string[] args)
    {
        if (args.Length == 0 || args[0] == "-h" || args[0] == "--help")
        {
            Console.WriteLine("Usage: invoice-gen <input.json> <output.pdf>");
            return 1;
        }

        var inputPath = args[0];
        var outputPath = args.Length > 1 ? args[1] : "invoice.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return 2;
        }

        try
        {
            using var fs = File.OpenRead(inputPath);
            var invoice = await JsonSerializer.DeserializeAsync<Invoice>(fs, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (invoice == null)
            {
                Console.Error.WriteLine("Failed to parse invoice JSON.");
                return 3;
            }

            var doc = new InvoiceDocument(invoice);
            doc.Generate(outputPath);
            Console.WriteLine($"Generated PDF: {outputPath}");
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return 99;
        }
    }
}
