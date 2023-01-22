namespace SpecialCharacterAssistance2.ClassMappings
{
    /// <summary>
    /// The class in order to contain the data.
    /// </summary>
    public class Genre
    {
        /// <value>The title like "Half-width symbol".</value>
        public string Name { get; set; }

        /// <value>Whether this genre can be repalced (like "<" to "&lt;" ) or not.</value>
        public bool CanReplce { get; set; }

        /// <value>Whether this genre can be used or not.</value>
        public bool CanUse { get; set; }

        /// <value>The list of the class SpecialCharacter which means some special characters like "Θ".</value>
        public System.Collections.Generic.List<SpecialCharacter> SpecialCharacters { get; set; }
    }
}