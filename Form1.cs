using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using System.Security.Policy;
using System.IO;
using System.Windows.Media;
using System.Net;

namespace GetOuterBrowser
{
    //SI HE USADO EL WEBVIEW, Y QUE?
    public partial class Form1 : Form
    {
        //no se utiliza para nada esta funcion, quiza en el futuro.
        public bool loaded = false;
        //para el archivo del GETOUT
        static string tempFolderPath = Path.Combine(Path.GetTempPath());

        public Form1()
        {
            InitializeComponent();
        }

        static async Task DescargarArchivoAsync(string url, string rutaDestino)
        {
            using (WebClient cliente = new WebClient())
            {
                await cliente.DownloadFileTaskAsync(new Uri(url), rutaDestino);
            }
        }


        private void gunaTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
      
        private async void Form1_Load(object sender, EventArgs e)
        {
            // gunaTextBox1.Text = webBrowser1.Url.ToString();
            loaded = true;
            //descarga el GETOUT
            await webView21.EnsureCoreWebView2Async(null);
            string getout = Path.Combine(tempFolderPath, "getout.mp3");
            //cambia esto por tu backend, no quiero que se filtre el mio.
            await DescargarArchivoAsync("https://spoofapi.onrender.com/getout", getout);


        }


        private void gunaTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            // esto es para cuando se da al enter.
            if (e.KeyCode == Keys.Enter && webView21 != null && webView21.CoreWebView2 != null)
            {
                //webBrowser1.Navigate(gunaTextBox1.Text);
                //si no pones el https da error asique comprobamos que se ponga y si no que se busque con el duckduckgo
               if(gunaTextBox1.Text.Contains("https://") || gunaTextBox1.Text.Contains("http://"))
                {
                    try
                    {
                        //no funciona esta funcion bien
                        if (gunaTextBox1.Text.Contains("http://")) { webView21.CoreWebView2.ExecuteScriptAsync($"alert('This website is not safe, try an https link')"); }

                        webView21.CoreWebView2.Navigate(gunaTextBox1.Text);
                    }
                    catch (Exception error)
                    {
                        //99% de probabilidad de q nunca que vaya a salir este error si no lo fuerzas.
                        MessageBox.Show("Error. URL is not working.");
                    }
                } else
                {
                    try
                    {
                        //no era tan dificl
                        if(gunaTextBox1.Text.ToLower() == "getout")
                        {
                            //string temp = Path.GetTempPath();
                            string file = Path.Combine(tempFolderPath, "getout.mp3");
                            MediaPlayer d = new MediaPlayer();
                            d.Open(new Uri(file));
                            d.Play();
                            //los threads ni idea de pq la verdad.
                            Thread.Sleep(1500);
                            //aahhhh para q se salga
                            Application.Exit();
                        }
                        else
                        {
                            //busca el texto en el duckduckgo (si no tiene https:// o http://)
                            webView21.CoreWebView2.Navigate("https://duckduckgo.com/?t=ffab&q=" + gunaTextBox1.Text.Replace(" ", "+"));
                        }
                    }
                    catch (Exception error)
                    {
                        //99% de probabilidad de q nunca que vaya a salir este error si no lo fuerzas.
                        MessageBox.Show("Error. URL is not working.");
                    }
                }
                
               

            }
        }

        private void webView21_Click(object sender, EventArgs e)
        {

        }

        private void webView21_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e)
        {
           

        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            //No se que hace esta funcion, tu dime.
            webView21.Reload();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            //no tocar, funciona bien, ni idea de pq pero funciona bien. NO TOCAR.
            if(webView21.CanGoBack)
            {
                gunaButton1.Enabled = true;
                webView21.GoBack();
                gunaButton2.Enabled = true;
            } else
            {
                gunaButton1.Enabled = false;
            }
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            //lo mismo que arriba NO TOCAR.
            if (webView21.CanGoForward)
            {
                gunaButton2.Enabled = true;
                webView21.GoForward();
            }
            else
            {
                gunaButton2.Enabled = false;
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            //por si das a la imagen👀
            string file = Path.Combine(tempFolderPath, "getout.mp3");
            MediaPlayer d = new MediaPlayer();
            d.Open(new Uri(file));
            d.Play();
        }

        private void gunaControlBox1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
