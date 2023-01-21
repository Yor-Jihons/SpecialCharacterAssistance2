using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpecialCharacterAssistance2.Forms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.typeComboBox.Items.Add( "ギリシャアルファベット(大文字)" );
        }

        private void MainWindow_Loaded(object sender, EventArgs args)
        {
            ClassMappings.WindowData data = ClassMappings.WindowData.Load( "data1.xml" );
            if( data == null )
            {
                data = ClassMappings.WindowData.CreateAsNew( (int)this.Width, (int)this.Height );
            }
            ClassMappings.WindowData.Save( data, "data1.xml" );
            // TODO:
            //MessageBox.Show( data.MainWindow.ToString() );
        }

        private void HtmlConversionButton_Click( object sender, RoutedEventArgs s ){
            MessageBox.Show( "h = " + this.Height + ", w = " + this.Width );
        }
    }
}
