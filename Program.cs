using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;
using W4_assignment_template.Services;

namespace W4_assignment_template;

class Program
{
    static IFileHandler fileHandler = new CsvFileHandler();
    static readonly List<Character> characters = fileHandler.ReadCharacters();

    enum FileFormat {
        CSV = 1,
        JSON = 2
    }

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Display Characters");
            Console.WriteLine("2. Add Character");
            Console.WriteLine("3. Find Character");
            Console.WriteLine("4. Level Up Character");
            Console.WriteLine("5. Change File Format (CSV/JSON)");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayAllCharacters();
                    break;
                case "2":
                    AddCharacter();
                    break;
                case "3":
                    FindCharacter();
                    break;
                case "4":
                    LevelUpCharacter();
                    break;
                case "5":
                    ChangeFileFormat();
                    break;
                case "6":
                    fileHandler.WriteCharacters(characters);
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    public static Character? FindCharacter() {
        Character? character;
        string? characterName = null;

        while (string.IsNullOrEmpty(characterName)) {
            Console.Write("Enter the name of the character you would like to find:");
            characterName = Console.ReadLine();
        }

        character = characters.Find(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));

        if (character == null) {
            Console.WriteLine($"Character {characterName} does not exist.");
            return character;
        }

        Console.WriteLine($"Found character {character}");

        return character;
    }

    static void DisplayAllCharacters()
    {
        if (characters.Count == 0) {
            Console.WriteLine("No characters to display character storage is empty, try creating a character first!");
            return;
        }

        foreach (Character character in characters)
        {
            Console.WriteLine(character);
        }
    }

    static void AddCharacter()
    {
        Character character = new();

        if (character == null) return;

        characters.Add(character);
    }

    static void LevelUpCharacter()
    {
        Character? character = FindCharacter();

        if (character == null)  return;

        character.Level += 1;

        Console.WriteLine($"Success character {character.Name} levled up to {character.Level}!");
    }

    static void ChangeFileFormat() {
        FileFormat selectedFormat;

        while (true) {
            string? input;

            Console.WriteLine("Which file format would you like to switch to?:");
            Console.WriteLine("1: CSV");
            Console.WriteLine("2: JSON");

            input = Console.ReadLine();

            if (Enum.TryParse(input, out selectedFormat) && Enum.IsDefined(typeof(FileFormat), selectedFormat)) {
                break;
            }

            Console.WriteLine("Invalid file format specified. Enter '1' for CSV or '2' for JSON.");
        }

        switch (selectedFormat) {
            case FileFormat.CSV:
                if (fileHandler is CsvFileHandler) {
                    Console.WriteLine("File format is already set to use CSV.");
                    return;
                }
                
                fileHandler = new CsvFileHandler();
                Console.WriteLine("Success file format changed to CSV!");
                break;
            case FileFormat.JSON:
                if (fileHandler is JsonFileHandler) {
                    Console.WriteLine("File format is already set to use JSON");
                    return;
                }

                fileHandler = new JsonFileHandler();
                Console.WriteLine("Success file format changed to JSON!");
                break;
        }
    }
}