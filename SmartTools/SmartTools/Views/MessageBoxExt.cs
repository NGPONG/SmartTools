using MaterialSkin.Controls;
using SmartTools.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartTools.Views
{
    public partial class MessageBoxExt : MaterialForm
    {
        public MessageBoxExt(string message)
        {
            InitializeComponent();
            this.lblMessage.Text = message;
        }

        private void BtnSure_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MessageBoxExt_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        public static void Show(string message, MessageboxType type)
        {
            using (MessageBoxExt messageBox = new MessageBoxExt(message))
            {
                string title;
                switch (type)
                {
                    case MessageboxType.Error:
                        title = "错误";
                        break;
                    case MessageboxType.Info:
                        title = "提示";
                        break;
                    default:
                        throw new ArgumentNullException();
                }

                messageBox.Text = title;
                messageBox.StartPosition = FormStartPosition.CenterParent;
                if (messageBox.ShowDialog() == DialogResult.OK)
                {
                    return;
                }
            }
        }

        private void LblMessage_SizeChanged(object sender, EventArgs e)
        {
            FormController.Instance().SetDynamicSize((MaterialLabel)sender, this);
        }
    }

    public enum MessageboxType
    {
        Error = 0,
        Info = 1
    }
}
