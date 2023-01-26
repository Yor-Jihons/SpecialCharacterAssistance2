namespace SpecialCharacterAssistance2.Replacers
{
    class Replacer
    {
        public Replacer( string text )
        {
            this.TargetText = text;
        }

        public void Begin()
        {
            //this.TargetText = this.TargetText.Replace( "ab", "<>" );
            var matchs = System.Text.RegularExpressions.Regex.Matches( this.TargetText, PATTERN );
            foreach (System.Text.RegularExpressions.Match match in matchs)
            {
                string replacement = match.Value;
                replacement = replacement.Replace( "&", AMPERSAND_UNREPLACED );
                this.TargetText = this.TargetText.Replace( match.Value, replacement );
            }
        }

        public void End()
        {
            this.TargetText = this.TargetText.Replace( AMPERSAND_UNREPLACED, "&" );
        }

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

        public string TargetText{ get; private set; }

        private string PATTERN = "&[#|a-z|0-9]{1,10};";

        private static string AMPERSAND_UNREPLACED = "AMPERSAND_UNREPLACED";
    }
}