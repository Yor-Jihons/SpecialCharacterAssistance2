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
            this.typeComboBox.SelectedIndex = 0;

            var wrapPanels = CreateWrapPanels( SpecialCharacterButton_Click, (Style)this.FindResource( "Font4Buttons" ) );

            stackPanel1.Children.Add( wrapPanels[0] );

            //stackPanel1.Children.Remove( wrapPanel1 );
        }

        private static List<WrapPanel> CreateWrapPanels( RoutedEventHandler handler, Style resource )
        {

            var wrapPanels = new List<WrapPanel>();

            {
                var wrapPanel1 = new WrapPanel();
                NameScope.SetNameScope( wrapPanel1, new NameScope() );
                wrapPanels.Add( wrapPanel1 );

                var button1 = new Button(){
                    Name = "button1", Content = "д", Style  = resource,
                    Margin = new Thickness( 5, 5, 5, 5 ),
                    Width  = 80, Height = 60
                };
                button1.Click += new RoutedEventHandler( handler );
                wrapPanel1.Children.Add( button1 );

                var button2 = new Button(){
                    Name = "button1", Content = "阿", Style  = resource,
                    Margin = new Thickness( 5, 5, 5, 5 ),
                    Width  = 80, Height = 60
                };
                button2.Click += new RoutedEventHandler( handler );
                wrapPanel1.Children.Add( button2 );
            }

        return wrapPanels;
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

        private void OpenFileMenuItem_Click( object sender, RoutedEventArgs s )
        {

        }

        private void SaveFileMenuItem_Click( object sender, RoutedEventArgs s )
        {

        }

        private void HtmlConversionButton_Click( object sender, RoutedEventArgs s )
        {
            MessageBox.Show( "『" + (sender as Button).Content + "』が押された" );
        }

        private void SpecialCharacterButton_Click( object sender, RoutedEventArgs s )
        {
            MessageBox.Show( "『" + (sender as Button).Content + "』が押された" );
        }
    }
}
