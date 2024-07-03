using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace VEvil.Core {

    /// <summary>
    /// Log types used by the <see cref="Logger"/>.
    /// </summary>
    internal enum ELogType : byte {
        TRACE = 0x0,
        WARNING,
        ERROR,
        EXCEPTION,
    }

    /// <summary>
    /// Wrapper for stylizing the <see cref="Debug"/> log calls provided by Unity
    /// and allow to save them in a file at runtime.
    /// </summary>
    /// <remarks>
    /// The following preprocessor definitions must be active such as "<i>LOGGER_ENABLE</i>" to authorize log calls
    /// and "<i>LOGGER_ENABLE_FILE</i>" to authorize runtime logging on a file.
    /// </remarks>
    public static class Logger {

        #region Attributes

        /// <summary>
        /// The color of the trace Debug.Log messages.
        /// </summary>
        private const string TRACE_COLOR = nameof(Color.white);
        /// <summary>
        /// The color of the trace Debug.LogWarning messages.
        /// </summary>
        private const string WARNING_COLOR = nameof(Color.yellow);
        /// <summary>
        /// The color of the trace Debug.LogError messages.
        /// </summary>
        private const string ERROR_COLOR = nameof(Color.red);
        
        /// <summary>
        /// The <see cref="StreamWriter"/> of the log file.
        /// </summary>
        private static StreamWriter logWriter = null;
        /// <summary>
        /// The <see cref="FileStream"/> of the log file.
        /// </summary>
        private static FileStream logFileStream = null;
        
        /// <summary>
        /// Is the <see cref="Logger"/> able to write log files.
        /// </summary>
        private static bool canWriteLog = false;

        #endregion

        #region Logger's Initialization Method

        /// <summary>
        /// Initialize the <see cref="Logger"/>.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)] private static void Initialize() {
#if !UNITY_EDITOR && LOGGER_ENABLE_FILE
                SetupLogFile();
                
                Application.quitting += () => { logWriter?.Close(); };
#endif
        }

        #endregion

        #region Logger's Internal Methods

        /// <summary>
        /// Format a Log message with the correct color.
        /// </summary>
        private static string FormatMessage(string _color, object _message) {
#if UNITY_EDITOR //In editor, format it with colors for the Console Window
            return $"<color={_color}>{_message}</color>";
#else //In builds, console application, ...
            return Encoding.UTF8.GetString(Encoding.UTF8.GetBytes($"[{GetTimestamp(DateTime.Now)}] {_message}"));
#endif
        }
        
        /// <summary>
        /// Format a Log message with the correct color and category.
        /// </summary>
        private static string FormatMessageWithCategory(string _color, string _category, object _message) {
#if UNITY_EDITOR //In editor, format it with colors for the Console Window
            return $"<color={_color}><b>[{_category}]</b> {_message}</color>";
#else //In builds, console application, ...
            return Encoding.UTF8.GetString(Encoding.UTF8.GetBytes($"[{GetTimestamp(DateTime.Now)}] [{_category}] {_message}"));
#endif
        }

        /// <summary>
        /// Converts a <see cref="DateTime"/> object to a formatted timestamp string.
        /// </summary>
        /// <param name="_time">The time to format.</param>
        /// <returns>The formatted timestamp.</returns>
        private static string GetTimestamp(DateTime _time) {
            return _time.ToString("HH:mm:ss");
        }

        /// <summary>
        /// Setup the Log Path/File.
        /// </summary>
        [Conditional("LOGGER_ENABLE_FILE")] private static void SetupLogFile() {
            string _logFilePath = $"{Application.dataPath}/Log_Latest.txt"; //The Log files' path
            FileInfo _logFileInfo = new FileInfo(_logFilePath); //Set the FileInfo
            DirectoryInfo _logDirectoryInfo = null;
            
            if(_logFileInfo.DirectoryName != null) {
                _logDirectoryInfo = new DirectoryInfo(_logFileInfo.DirectoryName);
            } else {
                TraceError("Logger", "Unable to write logs into a file!");
                return;
            }
            
            //If the directory doesn't exists, create it.
            if(!_logDirectoryInfo.Exists) _logDirectoryInfo.Create();
            
            if(!_logFileInfo.Exists) { //If the file doesn't exists
                logFileStream = _logFileInfo.Create();
            } else {
                //logFileStream = new FileStream(_logFilePath, FileMode.Create);
                logFileStream = _logFileInfo.Create();
            }
            
            canWriteLog = true;
            logWriter = new StreamWriter(logFileStream);
            
            //Write a basic log setup
            logWriter.WriteLine($"{Application.productName} - {Application.companyName}  ({Application.version} - {Application.platform.ToString()})");
            logWriter.WriteLine($"Output log file ({DateTime.Now} - {GetTimestamp(DateTime.Now)})");
            logWriter.WriteLine($"-------------------------------------------------------------------------------------------------------------------------");
        }
        
        [Conditional("LOGGER_ENABLE_FILE")] private static void WriteOnFile(ELogType _logType, string _message, string _category = "- - -") {
            if(!canWriteLog) return;
            
            logWriter.WriteLine($"[{DateTime.Now:HH:mm:ss}] [{_logType}] [{_category}] {_message}");
        }
        
        [Conditional("LOGGER_ENABLE_FILE")] private static void WriteOnFile(ELogType _logType, string _message) {
            if(!canWriteLog) return;
            
            logWriter.WriteLine($"[{DateTime.Now:HH:mm:ss}] [{_logType}] {_message}");
        }
        
        #endregion
        
        #region Logger's External Methods

        [Conditional("LOGGER_ENABLE")] public static void Trace(object _message) {
            Debug.Log(FormatMessage(TRACE_COLOR, _message));
            WriteOnFile(ELogType.TRACE, _message.ToString());
        }

        [Conditional("LOGGER_ENABLE")] public static void Trace(string _category, object _message) {
            Debug.Log(FormatMessageWithCategory(TRACE_COLOR, _category, _message));
            WriteOnFile(ELogType.TRACE, _message.ToString(), _category);
        }

        [Conditional("LOGGER_ENABLE")] public static void TraceFormat(string _format, params object[] _args) {
            Debug.Log(FormatMessage(TRACE_COLOR, string.Format(_format, _args)));
            WriteOnFile(ELogType.TRACE, string.Format(_format, _args));
        }

        [Conditional("LOGGER_ENABLE")] public static void TraceFormat(string _category, string _format, params object[] _args) {
            Debug.Log(FormatMessageWithCategory(TRACE_COLOR, _category, string.Format(_format, _args)));
            WriteOnFile(ELogType.TRACE, string.Format(_format, _args), _category);
        }

        [Conditional("LOGGER_ENABLE")] public static void TraceWarning(object _message) {
            Debug.LogWarning(FormatMessage(WARNING_COLOR, _message));
            WriteOnFile(ELogType.WARNING, _message.ToString());
        }

        [Conditional("LOGGER_ENABLE")] public static void TraceWarning(string _category, object _message) {
            Debug.LogWarning(FormatMessageWithCategory(WARNING_COLOR, _category, _message));
            WriteOnFile(ELogType.WARNING, _message.ToString());
        }

        [Conditional("LOGGER_ENABLE")] public static void TraceWarningFormat(string _format, params object[] _args) {
            Debug.LogWarningFormat(FormatMessage(WARNING_COLOR, string.Format(_format, _args)));
            WriteOnFile(ELogType.WARNING, string.Format(_format, _args));
        }

        [Conditional("LOGGER_ENABLE")] public static void TraceWarningFormat(string _category, string _format, params object[] _args) {
            Debug.LogWarningFormat(FormatMessageWithCategory(WARNING_COLOR, _category, string.Format(_format, _args)));
            WriteOnFile(ELogType.WARNING, string.Format(_format, _args), _category);
        }

        [Conditional("LOGGER_ENABLE")] public static void TraceError(object _message) {
            Debug.LogError(FormatMessage(ERROR_COLOR, _message));
            WriteOnFile(ELogType.ERROR, _message.ToString());
        }

        [Conditional("LOGGER_ENABLE")] public static void TraceError(string _category, object _message) {
            Debug.LogError(FormatMessageWithCategory(ERROR_COLOR, _category, _message));
            WriteOnFile(ELogType.ERROR, _message.ToString(), _category);
        }

        [Conditional("LOGGER_ENABLE")] public static void TraceErrorFormat(string _format, params object[] _args) {
            Debug.LogErrorFormat(FormatMessage(ERROR_COLOR, string.Format(_format, _args)));
            WriteOnFile(ELogType.ERROR, string.Format(_format, _args));
        }

        [Conditional("LOGGER_ENABLE")] public static void TraceErrorFormat(string _category, string _format, params object[] _args) {
            Debug.LogErrorFormat(FormatMessageWithCategory(ERROR_COLOR, _category, string.Format(_format, _args)));
            WriteOnFile(ELogType.ERROR, string.Format(_format, _args), _category);
        }

        [Conditional("LOGGER_ENABLE")] public static void TraceException(Exception _exception) {
            Debug.LogError(FormatMessage(ERROR_COLOR, _exception.Message));
        }

        [Conditional("LOGGER_ENABLE")] public static void TraceException(string _category, Exception _exception) {
            Debug.LogError(FormatMessageWithCategory(ERROR_COLOR, _category, _exception.Message));
        }

        #endregion
        
    }

}