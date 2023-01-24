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

        /// <value>The string for the textbox.</value>
        private string contentText;

        /// <value>The string for the textbox. (for the data-binding.)</value>
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

        /// <value>The PropertyChanged.</value>
        public event PropertyChangedEventHandler PropertyChanged = null;

        /// <summary>
        /// Notify the property was changed (to ViewModel and View).
        /// </summary>
        /// <param name="propertyName">The property name, which was changed.</param>
        protected void NotifyPropertyChanged( string propertyName )
        {
            if( PropertyChanged != null )
            {
                this.PropertyChanged?.Invoke( this, new PropertyChangedEventArgs(propertyName) );
            }
        }
    }
}