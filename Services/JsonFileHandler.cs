using Newtonsoft.Json;
using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;

namespace W4_assignment_template.Services;

public class JsonFileHandler : IFileHandler
{
    private static readonly string filePath = "Files/input.json";

    public List<Character> ReadCharacters()
    {
        using StreamReader streamReader = new(filePath);
        string json = streamReader.ReadToEnd();

        return JsonConvert.DeserializeObject<List<Character>>(json) ?? [];
    }

    public void WriteCharacters(List<Character> characters)
    {
        string json = JsonConvert.SerializeObject(characters, Formatting.Indented);
        using StreamWriter streamWriter = new(filePath);

        streamWriter.Write(json);
        streamWriter.Dispose();
    }

    public JsonFileHandler() {
        if (!File.Exists(filePath)) {
            Console.WriteLine($"Character storage file does not exist, creating it at {filePath}");
            File.Create(filePath).Dispose();
        }
    }
}