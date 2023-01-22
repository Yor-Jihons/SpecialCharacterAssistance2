namespace SpecialCharacterAssistance2.ClassMappings
{
    public class SpecialCharacters
    {
        public static string CreateJsonFilePath()
        {
            return System.IO.Path.Join(
                System.IO.Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString(),
                "specialcharacters", "specialcharacters.json"
            );
        }

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

        public System.Collections.Generic.List<Genre> Genres { get; set; }
    }
}