using MarkupValidator.Core;

namespace MarkupValidator.ConsoleApp.Services;

internal class AppService(IValidator validator) : IAppService
{
    private readonly IValidator _validator = validator;

    public void Run(string[] args)
    {
        // Check that the given arguments is as expected
        if (args.Length == 0)
        {
            ArgumentNullException.ThrowIfNull(nameof(args));
        }
        if (args.Length > 1)
        {
            throw new ArgumentException("Only 1 argument with a markup language input as input is allowed.");
        }

        var markupInput = args.Single();

        var isValid = _validator.Validate(markupInput);

        Console.WriteLine(isValid ? "Valid" : "Invalid");
    }
}