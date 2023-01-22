namespace SpecialCharacterAssistance2.ClassMappings
{
    public class Genre
    {
        public string Name { get; set; }
        public bool CanReplce { get; set; }
        public bool CanUse { get; set; }
        public System.Collections.Generic.List<SpecialCharacter> SpecialCharacters { get; set; }
    }
}