namespace SpecialCharacterAssistance2.ClassMappings
{
    /// <summary>
    /// The class to contain the data from a json-file.(as a root)
    /// </summary>
    public class SpecialCharacters
    {
        /// <summary>
        /// Create the json file path.
        /// </summary>
        /// <returns>The path as a subdirectory "./specialcharacters/specialcharacters.json".</returns>
        public static string CreateJsonFilePath()
        {
            return System.IO.Path.Join(
                System.IO.Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString(),
                "specialcharacters", "specialcharacters.json"
            );
        }

        /// <summary>
        /// The factory in order to create an object of this class.
        /// </summary>
        /// <param name="filepath">The json file path.</param>
        /// <returns>The object of this class which was made from the data contained in the json-file.</returns>
        public static SpecialCharacters Load( string filepath )
        {
            using(var file = new System.IO.StreamReader( filepath, System.Text.Encoding.GetEncoding("UTF-8") ))
            {
                var jsonStr = file.ReadToEnd();
                file.Close();
                var options = new System.Text.Json.JsonSerializerOptions{ WriteIndented = true };
                return System.Text.Json.JsonSerializer.Deserialize(
                    jsonStr,
                    typeof(SpecialCharacterAssistance2.ClassMappings.SpecialCharacters),
                    options
                ) as SpecialCharacterAssistance2.ClassMappings.SpecialCharacters;
            }
        }

        /// <value>The list of the class Genre.</value>
        public System.Collections.Generic.List<Genre> Genres { get; set; }
    }
}