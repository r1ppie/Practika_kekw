using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace Practika_kekw_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<string[]> playList = new List<string[]>();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter sr = new StreamWriter("C:\\Users\\Champ\\source\\repos\\Practika(kekw)\\baza.txt");
                string path = null;
                OpenFileDialog zxc = new OpenFileDialog();
                zxc.DefaultExt = ".mp4";
                zxc.Filter = "Файл формата .mp4|*mp4| .avi|*avi| .mov|*mov| .mkv|*mkv";
                zxc.RestoreDirectory = true;
                if(zxc.ShowDialog() == DialogResult.OK)
                    path = zxc.FileName;
                else
                {                  
                    return;
                }
                axWindowsMediaPlayer1.URL = path;

                if (axWindowsMediaPlayer1 != null)
                {
                    textBox1.Text = zxc.SafeFileName;
                    sr.WriteLine(zxc.SafeFileName, true);
                }
                sr.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {           
            axWindowsMediaPlayer1.settings.volume = trackBar1.Value;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if(trackBar1.Value > 0)
                    trackBar1.Value -= 5;
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (trackBar1.Value < 100)
                    trackBar1.Value += 5;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1000;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                trackBar2.Maximum = Convert.ToInt32(axWindowsMediaPlayer1.currentMedia.duration);
                trackBar2.Value = Convert.ToInt32(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);

                if (axWindowsMediaPlayer1 != null)
                {
                    int s = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                    int h = s / 3600;
                    int m = (s - (h * 3600)) / 60;
                    s -= (h * 3600 + m * 60);
                    label1.Text = String.Format("{0:D}:{1:D2}:{2:D2}", h, m, s);
                }
                else
                {
                    label1.Text = "0:00:00";
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = trackBar2.Value;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog playlist = new OpenFileDialog();
                StreamWriter sr = new StreamWriter("C:\\Users\\Champ\\source\\repos\\Practika(kekw)\\baza.txt");
                playlist.DefaultExt = ".mp4";
                playlist.Filter = "Файл формата .mp4|*mp4| .avi|*avi| .mov|*mov| .mkv|*mkv";
                playlist.RestoreDirectory = true;
                playlist.Multiselect = true;

                if (playlist.ShowDialog() == DialogResult.OK)
                {
                    foreach (string fileName in playlist.FileNames)
                    {
                        string shortPath = Path.GetFileName(fileName);
                        shortPath = shortPath.Substring(0, shortPath.Length - 4);

                        string[] pathElements = new string[2];
                        pathElements[0] = shortPath;
                        pathElements[1] = fileName;

                        playList.Add(pathElements);
                        listBox1.Items.Add(shortPath);
                        sr.WriteLine(shortPath, true);
                    }
                }
                sr.Close();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int i = listBox1.SelectedIndex;
                axWindowsMediaPlayer1.URL = playList[i][1];
                textBox1.Text = playList[i][0];
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedIndex + 1 < playList.Count)
                {
                    int i = listBox1.SelectedIndex + 1;
                    axWindowsMediaPlayer1.URL = playList[i][1];
                    textBox1.Text = playList[i][0];
                    listBox1.SelectedIndex += 1;
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedIndex > 0)
                {
                    int i = listBox1.SelectedIndex - 1;
                    axWindowsMediaPlayer1.URL = playList[i][1];
                    textBox1.Text = playList[i][0];
                    listBox1.SelectedIndex -= 1;
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                int i = listBox1.SelectedIndex;
                playList.Remove(playList[i]);
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
