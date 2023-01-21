using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Threading.Tasks;

namespace SpecialCharacterAssistance2.Apps
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            System.Windows.Controls.ToolTipService.ShowDurationProperty.OverrideMetadata(
                typeof(DependencyObject),
                new FrameworkPropertyMetadata(int.MaxValue));

            // UIスレッドの未処理例外で発生
            DispatcherUnhandledException += OnDispatcherUnhandledException;
            // UIスレッド以外の未処理例外で発生
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
            // それでも処理されない例外で発生
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var exception = e.Exception;
            HandleException(exception);
        }

        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            var exception = e.Exception.InnerException as Exception;
            HandleException(exception);
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            HandleException(exception);
        }

        private void HandleException(Exception e)
        {
            // ログを送ったり、ユーザーにお知らせしたりする
            MessageBox.Show( $"エラーが発生しました\n{e?.ToString()}" );
            Environment.Exit(1);
        }

        private Mutexes.MutexEx m_mutex = new Mutexes.MutexEx( "SampleWeightManager" );

        private void ApplicationStartup( object sender, System.Windows.StartupEventArgs e )
        {
            // ミューテックスの所有権を要求
            if( m_mutex.HasAlreadyRun() )
            {
                System.Windows.MessageBox.Show( "すでに起動しています!!!" );
                Mutexes.MutexEx.MoveForeground();
                this.Shutdown( -1 );
            }
        }

        private void Application_Exit( object sender, ExitEventArgs e )
        {
            m_mutex.Destruct();
        }
    }
}
