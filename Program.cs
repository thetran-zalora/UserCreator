using System;
using System.IO;
using System.Threading.Tasks;

namespace UserCreator
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            string fieldType;
            if(args.Length != 1)
            {
                await Console.Out.WriteLineAsync($"Usage: UserCreator [outputfile]");
                return 1;
            }

            await using var outputFile = File.OpenWrite(args[0]);
            await using var outputFileWriter = new StreamWriter(outputFile);

            while(!string.IsNullOrEmpty(fieldType = await GetFieldType()))
            {
                if(string.Equals("DateOfBirth", fieldType, StringComparison.CurrentCultureIgnoreCase))
                {
                    await WriteUserDataToFile<DateTime>(fieldType, outputFileWriter);
                }
                else if(string.Equals("Salary", fieldType, StringComparison.CurrentCultureIgnoreCase))
                {
                    await WriteUserDataToFile<decimal>(fieldType, outputFileWriter);
                }
                else
                {
                    await WriteUserDataToFile<string>(fieldType, outputFileWriter);
                }

                Console.WriteLine($"============");
            }
            return 0;
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
            await Console.Out.WriteLineAsync($"Please enter field, or enter to exit");
            return await Console.In.ReadLineAsync();
        }

        static async Task<string> GetData(string fieldName)
        {
            await Console.Out.WriteLineAsync($"Please enter user's {fieldName}:");
            return await Console.In.ReadLineAsync();
        }

    }

}
