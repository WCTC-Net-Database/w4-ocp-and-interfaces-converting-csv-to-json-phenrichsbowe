namespace W4_assignment_template.Models;

public class Character
{
    public string Name { get; set; }
    public string Class { get; set; }
    public uint Level { get; set; }
    public uint HP { get; set; }
    public List<string> Equipment { get; set; }

    private static string PromptCharacterName()
    {
        string? characterName = "";

        while (string.IsNullOrEmpty(characterName))
        {
            Console.Write("Enter the name of your character:");
            characterName = Console.ReadLine();
        }

        return characterName;
    }

    private static string PromptCharacterClass()
    {
        string? characterClass = "";

        while (string.IsNullOrEmpty(characterClass))
        {
            Console.Write("Enter the class of your character:");
            characterClass = Console.ReadLine();
        }

        return characterClass;
    }

    static private uint PromptCharacterLevel()
    {
        uint characterLevel = 0;
        bool isValidLevel = false;

        while (!isValidLevel)
        {
            Console.Write("Enter your character's level: ");
            isValidLevel = uint.TryParse(Console.ReadLine(), out characterLevel);
        }

        return characterLevel;
    }

    static private uint PromptCharacterHP()
    {
        uint characterHP = 0;
        bool isValidHP = false;

        while (!isValidHP)
        {
            Console.Write("Enter your character's HP: ");
            isValidHP = uint.TryParse(Console.ReadLine(), out characterHP);
        }

        return characterHP;
    }

    static private string[] PromptCharacterEquipment()
    {
        string? characterEquipment = null;

        while (string.IsNullOrEmpty(characterEquipment)) {
            Console.Write("Enter your character's equipment (separate items with a '|'): ");
            characterEquipment = Console.ReadLine();
        }

        return characterEquipment.Split("|", StringSplitOptions.None) ?? [];
    }

    public override string ToString()
    {
        return $"Name: {Name}, Class: {Class}, Level: {Level}, HP: {HP}, Equipment: {string.Join(", ", Equipment)}";
    }

    public Character(string? Name = null, string? Class = null, uint? Level = null, uint? HP = null, string? Equipment = null)
    {
        this.Name = Name ?? PromptCharacterName();
        this.Class = Class ?? PromptCharacterClass();
        this.Level = Level ?? PromptCharacterLevel();
        this.HP = HP ?? PromptCharacterHP();
        this.Equipment = [];

        if (Equipment is not null)
        {
            string[] equipment = Equipment.Split("|");

            foreach (string item in equipment)
            {
                this.Equipment.Add(item);
            }
        } else {
            string[] equipment = PromptCharacterEquipment();

            foreach (string iteam in equipment) {
                this.Equipment.Add(iteam);
            }
        }
    }
}