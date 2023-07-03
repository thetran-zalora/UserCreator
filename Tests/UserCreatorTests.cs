using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;

namespace UserCreator.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public async Task ProcessField_DateOfBirth_ShouldWriteDataToFile()
        {
            // Arrange
            var outputFilePath = "test_output.csv";
            await using var outputFile = File.OpenWrite(outputFilePath);
            await using var outputFileWriter = new StreamWriter(outputFile);
            var fieldType = "DateOfBirth";

            // Act
            await Program.ProcessField(fieldType, outputFileWriter);

            // Assert
            var fileContents = await File.ReadAllTextAsync(outputFilePath);
            Assert.AreEqual("DateOfBirth, <data>", fileContents); 
        }

        [Test]
        public async Task ProcessField_Salary_ShouldWriteDataToFile()
        {
            // Arrange
            var outputFilePath = "test_output.csv";
            await using var outputFile = File.OpenWrite(outputFilePath);
            await using var outputFileWriter = new StreamWriter(outputFile);
            var fieldType = "Salary";

            // Act
            await Program.ProcessField(fieldType, outputFileWriter);

            // Assert
            var fileContents = await File.ReadAllTextAsync(outputFilePath);
            Assert.AreEqual("Salary, <data>", fileContents);
        }

        [Test]
        public async Task ProcessField_OtherField_ShouldWriteDataToFile()
        {
            // Arrange
            var outputFilePath = "test_output.csv";
            await using var outputFile = File.OpenWrite(outputFilePath);
            await using var outputFileWriter = new StreamWriter(outputFile);
            var fieldType = "OtherField";

            // Act
            await Program.ProcessField(fieldType, outputFileWriter);

            // Assert
            var fileContents = await File.ReadAllTextAsync(outputFilePath);
            Assert.AreEqual("OtherField, <data>", fileContents); 
        }
    }
}
