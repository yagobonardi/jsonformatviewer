using Newtonsoft.Json;
using System;
using System.Windows.Forms;

namespace JsonViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Format();
        }

        private void BtnFormat_Click(object sender, EventArgs e)
        {
            Format();
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lbljson.Text);
            SetButtonCopyText("copied!");
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearPrincipalLabel();
            SetButtonCopyText("copy");
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();

                notifyIcon.Visible = true;
            }
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenByIcon();
            Format();
        }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            OpenByIcon();
            Format();
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            OpenByIcon();
            Format();
        }

        private void OpenByIcon()
        {
            this.Show();

            this.WindowState = FormWindowState.Normal;

            this.notifyIcon.Visible = false;
        }

        private void ClearPrincipalLabel()
        {
            lbljson.Text = "{ ... }";
        }

        private void SetButtonCopyText(string text)
        {
            btnCopy.Text = text;
        }

        private void Format()
        {
            ClearPrincipalLabel();
            SetButtonCopyText("copy");

            if (Clipboard.ContainsText() && Clipboard.GetText() != string.Empty)
            {
                try
                {
                    var text = Clipboard.GetText();

                    var obj = JsonConvert.DeserializeObject(text);

                    var f = JsonConvert.SerializeObject(obj, Formatting.Indented);

                    lbljson.Text = f;
                }
                catch (Exception ex)
                {
                    ClearPrincipalLabel();
                    MessageBox.Show(ex.Message, "copy a VALID text to format");
                }

            }
            else
            {
                ClearPrincipalLabel();
                MessageBox.Show("copy a text to format!", "error");
            }
        }
    }
}