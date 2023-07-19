using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Net;
using System.Windows.Forms;

namespace SimpleProgram
{
    public partial class Form1 : Form
    {
        private string photoFilePath;
        private string songFilePath;
        private SoundPlayer player;
        private int yesButtonClickCount = 0;
        private DateTime lastYesButtonClickTime = DateTime.MinValue;

        public Form1()
        {
            InitializeComponent();

            string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            photoFilePath = Path.Combine(documentsFolder, "downloaded_photo.jpg");
            songFilePath = Path.Combine(documentsFolder, "Mzg1ODMxNTIzMzg1ODM3_JzthsfvUY24.wav");

            try
            {
                string photoUrl = "https://cdn.vox-cdn.com/thumbor/WR9hE8wvdM4hfHysXitls9_bCZI=/0x0:1192x795/1400x1400/filters:focal(596x398:597x399)/cdn.vox-cdn.com/uploads/chorus_asset/file/22312759/rickroll_4k.jpg";
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(photoUrl, photoFilePath);
                }

                if (File.Exists(photoFilePath))
                {
                    using (FileStream stream = new FileStream(photoFilePath, FileMode.Open))
                    {
                        pictureBox1.Image = Image.FromStream(stream);
                    }
                }
                else
                {
                    MessageBox.Show("Photo not found!", "Error");
                }

                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile("https://storage.cloudconvert.com/tasks/88b5ce47-7b5b-420d-b6fa-87f5c7a07fc7/Mzg1ODMxNTIzMzg1ODM3_JzthsfvUY24.wav?AWSAccessKeyId=cloudconvert-production&Expires=1689870764&Signature=qOt2JkXJ6qfHq3FvOX6oHSMVht8%3D&response-content-disposition=attachment%3B%20filename%3D%22Mzg1ODMxNTIzMzg1ODM3_JzthsfvUY24.wav%22&response-content-type=audio%2Fwav", songFilePath);
                }

                player = new SoundPlayer(songFilePath);
                player.Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            player.PlayLooping();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to close this application?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (DateTime.Now - lastYesButtonClickTime < TimeSpan.FromSeconds(1))
                {
                    yesButtonClickCount++;
                }
                else
                {
                    yesButtonClickCount = 1;
                }

                lastYesButtonClickTime = DateTime.Now;

                if (yesButtonClickCount >= 1)
                {
                    MessageBox.Show("You won't get out! :)", "Haha!");
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
