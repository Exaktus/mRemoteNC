using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mRemoteNC.Forms
{
    public partial class ProgressForm : Form
    {
        public bool AllowClose = false;

        public ProgressForm()
        {
            InitializeComponent();
        }

        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !AllowClose;
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {

        }
    }
}
