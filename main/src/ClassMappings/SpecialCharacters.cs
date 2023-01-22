namespace SpecialCharacterAssistance2.ClassMappings
{
    public class specialcharacters
    {
        public static string CreateJsonFilePath()
        {
            return System.IO.Path.Join(
                System.IO.Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString(),
                "specialcharacters", "specialcharacters.json"
            );
        }

        public static specialcharacters Load( string filepath )
        {
            // 1. ファイルを読み込む
            // 2. jsonからオブジェクトに変換する
            // 3. (2)をreturnする
        return null;
        }

        public Genre[] Genres{ get; set; }
    }
}