using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomIconDashboarderPlugin
{
    public partial class WaitingForm : Form
    {
        public WaitingForm()
        {
            InitializeComponent();
        }

        public void SetMessage(string msg)
        {
            lbl_waitingMessage.Text = msg;
        }

    }
}
