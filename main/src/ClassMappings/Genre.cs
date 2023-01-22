namespace SpecialCharacterAssistance2.ClassMappings
{
    public class Genre
    {
        public string Name{ get; set; }

        public bool CanReplace{ get; set; }

        public bool CanUse{ get; set; }

        public SpecialCharacter[] SpecialCharacters{ get; set; }
    }
}