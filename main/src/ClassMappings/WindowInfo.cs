namespace SpecialCharacterAssistance2.ClassMappings
{
    public class WindowInfo
    {
        public WindowInfo()
        {

        }

        public override string ToString()
        {
            return ("X = " + this.X + ", Y = " + this.Y + "\nWidth = " + this.Width + ", Height = " + this.Height);
        }

        [System.Xml.Serialization.XmlElement("x")]
        public int X{ get; set; } = 0;

        [System.Xml.Serialization.XmlElement("y")]
        public int Y{ get; set; } = 0;

        [System.Xml.Serialization.XmlElement("width")]
        public int Width{ get; set; } = 0;

        [System.Xml.Serialization.XmlElement("height")]
        public int Height{ get; set; } = 0;
    }
}