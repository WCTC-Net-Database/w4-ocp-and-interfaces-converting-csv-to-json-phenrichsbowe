using Newtonsoft.Json;
using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;

namespace W4_assignment_template.Services;

public class JsonFileHandler : IFileHandler
{
    private static string filePath = "Files/input.json";

    public List<Character> ReadCharacters()
    {
        List<Character>? characters;

        if (!File.Exists(filePath)) {
            Console.WriteLine($"Character storage at {filePath} does not exist attempting to create it...");

            File.Create(filePath).Dispose();
        }

        using StreamReader streamReader = new(filePath);
        string json = streamReader.ReadToEnd();

        characters = JsonConvert.DeserializeObject<List<Character>>(json);
        
        return characters;
    }

    public void WriteCharacters(List<Character> characters)
    {
        string json = JsonConvert.SerializeObject(characters, Formatting.Indented);
        using StreamWriter streamWriter = new(filePath);

        streamWriter.Write(json);
        streamWriter.Dispose();
    }
}