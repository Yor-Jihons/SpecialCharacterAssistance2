using System.ComponentModel;

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
        public MainViewModelEx(){
            this.ContentText = "";
        }

        private string contentText;
        public string ContentText
        {
            get
            {
                return this.contentText;
            }
            set
            {
                this.contentText = value;
                // 通知する
                this.NotifyPropertyChanged( nameof(ContentText) );
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = null;
        protected void NotifyPropertyChanged( string propertyName )
        {
            if( PropertyChanged != null )
            {
                this.PropertyChanged?.Invoke( this, new PropertyChangedEventArgs(propertyName) );
            }
        }
    }
}