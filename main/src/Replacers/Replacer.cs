namespace SpecialCharacterAssistance2.Replacers
{
    /// <summary>
    /// The class in ordert to replace the string on the event HtmlConversionButton_Click.
    /// </summary>
    public class Replacer
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">The target string.</param>
        public Replacer( string text )
        {
            this.TargetText = text;
        }

        /// <summary>
        /// The method which will be invoked before you replace the string.
        /// </summary>
        public void Begin()
        {
            var matchs = System.Text.RegularExpressions.Regex.Matches( this.TargetText, PATTERN );
            foreach (System.Text.RegularExpressions.Match match in matchs)
            {
                string replacement = match.Value;
                replacement = replacement.Replace( "&", AMPERSAND_UNREPLACED );
                this.TargetText = this.TargetText.Replace( match.Value, replacement );
            }
        }

        /// <summary>
        /// The method which will be invoked after you replace the string.
        /// </summary>
        public void End()
        {
            this.TargetText = this.TargetText.Replace( AMPERSAND_UNREPLACED, "&" );
        }

        /// <summary>
        /// The main process to replace the string.
        /// </summary>
        /// <param name="specialcharacters">The object of the class SpecialCharacters, which contains the special characters.</param>
        public void Replace( ClassMappings.SpecialCharacters specialcharacters )
        {
            foreach( var genre in specialcharacters.Genres )
            {
                if( !genre.CanReplce ) continue;

                foreach( var specialCharacter in genre.SpecialCharacters )
                {
                    if( !specialCharacter.CanUse ) continue;

                    // If the HTML string is empty, skip.
                    if( specialCharacter.HtmlString.Equals( string.Empty ) ) continue;

                    this.TargetText = this.TargetText.Replace( specialCharacter.Character, specialCharacter.HtmlString );
                }
            }
        }

        /// <value>The target string.</value>
        public string TargetText{ get; private set; }

        /// <value>The pattern which this object replaces.</value>
        private string PATTERN = "&[#|a-z|0-9]{1,10};";

        /// <value>The string which stands for the ampersand.</value>
        private static string AMPERSAND_UNREPLACED = "AMPERSAND_UNREPLACED";
    }
}