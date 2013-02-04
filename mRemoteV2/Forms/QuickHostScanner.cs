using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using mRemoteNC.Connection;

namespace mRemoteNC.Forms
{
    public partial class QuickHostScanner : Form
    {
        public QuickHostScanner()
        {
            InitializeComponent();
        }
        ConnectionStatusForm.ListViewSorter s = new ConnectionStatusForm.ListViewSorter();
        private void QuickHostScanner_Load(object sender, EventArgs e)
        {
            if (txtPorts.Text == "") txtPorts.Text = "22,80,443,4899,5800,5900,3389,5938,5500";
            s.ByColumn = 2;
            lstStatus.ListViewItemSorter = s;
        }

        public static Protocols PortToProt(int port)
        {
            switch (port)
            {
                case 22:
                    return Protocols.SSH2;
                case 80:
                    return Protocols.HTTP;
                case 443:
                    return Protocols.HTTPS;
                case 4899:
                    return Protocols.RAdmin;
                case 5500:
                case 5800:
                case 5900:
                    return Protocols.VNC;
                case 3389:
                    return Protocols.RDP;
                case 5938:
                    return Protocols.TeamViewer;
                default:
                    return Protocols.NONE;
            }
        }


        void TestAsync(int port)
        {
            try
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    bool result = Tools.Misc.TestConnect(txtIP.Text, port);
                    lstStatus.Invoke((MethodInvoker)(() =>
                    {
                        foreach (ListViewItem i in lstStatus.Items.Cast<ListViewItem>().Where(i => i.Text==port.ToString()))
                        {
                            i.SubItems[2].Text = result ? "Up" : "Down";
                            if (!result)
                            {
                                i.ForeColor = Color.Gainsboro;
                            }
                            else
                            {
                                i.Font = new Font(i.Font.FontFamily, i.Font.Size,FontStyle.Bold);
                                i.ForeColor = Color.Black;
                            }

                        } foreach (ColumnHeader column in lstStatus.Columns)
                        {
                            column.Width = -2;
                        }
                        lstStatus.Sort();
                    }));
                });
            }
            catch (Exception)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                lstStatus.Items.Clear();
                foreach (var port in txtPorts.Text.Split(',').Select(s =>Convert.ToInt32(s.Trim())))
                {
                    ListViewItem lv=new ListViewItem(port.ToString());
                    lv.SubItems.Add(PortToProt(port).ToString());
                    lv.SubItems.Add("Checking...");
                    lv.ForeColor = Color.Gray;
                    lstStatus.Items.Add(lv);
                    TestAsync(port);
                }
                lstStatus.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    (string)
                                                    ("toolStripButton1_Click (tcs) failed" + Constants.vbNewLine + ex.Message),
                                                    true);
            }
        }

        private void lstStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem lstStatu in lstStatus.SelectedItems)
            {
                if (lstStatu.ForeColor == Color.Gainsboro || lstStatu.ForeColor == Color.Gray)
                {
                    lstStatu.Selected = false;
                }
            }
        }

        private void lstStatus_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hit = lstStatus.HitTest(e.Location);
            if (hit.Item != null && hit.Item.ForeColor != Color.Gainsboro && hit.Item.ForeColor != Color.Gray&&MessageBox.Show("Add this connection?","Adding connection",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                Runtime.Windows.treeForm.AddConnection(txtIP.Text, Convert.ToInt32(hit.Item.Text), PortToProt(Convert.ToInt32(hit.Item.Text)));
            }
        }
    }
}
