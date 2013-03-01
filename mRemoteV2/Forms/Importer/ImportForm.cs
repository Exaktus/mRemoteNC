using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using EXControls;
using mRemoteNC.App;
using mRemoteNC.Connection;
using mRemoteNC.Forms.Importer.Helpers;

namespace mRemoteNC.Forms.Importer
{
    public partial class ImportForm : Form
    {
        public ImportForm()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SetSearchButtonState(false);
            ThreadPool.QueueUserWorkItem(state => FindConfigs());
        }

        void SetSearchButtonState(bool eneabled)
        {
            try
            {
                if (btnSearch.InvokeRequired)
                {
                    Invoke(new MethodInvoker(() => SetSearchButtonState(eneabled)));
                }
                else
                {
                    btnSearch.Enabled = eneabled;
                }
            }
            catch (Exception)
            {
                
            }
        }

        public void FindConfigs()
        {
            try
            {
                new List<IConfigSearcher> {new PuttyConfigSearcher(), new RDPFileImporter()}
                    .AsParallel()
                    .Select(searcher => searcher.GetConnections())
                    .ForAll(AddFoundConnections);
            }
            catch (Exception)
            {

            }
            finally
            {
                FileEnumerator.ClearFiles();
                SetSearchButtonState(true);
            }
        }

        private void AddFoundConnections(IEnumerable<Info> iEnumerable)
        {
            try
            {
                var l = new List<EXImageListViewItem>();
                foreach (var info in iEnumerable)
                {
                    var item = new EXImageListViewItem { Tag = info };
                    item.SubItems.Add(new EXBoolListViewSubItem(true));
                    item.SubItems.Add(new EXImageListViewSubItem(Connection.Icon.FromString(info.Icon).ToBitmap()));
                    item.SubItems.Add(info.Description);
                    item.SubItems.Add(info.Protocol.ToString());
                    item.SubItems.Add(info.Hostname);
                    item.SubItems.Add(info.Port.ToString());
                    l.Add(item);
                }
                if (lvFoundConections.InvokeRequired)
                {
                    lvFoundConections.Invoke(new MethodInvoker(() =>
                                                                   {
                                                                       lvFoundConections.Items.AddRange(l.ToArray());
                                                                   }));
                }
                else
                {
                    lvFoundConections.Items.AddRange(l.ToArray());
                }
            }
            catch (Exception)
            {

            }
            
        }

        private void Import_Load(object sender, EventArgs e)
        {
            btnSearch_Click(null,null);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            foreach (var con in lvFoundConections.Items.Cast<EXImageListViewItem>()
                .Where(item => (item.SubItems[1] as EXBoolListViewSubItem) != null && (item.SubItems[1] as EXBoolListViewSubItem).BoolValue))
            {
                Runtime.Windows.treeForm.AddConnection(con.Tag as Info);
                var exBoolListViewSubItem = con.SubItems[1] as EXBoolListViewSubItem;
                if (exBoolListViewSubItem != null)
                    exBoolListViewSubItem.BoolValue = false;
            }
            lvFoundConections.Refresh();
        }

    }
}
