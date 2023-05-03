using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SymBank
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class MyApp : Application
    {
        private static MyApp _instance;
        public static MyApp Instance { get { return _instance; } }

        private static IPrincipal _principal;

        public MyApp()
        {
            _instance = this;
            //ShutdownMode = ShutdownMode.OnMainWindowClose;
            //StartupUri = new Uri("Shell.xaml", UriKind.Relative);
            //Startup += Application_Startup;
            //Activated += Application_Activated;
            //Deactivated += Application_Deactivated;
            //DispatcherUnhandledException += Application_DispatcherUnhandledException;
            //SessionEnding += Application_SessionEnding;
            //Exit += Application_Exit;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //    var identity = WindowsIdentity.GetCurrent();
            //    _principal = new WindowsPrincipal(identity);
            AppDomain.CurrentDomain.SetPrincipalPolicy(
                PrincipalPolicy.WindowsPrincipal);
            _principal = Thread.CurrentPrincipal;
            //if (!IsInAnyRoles("Administrators", "Banking"))
            //    throw new Exception("Access denied.");
        }

        public static string UserName
        {
            get { return _principal.Identity.Name; }
        }

        public static bool IsInRole(string roleName)
        {
            return _principal.IsInRole(roleName);
        }

        public static bool IsInAnyRoles(params string[] roleNames)
        {
            foreach (var roleName in roleNames)
                if (_principal.IsInRole(roleName)) return true;
            return false;
        }
        public static bool IsInAllRoles(params string[] roleNames)
        {
            foreach (var roleName in roleNames)
                if (!_principal.IsInRole(roleName)) return false;
            return true;
        }


        //private void Application_Activated(object sender, EventArgs e)
        //{
        //    Debug.WriteLine("Application activated!");
        //}


        //private void Application_Deactivated(object sender, EventArgs e)
        //{
        //    Debug.WriteLine("Application deactivated!");
        //}

        public static void Log(string type, string message)
        {
            File.AppendAllText("SymBank.log",
                $"[{DateTime.Now:yy-MM-dd hh:mm:ss}] {type}(\r\n{message}\r\n)\r\n");
        }

        public static void LogMessage(string message) {
            Log("MESSAGE", message);
        }

        public static void LogWarning(string message) {
            Log("WARNING", message);
        }

        public static void LogFailure(string message)
        {
            Log("FAILURE", message);
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var ex = e.Exception;
            if (Debugger.IsAttached) Debugger.Break(); else
            {
                LogFailure(ex.ToString());
                MessageBox.Show(string.Format(
                    "A serious error has occurred.\n" +
                    "Please contact the administrator.\n" +
                        "The error is: {0}\n", ex.Message),
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        //private void Application_SessionEnding(object sender, SessionEndingCancelEventArgs e)
        //{
        //    switch (e.ReasonSessionEnding)
        //    {
        //        case ReasonSessionEnding.Shutdown:
        //            Debug.WriteLine("System is shutting down!");
        //            break;
        //        case ReasonSessionEnding.Logoff:
        //            Debug.WriteLine("User is logging off!");
        //            break;
        //    }

        //    e.Cancel = true;
        //}

        //private void Application_Exit(object sender, ExitEventArgs e)
        //{
        //    var exitCode = e.ApplicationExitCode;
        //    Debug.WriteLine($"Application exiting with exit code {exitCode}!");
        //}
    }
}
