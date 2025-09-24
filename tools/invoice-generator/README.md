invoice-gen - simple PDF invoice generator (dotnet tool)

Usage

1. Build the tool:

   dotnet build

2. Run locally:

   dotnet run --project ./tools/invoice-generator -- ./tools/invoice-generator/sample-invoice.json ./out/invoice.pdf

3. Install as a global tool (optional):

   dotnet pack -c Release -o ./nupkg
   dotnet tool install --global --add-source ./nupkg invoice-gen

Input

Provide an input JSON matching `sample-invoice.json`. The `Seller.LogoPath` can point to a PNG/JPG file; if missing the logo is skipped.
