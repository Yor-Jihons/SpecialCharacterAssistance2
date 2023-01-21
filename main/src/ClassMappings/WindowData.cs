namespace SpecialCharacterAssistance2.ClassMappings
{
    [System.Xml.Serialization.XmlRoot("WindowData")]
    public class WindowData
    {
        /// <summary>
        /// Create an object of this class by applying the Factory-pattern.
        /// </summary>
        /// <param name="filepath">The path of the info file.</param>
        /// <returns>An object of this class.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.IO.DirectoryNotFoundException"></exception>
        /// <exception cref="System.IO.PathTooLongException"></exception>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public static WindowData Load( string filepath )
        {
            if( !System.IO.File.Exists( filepath ) ) return null;

            var fs = new System.IO.FileStream( filepath, System.IO.FileMode.Open );
            var serializer = new System.Xml.Serialization.XmlSerializer( typeof(WindowData) );
            var seed = (WindowData)serializer.Deserialize( fs );
            fs.Close();
        return seed;
        }

        public static WindowData CreateAsNew( int width, int height )
        {
            var obj        = new WindowData();
            obj.MainWindow = new WindowInfo();

            obj.MainWindow.Width  = width;
            obj.MainWindow.Height = height;
        return obj;
        }

        public static void Save( WindowData infos, string filepath )
        {
            var fs = new System.IO.FileStream( filepath, System.IO.FileMode.Create );
            var serializer = new System.Xml.Serialization.XmlSerializer( typeof(WindowData) );
            serializer.Serialize( fs, infos );
            fs.Close();
        }

        [System.Xml.Serialization.XmlElement("mainwindow")]
        public WindowInfo MainWindow{ get; set; }
    }
}