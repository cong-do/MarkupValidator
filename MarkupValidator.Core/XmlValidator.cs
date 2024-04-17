namespace MarkupValidator.Core;

public class XmlValidator : IValidator
{
    public bool Validate(string input)
    {
        // Initial input validation
        if (string.IsNullOrWhiteSpace(input) || !input.StartsWith("<") || !input.EndsWith(">"))
        {
            return false;
        }

        // Keep track of tags with Stack that utilizes a "Last In First Out" system
        // to validate the elements are well nested
        var tagStack = new Stack<string>();

        // Loop over the input and only process if the input starts with the opening angle bracket
        // If it's the opening tag, push the tag name to the tagStack
        // If it's the closing tag, pop the last added tag name from the tagStack
        // 
        // Ultimately, if the input is well nested, the tagStack should end up being empty again
        for (var index = 0; index < input.Length - 1; index++)
        {
            if (input[index] != '<')
            {
                continue;
            }

            // Check if node is the opening tag
            if (input[index + 1] != '/')
            {
                var startPosition = index + 1;
                var endPosition = input.IndexOf('>', startPosition);

                if (endPosition > startPosition)
                {
                    var tagName = input[startPosition..endPosition].Trim();

                    // Push the tag onto the stack
                    tagStack.Push(tagName);
                }
            }
            // Check if node is the closing tag
            else if (input[index + 1] == '/')
            {
                var startPosition = index + 2;
                var endPosition = input.IndexOf('>', startPosition);

                if (endPosition > startPosition)
                {
                    var tagName = input[startPosition..endPosition].Trim();

                    // Check if the closing tag matches the last added tag in the stack
                    if (tagStack.Count == 0 || tagStack.Peek() != tagName)
                    {
                        return false;
                    }

                    // Pop the tag from the stack
                    tagStack.Pop();
                }
            }
        }

        // When opening and closing tags are all matching and well nested, the tagStack should be empty
        var hasMatchingTags = tagStack.Count == 0;
        return hasMatchingTags;
    }
}