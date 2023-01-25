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
        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            this.mainViewModelEx = new ViewModels.MainViewModelEx();

            this.DataContext = this.mainViewModelEx;

            var jsonfilePath = ClassMappings.SpecialCharacters.CreateJsonFilePath();

            this.specialcharacters = ClassMappings.SpecialCharacters.Load( jsonfilePath );

            foreach( var genre in specialcharacters.Genres ){
                this.typeComboBox.Items.Add( genre.Name );
            }
            this.typeComboBox.SelectedIndex = 0;

            this.wrapPanels = CreateWrapPanels(
                this.specialcharacters,
                SpecialCharacterButton_Click,
                (Style)this.FindResource( "Font4Buttons" )
            );

            // イメージブラシの作成
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new System.Windows.Media.Imaging.BitmapImage(
                new Uri( "res/Frames/wood1.png", UriKind.RelativeOrAbsolute )
            );
            //imageBrush.Opacity = 0;

            // ブラシを背景に設定する
            this.Background = imageBrush;
        }

        /// <summary>
        /// The event when MainWindow closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MainWindow_Loaded( object sender, EventArgs args )
        {
            this.MinWidth  = this.Width;
            this.MinHeight = this.Height;

            this.TypeComboBox_SelectionChanged( typeComboBox, null );

            textbox1.Select( 0, 0 );
        }

        /// <summary>
        /// The event when OpenFileMenuItem was clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OpenFileMenuItem_Click( object sender, RoutedEventArgs e )
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();

            dialog.FileName         = "Document";
            dialog.DefaultExt       = ".txt";
            dialog.Filter           = "テキストファイル (*.txt)|*.txt|全てのファイル (*.*)|*.*";
            dialog.FilterIndex      = 1;
            dialog.InitialDirectory = "";
            dialog.AddExtension     = true;
            dialog.CheckFileExists  = true;
            dialog.CheckPathExists  = true;
            dialog.Multiselect      = false;

            if( dialog.ShowDialog() != true ) return;

            using( var file = new System.IO.StreamReader( dialog.FileName, new System.Text.UTF8Encoding( false ) ) )
            {
                this.mainViewModelEx.ContentText = file.ReadToEnd();
                file.Close();
            }
        }

        /// <summary>
        /// The event when SaveFileMenuItem was clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void SaveFileMenuItem_Click( object sender, RoutedEventArgs e )
        {
            string text = this.mainViewModelEx.ContentText;

            var dialog = new Microsoft.Win32.SaveFileDialog();

            dialog.FileName         = "special_character_assistance.txt";
            dialog.DefaultExt       = ".txt";
            dialog.Filter           = "テキストファイル (*.txt)|*.txt|全てのファイル (*.*)|*.*";
            dialog.FilterIndex      = 1;
            dialog.InitialDirectory = "";
            dialog.AddExtension     = true;
            dialog.DereferenceLinks = true;
            // ユーザーが既に存在するファイル名を指定した場合に SaveFileDialog で警告を表示するかどうか
            dialog.OverwritePrompt  = true;

            if( dialog.ShowDialog() != true ) return;

            using( var file = new System.IO.StreamWriter( dialog.FileName, false, new System.Text.UTF8Encoding( false ) ) )
            {
                file.Write( text );
                file.Close();
            }
        }

        /// <summary>
        /// The event when the selected index of typeComboBox was changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void TypeComboBox_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            int selectedIndex = (sender as ComboBox).SelectedIndex;
            if( selectedIndex == -1 ) return;

            if( wrapPanels == null ) return;

            if( prevWrapPanel != null ) stackPanel1.Children.Remove( prevWrapPanel );

            stackPanel1.Children.Add( wrapPanels[ selectedIndex ] );

            this.prevWrapPanel = wrapPanels[ selectedIndex ];
        }

        /// <summary>
        /// The event when HtmlConversionButton was clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void HtmlConversionButton_Click( object sender, RoutedEventArgs e )
        {
            int caretPos = textbox1.SelectionStart;

            foreach( var genre in this.specialcharacters.Genres )
            {
                if( !genre.CanReplce ) continue;

                foreach( var specialCharacter in genre.SpecialCharacters )
                {
                    if( !specialCharacter.CanUse ) continue;

                    this.mainViewModelEx.ContentText = this.mainViewModelEx.ContentText.Replace( specialCharacter.Character, specialCharacter.HtmlString );
                }
            }

            textbox1.Select( this.mainViewModelEx.ContentText.Length - 1, 1 );
        }

        /// <summary>
        /// The event when buttons (of special characters) was clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void SpecialCharacterButton_Click( object sender, RoutedEventArgs e )
        {
            int caretPos = textbox1.SelectionStart;
            this.mainViewModelEx.ContentText = this.mainViewModelEx.ContentText.Insert( caretPos, (sender as Button).Content.ToString() );
            textbox1.Select( caretPos + 1, 1 );
        }

        /// <summary>
        /// Create the list of the class WrapPanel, in order to be able to switch them.
        /// </summary>
        /// <param name="specialcharacters">The object of the class SpecialCharacters, which contains the json data.</param>
        /// <param name="handler">The event buttons was clicked.</param>
        /// <param name="resource">The font resource for buttons.</param>
        /// <returns>The list of the class WrapPanel.</returns>
        private static List<WrapPanel> CreateWrapPanels( ClassMappings.SpecialCharacters specialcharacters, RoutedEventHandler handler, Style resource )
        {
            var wrapPanels = new List<WrapPanel>();

            foreach( var genre in specialcharacters.Genres )
            {
                var wrapPanel = new WrapPanel();
                NameScope.SetNameScope( wrapPanel, new NameScope() );
                wrapPanels.Add( wrapPanel );

                foreach( var specialcharacter in genre.SpecialCharacters )
                {
                    if( !specialcharacter.CanUse ) continue;

                    var button = new Button()
                    {
                        Name    = ("c" + specialcharacter.Id),
                        Content = specialcharacter.Character,
                        Style   = resource,
                        Margin  = new Thickness( 5, 5, 5, 5 ),
                        Width   = 80,
                        Height  = 60
                    };
                    button.Click += new RoutedEventHandler( handler );

                    wrapPanel.Children.Add( button );
                }
            }
        return wrapPanels;
        }

        /// <value>The View model.</value>
        private ViewModels.MainViewModelEx mainViewModelEx;

        /// <value>The object of the class SpecialCharacters which contains the json data.</value>
        private ClassMappings.SpecialCharacters specialcharacters;

        /// <value>The list of the class WrapPanel in order to switch.</value>
        private List<WrapPanel> wrapPanels;

        private WrapPanel prevWrapPanel;
    }
}
