using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;
using CsvHelper;
using System.Globalization;

namespace W4_assignment_template.Services;

public class CsvFileHandler : IFileHandler
{
    public List<Character> ReadCharacters(string filePath)
    {
        List<Character> characters = [];

        if (!File.Exists(filePath)) {
            Console.WriteLine($"Character storage file does not exist, creating it at {filePath}");
            File.Create(filePath).Dispose();
            return characters;
        }

        using var streamReader = new StreamReader(filePath);
        using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        var records = csvReader.GetRecords<Character>().ToList();

        foreach (Character character in records) {
            characters.Add(character);
        }

        return characters;
    }

    public void WriteCharacters(string filePath, List<Character> characters)
    {
        using var streamWriter = new StreamWriter(filePath);
        using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

        csvWriter.WriteRecords(characters);
    }
}