using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Mail;

namespace FileDropper
{
    public partial class Main : Form
    {
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

        public Main()
        {
            InitializeComponent();

            this.listBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox1_DragDrop);
            
            this.listBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox1_DragEnter);

            try
            {
                mail.From = new MailAddress("elonatura2015compsciia@gmail.com");
                mail.To.Add("santopelufincho@gmail.com");
                mail.Subject = "Just another attachment";
                mail.Body = "Foda-se";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("elonatura2015compsciia@gmail.com", "eloNatur");
                SmtpServer.EnableSsl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void listBox1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void listBox1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            for (int i = 0; i < s.Length; i++)
            {
                listBox1.Items.Add(s[i]);
                Attachment attachment = new Attachment(s[i]);
                System.Net.Mime.ContentDisposition disposition = attachment.ContentDisposition;
                disposition.CreationDate = File.GetCreationTime(s[i]);
                disposition.ModificationDate = File.GetLastWriteTime(s[i]);
                disposition.ReadDate = File.GetLastAccessTime(s[i]);
                disposition.FileName = Path.GetFileName(s[i]);
                disposition.Size = new FileInfo(s[i]).Length;
                disposition.DispositionType = System.Net.Mime.DispositionTypeNames.Attachment;
                mail.Attachments.Add(attachment);
            }
        }

        private void pictureBox1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void pictureBox1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            for (int i = 0; i < s.Length; i++)
            {
                listBox1.Items.Add(s[i]);
                Attachment attachment = new Attachment(s[i]);
                System.Net.Mime.ContentDisposition disposition = attachment.ContentDisposition;
                disposition.CreationDate = File.GetCreationTime(s[i]);
                disposition.ModificationDate = File.GetLastWriteTime(s[i]);
                disposition.ReadDate = File.GetLastAccessTime(s[i]);
                disposition.FileName = Path.GetFileName(s[i]);
                disposition.Size = new FileInfo(s[i]).Length;
                disposition.DispositionType = System.Net.Mime.DispositionTypeNames.Attachment;
                mail.Attachments.Add(attachment);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SmtpServer.Send(mail);
            MessageBox.Show("Mail was successfully sent!");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
