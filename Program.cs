using System;
using System.IO;
using System.Threading.Tasks;

namespace UserCreator
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            if(args.Length != 1)
            {
                await Console.Out.WriteLineAsync("Usage: UserCreator [outputfile]");
                return 1;
            }

            string outputFilePath = args[0];
            await using var outputFile = File.OpenWrite(outputFilePath);
            await using var outputFileWriter = new StreamWriter(outputFile);

            await ProcessUserFields(outputFileWriter);

            return 0;
        }

         static async Task ProcessUserFields(StreamWriter outputFileWriter)
        {
            string fieldType;
            while (!string.IsNullOrEmpty(fieldType = await GetFieldType()))
            {
                await ProcessField(fieldType, outputFileWriter);
                Console.WriteLine("============");
            }
        }

         static async Task ProcessField(string fieldType, StreamWriter outputFileWriter)
        {
            if (string.Equals("DateOfBirth", fieldType, StringComparison.CurrentCultureIgnoreCase))
            {
                await WriteUserDataToFile<DateTime>(fieldType, outputFileWriter);
            }
            else if (string.Equals("Salary", fieldType, StringComparison.CurrentCultureIgnoreCase))
            {
                await WriteUserDataToFile<decimal>(fieldType, outputFileWriter);
            }
            else
            {
                await WriteUserDataToFile<string>(fieldType, outputFileWriter);
            }
        }

        private static async Task WriteUserDataToFile<TDataType>(string fieldName, StreamWriter streamWriter)
        {
            var userDataParser = new UserDataParser<TDataType>();
            var dataAsString = await GetData(fieldName);
            if(userDataParser.TryConvertData(dataAsString, out var data))
            {
                await userDataParser.WriteDataToCsv(streamWriter, fieldName, data);
            }
        }

        static async Task<string> GetFieldType()
        {
            await Console.Out.WriteLineAsync("Please enter field, or enter to exit");
            return await Console.In.ReadLineAsync();
        }

        static async Task<string> GetData(string fieldName)
        {
            await Console.Out.WriteLineAsync($"Please enter user's {fieldName}:");
            return await Console.In.ReadLineAsync();
        }

    }

}
