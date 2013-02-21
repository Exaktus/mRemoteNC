using System;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using My;
using WeifenLuo.WinFormsUI.Docking;
using mRemoteNC.App;
using mRemoteNC.Connection;
using mRemoteNC.Tools;


namespace mRemoteNC.Forms
{
    public partial class ConnectionStatusForm : UI.Window.Base
    {
        public ConnectionStatusForm()
        {
            InitializeComponent();
        }

        public ConnectionStatusForm(DockContent Panel)
                {
                    this.WindowType = UI.Window.Type.ConnectionStatus;
                    this.DockPnl = Panel;
                    this.InitializeComponent();
                }

        private System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();


        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.SelectedItem = Settings.Default.ConnectStatusTestType;
            s.ByColumn=2;
            lstStatus.ListViewItemSorter = s;
            AutoUpdateStart();
        }

        void AutoUpdateStart()
        {
            toolStripButton2.Checked = Settings.Default.ConnectStatusAutoupdate;
            if (!Settings.Default.ConnectStatusAutoupdate) return;
            t.Interval = 1000*30;
            t.Tick += t_Tick;
            t.Start();
            toolStripButton1_Click(null,null);
        }

        void t_Tick(object sender, EventArgs e)
        {

            if (!Settings.Default.ConnectStatusAutoupdate || IsHidden) return;
            foreach (Connection.Info con in Runtime.ConnectionList)
            {
                TestAsync(con);
            }
        }

        bool TestStatus(Connection.Info con)
        {
            try
            {
                bool upPing = true;
                bool upConnect = true;
                if (Settings.Default.ConnectStatusTestType == "Ping" || Settings.Default.ConnectStatusTestType == "Both")
                {
                    upPing = Misc.Pinger(con.Hostname);
                }
                if (Settings.Default.ConnectStatusTestType == "Connect" || Settings.Default.ConnectStatusTestType == "Both")
                {
                    upConnect = Misc.TestConnect(con.Hostname, con.Port);
                }
                return upPing && upConnect;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public class ListViewSorter : System.Collections.IComparer
        {
            public int Compare(object o1, object o2)
            {
                if (!(o1 is ListViewItem))
                    return (0);
                if (!(o2 is ListViewItem))
                    return (0);

                ListViewItem lvi1 = (ListViewItem)o2;
                string str1 = lvi1.SubItems[ByColumn].Text;
                ListViewItem lvi2 = (ListViewItem)o1;
                string str2 = lvi2.SubItems[ByColumn].Text;

                int result;
                result = lvi1.ListView.Sorting == SortOrder.Ascending ? String.Compare(str1, str2) : String.Compare(str2, str1);

                LastSort = ByColumn;

                return (result);
            }


            public int ByColumn
            {
                get { return Column; }
                set { Column = value; }
            }
            int Column = 0;

            public int LastSort
            {
                get { return LastColumn; }
                set { LastColumn = value; }
            }
            int LastColumn = 0;
        }

        ListViewSorter s = new ListViewSorter();

        void TestAsync(Info con1)
        {
            try
            {
                ThreadPool.QueueUserWorkItem(state =>
                 {
                     bool result = TestStatus(con1);
                     lstStatus.Invoke((MethodInvoker)(() =>
                     {
                         foreach (ListViewItem i in lstStatus.Items.Cast<ListViewItem>().Where(i => i.Tag == con1))
                         {
                             i.SubItems[2].Text = result ? "Up" : "Down";
                             i.ForeColor = result ? Color.Green : Color.Red;
                         }
                         lstStatus.Sort();
                     }));
                 });
            }
            catch (Exception)
            {
                
            }
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                lstStatus.Items.Clear();
                foreach (Connection.Info con in Runtime.ConnectionList)
                {
                    ListViewItem lvi = new ListViewItem(con.Name);
                    lvi.SubItems.Add(con.Hostname + ":" + con.Port);
                    lvi.SubItems.Add("Checking...");
                    lvi.ForeColor = Color.Gray;
                    lvi.Tag = con;
                    lstStatus.Items.Add(lvi);
                    TestAsync(con);
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

        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.ConnectStatusTestType = toolStripComboBox1.Text;
            Settings.Default.Save();
        }

        private void lstStatus_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstStatus.SelectedItems.Count < 1) return;

            lstStatus.Invoke((MethodInvoker) (() =>
                                                  {
                                                      lstStatus.SelectedItems[0].ForeColor = Color.Gray;
                                                      lstStatus.SelectedItems[0].SubItems[2].Text = "Checking...";
                                                  }));
            TestAsync(lstStatus.SelectedItems[0].Tag as Info);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Settings.Default.ConnectStatusAutoupdate= toolStripButton2.Checked;
            Settings.Default.Save();
            if(!Settings.Default.ConnectStatusAutoupdate) t.Stop();
            else AutoUpdateStart();
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void ConnectionStatusForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMain.Default.mConStatus.Checked = false;
        }
    }
}
