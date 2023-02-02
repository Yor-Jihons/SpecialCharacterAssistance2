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
        /// <summary>
        /// Raises the Startup event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup( StartupEventArgs e )
        {
            base.OnStartup( e );

            // UIスレッドの未処理例外で発生
            DispatcherUnhandledException += OnDispatcherUnhandledException;
            // UIスレッド以外の未処理例外で発生
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
            // それでも処理されない例外で発生
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        /// <summary>
        /// Occurs when an exception is thrown by an application but not handled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDispatcherUnhandledException( object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e )
        {
            var exception = e.Exception;
            HandleException(exception);
        }

        /// <summary>
        /// Occurs when a faulted task's unobserved exception is about to trigger exception escalation policy, which, by default, would terminate the process.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUnobservedTaskException( object sender, UnobservedTaskExceptionEventArgs e )
        {
            var exception = e.Exception.InnerException as Exception;
            HandleException(exception);
        }

        /// <summary>
        /// When overridden in a derived class, allows for code to run when an unhandled exception occurs in the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUnhandledException( object sender, UnhandledExceptionEventArgs e )
        {
            var exception = e.ExceptionObject as Exception;
            HandleException(exception);
        }

        /// <summary>
        /// Logs the errors.
        /// </summary>
        /// <param name="e"></param>
        private void HandleException( Exception e )
        {
            // ログを送ったり、ユーザーにお知らせしたりする
            MessageBox.Show( $"エラーが発生しました\n{e?.ToString()}" );
            Environment.Exit(1);
        }

        /// <summary>
        /// Occurs just before an application shuts down and cannot be canceled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Exit( object sender, ExitEventArgs e )
        {
            
        }
    }
}
