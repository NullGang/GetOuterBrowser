using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetOuterBrowser
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //sistema de comprobacion de archivos
            //lista de archivos (si lo modificas pon los nuevos DLLS que uses aqui.)
            List<string> fileNames = new List<string>
            {
                "Guna.UI.dll", 
                "Guna.UI2.dll",
                "Microsoft.Web.WebView2.Core.dll",
                "Microsoft.Web.WebView2.WinForms.dll",
                "Microsoft.Web.WebView2.Wpf.dll",
                "Newtonsoft.Json.dll",
                "System.Diagnostics.DiagnosticSource.dll" 
                
            };
            //lista de carpetas (no es necesario la de webview2, se genera automaticamente)
            List<string> folderNames = new List<string>
            {
                "runtimes", 
               // "GetOuterBrowser.exe.WebView2" || se genera automaticamente, no es necesario.
            };

            //donde esta el exe (quiza era mas facil un Application.StartupPath)
            string exePath = AppDomain.CurrentDomain.BaseDirectory; 
            //lo que falta de tal.
            List<string> missingFiles = new List<string>();
            List<string> missingFolders = new List<string>();

            //pa cada uno.
            foreach (string fileName in fileNames)
            {
                string filePath = Path.Combine(exePath, fileName); 
                if (!File.Exists(filePath))
                {
                    missingFiles.Add(fileName); 
                }
            }

            foreach (string folderName in folderNames)
            {
                string folderPath = Path.Combine(exePath, folderName); 
                if (!Directory.Exists(folderPath))
                {
                    missingFolders.Add(folderName); 
                }
            }

            //PD: no se puede iniciar la aplicacion si faltan archivos (por si peta)
            if (missingFiles.Count > 0 || missingFolders.Count > 0)
            {
                string missingMessage = "Missing\n";

                if (missingFiles.Count > 0)
                {
                    missingMessage += "Files:\n" + string.Join("\n", missingFiles) + "\n";
                }

                if (missingFolders.Count > 0)
                {
                    missingMessage += "Folders:\n" + string.Join("\n", missingFolders);
                }

                MessageBox.Show(missingMessage + " \nTry reinstalling the application or contacting support.", "Missing Elements. Try reinstalling the application.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //furulaaaa
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        

          
        }
    }
}
