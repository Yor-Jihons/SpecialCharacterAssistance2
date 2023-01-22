namespace SpecialCharacterAssistance2.ClassMappings
{
    /// <summary>
    /// The class in order to contain the special character. (like "Θ".)
    /// </summary>
    public class SpecialCharacter
    {
        /// <value>The ID. (in order to sort.)</value>
        public int Id { get; set; }

        /// <value>Whether this special character can be used or not.</value>
        public bool CanUse { get; set; }

        /// <value>The special character.</value>
        public string Character { get; set; }

        /// <value>The html string like "&gt;".</value>
        public string HtmlString { get; set; }

        /// <value>The description.</value>
        public string Description { get; set; }
    }
}