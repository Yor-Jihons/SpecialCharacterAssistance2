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

            string imgLogicalName = "background_wood1.png";
            this.Background = LoadImageBrushFromResource( imgLogicalName );
        }

        /// <summary>
        /// Load the brush of image from resource.
        /// </summary>
        /// <param name="imgLogicalName">The logic name of the resource.</param>
        /// <returns>The object of the class Brush.</returns>
        private Brush LoadImageBrushFromResource( string imgLogicalName )
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream( imgLogicalName );
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = bitmapImage;
        return imageBrush;
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
        private async void OpenFileMenuItem_Click( object sender, RoutedEventArgs e )
        {
            bool ret = await Task.Run( () =>{
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

                if( dialog.ShowDialog() != true ) return false;

                using( var file = new System.IO.StreamReader( dialog.FileName, new System.Text.UTF8Encoding( false ) ) )
                {
                    this.mainViewModelEx.ContentText = file.ReadToEnd();
                    file.Close();
                }
            return true;
            });

            if( ret ) textbox1.Select( this.mainViewModelEx.ContentText.Length - 1, 1 );
        }

        /// <summary>
        /// The event when SaveFileMenuItem was clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void SaveFileMenuItem_Click( object sender, RoutedEventArgs e )
        {
            string text = this.mainViewModelEx.ContentText;

            Task.Run( () =>{
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

                MessageBox.Show( dialog.FileName + "に書き込み完了!" );
            });
        }

        /// <summary>
        /// The event when the item of the menu. (To show the help page.)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpMenuItem_Click( object sender, System.Windows.RoutedEventArgs e )
        {
            var startInfo = new System.Diagnostics.ProcessStartInfo( "https://yorroom2.cloudfree.jp/help/ja/specialcharacterassistance2.html" );
            startInfo.UseShellExecute = true;
            System.Diagnostics.Process.Start(startInfo);
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
        private async void HtmlConversionButton_Click( object sender, RoutedEventArgs e )
        {
            await Task.Run( () => {
                var replacer = new Replacers.Replacer( this.mainViewModelEx.ContentText );
                replacer.Begin();
                replacer.Replace( this.specialcharacters );
                replacer.End();
                this.mainViewModelEx.ContentText = replacer.TargetText;
            });
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
