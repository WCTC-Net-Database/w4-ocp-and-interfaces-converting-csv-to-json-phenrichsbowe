using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;

namespace W4_assignment_template.Services;

public class CsvFileHandler : IFileHandler
{
    private static readonly string filePath = "Files/input.csv";

    public List<Character> ReadCharacters()
    {
        List<Character> characters = [];

        using var streamReader = new StreamReader(filePath);
        using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        var records = csvReader.GetRecords<Character>().ToList();

        foreach (Character character in records) {
            characters.Add(character);
        }

        return characters;
    }

    public void WriteCharacters(List<Character> characters)
    {
        using var streamWriter = new StreamWriter(filePath);
        using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
        
        csvWriter.Context.RegisterClassMap<CSVMap>();
        csvWriter.WriteRecords(characters);
    }

    public CsvFileHandler() {
        if (!File.Exists(filePath)) {
            Console.WriteLine($"Character storage file does not exist, creating it at {filePath}");
            File.Create(filePath).Dispose();
        }
    }
}

public class CSVMap : ClassMap<Character>
{
    public CSVMap()
    {   
        AutoMap(CultureInfo.InvariantCulture);
        Map(m => m.Equipment).Convert(args => string.Join(",", args.Value.Equipment)).Index(4);
    }
}