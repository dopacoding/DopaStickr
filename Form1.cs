using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DopaStickr
{
    public partial class dopastickr : Form
    {
        //---------------------------------- code for lock form() --------------------------------------
                                                                                                            //
        private bool isPinned = false; // حالة التثبيت                                                    //
                                                                                                         //
           protected override void WndProc(ref Message m)                                              //
        {                                                                                             //
            const int WM_NCLBUTTONDOWN = 0xA1; // الرسالة الخاصة بسحب النافذة                       //
            const int HTCAPTION = 0x2;         // المنطقة العلوية للنافذة                          //
                                                                                                   //
            // إذا كان الفورم مثبتًا، امنع التحريك                                                //
            if (isPinned && m.Msg == WM_NCLBUTTONDOWN && m.WParam.ToInt32() == HTCAPTION)        //
            {                                                                                   //
                return; // منع المستخدم من سحب الفورم                                          //
            }                                                                                  //
                                                                                              //
            base.WndProc(ref m);                                                             //
        }                                                                                   //
                                                                                           //
        //--------------------------------------------------------------------------------------------

        public dopastickr()
        {
            InitializeComponent();
            Text = DateTime.Now.ToString();

            toolStripButton3.BackColor = richTextBox1.BackColor;

            changeThemeToolStripMenuItem.Enabled = false;
            themeToolStripMenuItem.Enabled = false;

            systemTrayMenuToolStripMenuItem.Enabled = false;



        }


        //----------------------- section of functions() --------------------------//

        void NewStickr()
        {
            dopastickr frm1 = new dopastickr();
            frm1.Show();
        }

        // function redo () for richtextbox1
        private void redo()
        {
            richTextBox1.Redo();
        }
        // function undo() for richTextBox1
        private void undo()
        {
            richTextBox1.Undo();
        }

        // function copy() for richTextBox1
        string CopyText;
        private void copy()
        {
            if (richTextBox1.SelectedText.Length > 0)
            {
                CopyText = richTextBox1.SelectedText;
            }
            else
            {
                CopyText = richTextBox1.Text;
            }
        }

        // function cut() for richTextBox1 
        private void Cut()
        {
            if (richTextBox1.SelectedText.Length > 0)
            {
                CopyText = richTextBox1.SelectedText;
                richTextBox1.SelectedText = "";
            }
            else
            {
                CopyText = richTextBox1.Text;
                richTextBox1.Clear();
            }
        }


        // function past() for richTextBox1
        private void Past()
        {
            richTextBox1.Text += CopyText;
        }


        // function font() for richtext box1
        Font currentFont = new Font("Ubuntu", 11, FontStyle.Regular);
        private void font()
        {
            fontDialog1.ShowApply = true;
       
            using (FontDialog fontDialog = new FontDialog())
            {
                // Set the current font as the default in the FontDialog
                fontDialog.Font = currentFont;

                if (fontDialog1.ShowDialog() == DialogResult.OK)
                {
                    
                    richTextBox1.SelectionFont = fontDialog1.Font;
                }
            }
        }
        // function writting left to right in richtextbox1
        void WritingLeftToRight()
        {
            richTextBox1.RightToLeft = RightToLeft.No;
        }

        //function writting right to left in richtextbox1
        void WritingRightToLeft()
        {
            richTextBox1.RightToLeft = RightToLeft.Yes;
        }
        // function save as txt ()
        void SaveAs()
        {

            saveFileDialog1.InitialDirectory = @".Desktop";
            saveFileDialog1.Title = "Save As";
            saveFileDialog1.Filter = "txt files(*.txt)|*.txt|All Files(*.*)|*.*";
            saveFileDialog1.DefaultExt = "txt";



            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string strPath = saveFileDialog1.FileName;
                if (Path.GetExtension(saveFileDialog1.FileName) != ".txt")
                {
                    strPath += ".txt";
                }
                StreamWriter sw = new StreamWriter(strPath);
                sw.WriteLine(richTextBox1.Text);
                sw.Close();
            }
            else { }
        }

        // function CopyAll() from richTextBox1

        void CopyAll()
        {
            richTextBox1.SelectAll();
            copy();
        }

        // hide form and send it to hide icons selection in windows
        void HideForm()
        {
            

            notifyIcon1.ShowBalloonTip(30000); // مدة اظهار الفورم
            this.Hide(); // إخفاء الفورم
        }

        //show form by click notify
        void ShowFormByClickNotify()
        {
            this.Show(); // إظهار الفورم
            this.WindowState = FormWindowState.Normal; // إعادة الفورم للوضع الطبيعي
            this.BringToFront(); // التأكد من ظهور الفورم أمام جميع النوافذ الأخرى
        }
        // function lock form ()
        void LockForm()
        {
            if (lockToolStripMenuItem.Checked)
            {
                isPinned = true;
                lockToolStripMenuItem.Text = "unLock";

            }
            else
            {
                isPinned = false;
                lockToolStripMenuItem.Text = "Lock";
            }
        }

        // function printdatetime in richtextbox1
        void PrintDateTimeNow()
        {
            richTextBox1.Text += DateTime.Now.ToString() + " ";
        }
        // function markascomplete() from richtextbox1
        void MarkAsComplete()
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionFont = new Font(currentFont, FontStyle.Strikeout);
        }

        // function removemarkascomplete() from richtextbox1
        void RemoveMarkAsComplete()
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionFont = new Font(currentFont,FontStyle.Regular);
        }

        //-------------------------------------------------------------------------------//



        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            NewStickr();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.BackColor = colorDialog1.Color;
                toolStripButton3.BackColor = colorDialog1.Color;
            }
        }

        private void systemTrayMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contextMenuStrip2.Show();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            undo();
        }
        private void rendoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redo();
        }
        private void coperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void pastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Past();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void selectionAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void copeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyAll();
        }

        private void saveAsTXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

       
        private void lockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LockForm();
        }
       
      
       
        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideForm();
            
            
        }


        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            ShowFormByClickNotify();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            ShowFormByClickNotify();
        }

        private void timeStampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDateTimeNow();
        }

        private void markAsCompleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarkAsComplete();
        }

        private void removeMarkAsCompleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveMarkAsComplete();
        }

        private void newStickerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewStickr();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void leftToRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WritingLeftToRight();
        }

        private void rightToLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WritingRightToLeft();
        }

   

        private void fontToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            font();
        }
    }
}



