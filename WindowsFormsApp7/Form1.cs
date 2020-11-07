using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp7
{
    public partial class Form1 : Form
    {
        Color c;
        string text_copy;
       
        public Form1()
        {
            InitializeComponent();
           c = this.BackColor;
            timer1.Start();
        }

        TextBox CurrentTextBox
        {
            get
            {
                if (tabControl1.SelectedIndex != -1)
                {
                    return (TextBox)tabControl1.SelectedTab.Controls[0];
                }
                return null;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            open.Filter = "All Files(*.*)|*.*|Text Files(*.txt)|*.txt||";
            open.FilterIndex = 2;
            open.CheckFileExists = false;     
            if (open.ShowDialog() == DialogResult.OK)
            {
               
                StreamReader reader = new StreamReader(open.OpenFile());
                CurrentTextBox.Text = reader.ReadToEnd();
                reader.Close();
            }
        }

        private void dublicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
      
            save.DefaultExt = ".txt";
            save.OverwritePrompt = true;
           
            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(save.FileName);

                writer.Write(CurrentTextBox.Text);
                string str = null;
                for (int i = save.FileName.Length - 1; i >= 0; i--)
                {
                    if (save.FileName[i] != '\\')
                    {
                        str += save.FileName[i];
                    }
                    else
                    {
                        break;
                    }
                }
                char[] s = str.ToCharArray();
                Array.Reverse(s);
                str = new string(s);
                tabControl1.SelectedTab.Text = str;
                writer.Close();             
            }
        }

        private void newDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            open.Filter = "All Files(*.*)|*.*|Text Files(*.txt)|*.txt||";
            open.FilterIndex = 2;
            open.CheckFileExists = true;
            if (open.ShowDialog() == DialogResult.OK)
            {

                StreamReader reader = new StreamReader(open.OpenFile());
                CurrentTextBox.Text = reader.ReadToEnd();
                reader.Close();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            text_copy = CurrentTextBox.Text;
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTextBox.SelectedText ="";       
        }

        private void pastleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTextBox.Text += text_copy;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTextBox.ResetText();
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem it = (ToolStripMenuItem)sender;           
            if (it.Checked == true)
            {
                this.BackColor = Color.Black;
            }
            else 
            {
                this.BackColor = c;
            }
        }

        private void textColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                CurrentTextBox.ForeColor = color.Color;
            }
        }

        private void textFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = color.Color;
            }
        }

        private void textFontToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();

            if (font.ShowDialog() == DialogResult.OK)
            {
                var selected = font.Font;
                CurrentTextBox.Font = selected;
               
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CurrentTextBox.SelectedText = "";
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            text_copy = CurrentTextBox.Text;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            CurrentTextBox.Text += text_copy;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            CurrentTextBox.ResetText();
        }
        int countOfLeter = 0;

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            TabPage newPage = new TabPage($"New Tab {tabControl1.TabPages.Count + 1}");

            TextBox newTextBox = new TextBox();
            newTextBox.ContextMenuStrip = this.contextMenuStrip1;
            newTextBox.Location = new System.Drawing.Point(10, 10);
            newTextBox.Multiline = true;
            newTextBox.Name = "textBox1";
            newTextBox.Size = new System.Drawing.Size(630, 300);
            newTextBox.TabIndex = 1;

            newPage.Controls.Add(newTextBox);

            tabControl1.TabPages.Add(newPage);
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
           DialogResult dialog = MessageBox.Show("закрити без сохранения","text",MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                if (tabControl1.SelectedIndex != -1)
                {
                    tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
                }

              
            }
            else
            {
                dublicateToolStripMenuItem_Click(sender,e);
                if (tabControl1.SelectedIndex != -1)
                {
                    tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
                }
                
            }
            
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != -1)
            {
                toolStripStatusLabel4.Text = "0";
                countOfLeter = 0;
                toolStripStatusLabel2.Text = CurrentTextBox.Text.Length.ToString();

                foreach (var item in CurrentTextBox.Text)
                {
                    if ((item >= 65 && item <= 90) || (item >= 97 && item <= 122))
                    {
                        toolStripStatusLabel4.Text = (++countOfLeter).ToString();
                    }
                }
            }
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != -1)
            {
                countOfLeter = 0;

                toolStripStatusLabel2.Text = CurrentTextBox.Text.Length.ToString();


                foreach (var item in CurrentTextBox.Text)
                {
                    if ((item >= 65 && item <= 90) || (item >= 97 && item <= 122))
                    {
                        toolStripStatusLabel4.Text = (++countOfLeter).ToString();
                    }
                }
            }
        }
    }
}
