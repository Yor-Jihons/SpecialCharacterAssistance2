using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SpecialCharacterAssistance2.ViewModels
{
    /// <summary>
    /// The class in order to express the ViewModel.
    /// </summary>
    public class MainViewModelEx : INotifyPropertyChanged
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MainViewModelEx()
        {
            this.ContentText = "";
            this.InitCommands();
        }

        private void InitCommands()
        {
            this.OpenFileCommand = new RelayCommand( async (p) => await OpenFileAsync() );
            this.SaveFileCommand = new RelayCommand( (p) => SaveFile() );
            this.HtmlConversionCommand = new RelayCommand( async (p) => await ConvertToHtmlAsync() );
        }

        /// <value>The string for the textbox.</value>
        private string contentText;

        /// <value>The string for the textbox. (for the data-binding.)</value>
        public string ContentText
        {
            get => this.contentText;
            set
            {
                this.contentText = value;
                this.NotifyPropertyChanged( nameof(ContentText) );
            }
        }

        // Commands
        public ICommand OpenFileCommand { get; private set; }
        public ICommand SaveFileCommand { get; private set; }
        public ICommand HtmlConversionCommand { get; private set; }

        // Data (ジャンル情報は本来ここにあるべき)
        public ClassMappings.SpecialCharacters SpecialCharactersData { get; set; }

        private async Task OpenFileAsync()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "Document",
                DefaultExt = ".txt",
                Filter = "テキストファイル (*.txt)|*.txt|全てのファイル (*.*)|*.*",
                CheckFileExists = true
            };

            if (dialog.ShowDialog() == true)
            {
                using (var file = new System.IO.StreamReader(dialog.FileName, new System.Text.UTF8Encoding(false)))
                {
                    this.ContentText = await file.ReadToEndAsync();
                }
            }
        }

        private void SaveFile()
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "special_character_assistance.txt",
                DefaultExt = ".txt",
                Filter = "テキストファイル (*.txt)|*.txt|全てのファイル (*.*)|*.*",
                OverwritePrompt = true
            };

            if (dialog.ShowDialog() == true)
            {
                using (var file = new System.IO.StreamWriter(dialog.FileName, false, new System.Text.UTF8Encoding(false)))
                {
                    file.Write(this.ContentText);
                }
                MessageBox.Show(dialog.FileName + "に書き込み完了!");
            }
        }

        private async Task ConvertToHtmlAsync()
        {
            if (this.SpecialCharactersData == null) return;

            await Task.Run(() =>
            {
                var replacer = new Replacers.Replacer(this.ContentText);
                replacer.Begin();
                replacer.Replace(this.SpecialCharactersData);
                replacer.End();
                this.ContentText = replacer.TargetText;
            });
        }

        /// <value>The PropertyChanged.</value>
        public event PropertyChangedEventHandler PropertyChanged = null;

        /// <summary>
        /// Notify the property was changed (to ViewModel and View).
        /// </summary>
        /// <param name="propertyName">The property name, which was changed.</param>
        protected void NotifyPropertyChanged( string propertyName )
        {
            this.PropertyChanged?.Invoke( this, new PropertyChangedEventArgs(propertyName) );
        }
    }
}