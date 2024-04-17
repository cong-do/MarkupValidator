using FluentAssertions;
using MarkupValidator.Core;

namespace MarkupValidator.UnitTests;

public static class XmlValidatorTests
{
    public abstract class XmlValidatorTestsBase
    {
        protected readonly XmlValidator _validator;

        protected XmlValidatorTestsBase()
        {
            _validator = new XmlValidator();
        }
    }

    public class Validate : XmlValidatorTestsBase
    {
        [Theory]
        [InlineData("<Design><Code>hello world</Code></Design>")]
        [InlineData("<People><Design><Code>hello world</Code></Design></People>")]
        [InlineData("<People age='1'>hello world</People age='1'>")]
        public void ValidInputs(string xmlInput)
        {
            // Act
            var result = _validator.Validate(xmlInput);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("<Design><Code>hello world</Code></Design><People>")]
        [InlineData("<People><Design><Code>hello world</People></Code></Design>")]
        [InlineData("<People age='1'>hello world</People>")]
        [InlineData("Design><Code>hello world</Code></Design>")]
        [InlineData("<Design><Code>hello world</Code><Design>")]
        [InlineData("hello world")]
        public void InvalidInputs(string xmlInput)
        {
            // Act
            var result = _validator.Validate(xmlInput);

            // Assert
            result.Should().BeFalse();
        }
    }
}