using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;
using MediaToolkit;
using System.IO;
using System.Net;

namespace MP3Dönüstürücü
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Boolean format = true;
        private async void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "İndirmek istediğiniz klasörü seçiniz." })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    GetTitle();
                    MessageBox.Show("İndirme işlemi başlatıldı...","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    var yt = YouTube.Default;
                    var video = await yt.GetVideoAsync(textBox1.Text);
                    File.WriteAllBytes(fbd.SelectedPath + @"\" + video.FullName, await video.GetBytesAsync());
                    var inputfile = new MediaToolkit.Model.MediaFile { Filename = fbd.SelectedPath + @"\" + video.FullName };
                    var outputfile = new MediaToolkit.Model.MediaFile { Filename = $"{fbd.SelectedPath + @"\" + video.FullName} .mp3"};


                    using (var enging = new Engine())
                    {
                        enging.GetMetadata(inputfile);
                        enging.Convert(inputfile,outputfile);
                    }

                    if (format==true)
                    {
                        File.Delete(fbd.SelectedPath + @"\" + video.FullName);
                    }
                    else                       
                    {
                        File.Delete( $"{fbd.SelectedPath + @"\" + video.FullName}.mp3") ;
                    }

                    progressBar1.Value = 100;
                    MessageBox.Show("İndirme işlemi tamamlandı...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Lütfen dosya yolu seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void radioButton1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton2_TextChanged(object sender, EventArgs e)
        {
            
        }
        void GetTitle()
        {
            //WebRequest istek = HttpWebRequest.Create(textBox1.Text);
            //WebResponse yanit;
            //yanit = istek.GetResponse();
            //StreamReader sr = new StreamReader(yanit.GetResponseStream());
            //string gelen = sr.ReadToEnd();
            //int baslangic = gelen.IndexOf("<title>") + 7;
            //int bitis = gelen.Substring(baslangic).IndexOf("</title>");
            //string gelenbilgi = gelen.Substring(baslangic, bitis);
            //label3.Text = (gelenbilgi);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            format = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            format = false;
        }     
    }
}
