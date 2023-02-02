using NLog;

namespace SpecialCharacterAssistance2.Loggers
{
    public class Logger
    {
        /// <summary>
        /// Initializes the object of the NLog. You should invoke this method before you use this class.
        /// </summary>
        /// <param name="logName">The name for log.</param>
        /// <param name="filepath">The file name for log.</param>
        public static void SetLogInfo( string logName, string filepath )
        {
            var config = new NLog.Config.LoggingConfiguration();

            var logfile = new NLog.Targets.FileTarget( logName ){ FileName = filepath };
            var logconsole = new NLog.Targets.ConsoleTarget( "logconsole" );

            config.AddRule( NLog.LogLevel.Info, NLog.LogLevel.Fatal, logconsole );
            config.AddRule( NLog.LogLevel.Debug, NLog.LogLevel.Fatal, logfile );

            NLog.LogManager.Configuration = config;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Logger()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Writes the diagnostic message at the Info level.
        /// </summary>
        /// <param name="text">The text you want to log.</param>
        public void Info( string text )
        {
            this.logger.Info( text );
        }

        /// <summary>
        /// Writes the diagnostic message at the Trace level.
        /// </summary>
        /// <param name="text">The text you want to log.</param>
        public void Trace( string text )
        {
            this.logger.Trace( text );
        }

        /// <summary>
        /// Writes the diagnostic message at the Debug level.
        /// </summary>
        /// <param name="text">The text you want to log.</param>
        public void Debug( string text )
        {
            this.logger.Debug( text );
        }

        /// <summary>
        /// Writes the diagnostic message at the Error level.
        /// </summary>
        /// <param name="text">The text you want to log.</param>
        public void Error( string text )
        {
            this.logger.Error( text );
        }

        /// <summary>
        /// Writes the diagnostic message at the Fatal level.
        /// </summary>
        /// <param name="text">The text you want to log.</param>
        public void Fatal( string text )
        {
            this.logger.Fatal( text );
        }

        /// <value>The object of the class NLog.Logger, in order to log.</value>
        private NLog.Logger logger;
    }
}