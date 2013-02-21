using System;
using System.Collections;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using mRemoteNC.Connection;
using My;

//using mRemoteNC.Runtime;

namespace mRemoteNC.Tools
{
    public class Controls
    {
        public class ComboBoxItem
        {
            private string _Text;

            public string Text
            {
                get { return this._Text; }
                set { this._Text = value; }
            }

            private object _Tag;

            public object Tag
            {
                get { return this._Tag; }
                set { this._Tag = value; }
            }

            public ComboBoxItem(string Text, object Tag = null)
            {
                this._Text = Text;
                if (Tag != null)
                {
                    this._Tag = Tag;
                }
            }

            public override string ToString()
            {
                return this._Text;
            }
        }

        public class NotificationAreaIcon
        {
            private NotifyIcon _nI;

            private ContextMenuStrip _cMen;
            private ToolStripMenuItem _cMenCons;
            private ToolStripSeparator _cMenSep1;
            private ToolStripMenuItem _cMenExit;

            private bool _Disposed;

            public bool Disposed
            {
                get { return _Disposed; }
                set { _Disposed = value; }
            }

            //Public Event MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
            //Public Event MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

            public NotificationAreaIcon()
            {
                try
                {
                    this._cMenCons = new ToolStripMenuItem();
                    this._cMenCons.Text = Language.strConnections;
                    this._cMenCons.Image = global::My.Resources.Resources.Root;

                    this._cMenSep1 = new ToolStripSeparator();

                    this._cMenExit = new ToolStripMenuItem();
                    this._cMenExit.Text = Language.strMenuExit;
                    this._cMenExit.Click += new System.EventHandler(cMenExit_Click);

                    this._cMen = new ContextMenuStrip();
                    this._cMen.Font = new System.Drawing.Font("Microsoft Sans Serif", (float)(8.25F),
                                                              System.Drawing.FontStyle.Regular,
                                                              System.Drawing.GraphicsUnit.Point, (byte)(0));
                    this._cMen.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
                    this._cMen.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this._cMenCons, this._cMenSep1, this._cMenExit });

                    this._nI = new NotifyIcon();
                    this._nI.Text = "mRemote";
                    this._nI.BalloonTipText = "mRemote";
                    this._nI.Icon = System.Drawing.Icon.FromHandle(global::My.Resources.Resources.mRemote_Icon.GetHicon());
                    this._nI.ContextMenuStrip = this._cMen;
                    this._nI.Visible = true;

                    this._nI.MouseClick += new System.Windows.Forms.MouseEventHandler(nI_MouseClick);
                    this._nI.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(nI_MouseDoubleClick);
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("Creating new SysTrayIcon failed" + Constants.vbNewLine +
                                                         ex.Message), true);
                }
            }

            public void Dispose()
            {
                try
                {
                    this._nI.Visible = false;
                    this._nI.Dispose();
                    this._cMen.Dispose();
                    this._Disposed = true;
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("Disposing SysTrayIcon failed" + Constants.vbNewLine +
                                                         ex.Message), true);
                }
            }

            private void nI_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Right)
                {
                    this._cMenCons.DropDownItems.Clear();

                    foreach (TreeNode tNode in Runtime.Windows.treeForm.tvConnections.Nodes)
                    {
                        AddNodeToMenu(tNode.Nodes, this._cMenCons);
                    }
                }
            }

            private void AddNodeToMenu(TreeNodeCollection tnc, ToolStripMenuItem menToolStrip)
            {
                try
                {
                    foreach (TreeNode tNode in tnc)
                    {
                        ToolStripMenuItem tMenItem = new ToolStripMenuItem();
                        tMenItem.Text = tNode.Text;
                        tMenItem.Tag = tNode;

                        if (Tree.Node.GetNodeType(tNode) == Tree.Node.Type.Container)
                        {
                            tMenItem.Image = global::My.Resources.Resources.Folder;
                            tMenItem.Tag = tNode.Tag;

                            menToolStrip.DropDownItems.Add(tMenItem);
                            AddNodeToMenu(tNode.Nodes, tMenItem);
                        }
                        else
                        {
                            tMenItem.Image = global::My.Resources.Resources.Play;
                            tMenItem.Tag = tNode.Tag;

                            menToolStrip.DropDownItems.Add(tMenItem);
                        }

                        tMenItem.MouseDown += new System.Windows.Forms.MouseEventHandler(ConMenItem_MouseDown);
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("AddNodeToMenu failed" + Constants.vbNewLine + ex.Message),
                                                        true);
                }
            }

            private void nI_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
            {
                if (frmMain.Default.Visible == true)
                {
                    HideForm();
                }
                else
                {
                    ShowForm();
                }
            }

            private void ShowForm()
            {
                frmMain.Default.Show();
                frmMain.Default.WindowState = frmMain.Default.PreviousWindowState;

                if (Settings.Default.ShowSystemTrayIcon == false)
                {
                    Runtime.NotificationAreaIcon.Dispose();
                    Runtime.NotificationAreaIcon = null;
                }
            }

            private void HideForm()
            {
                frmMain.Default.Hide();
                frmMain.Default.PreviousWindowState = frmMain.Default.WindowState;
            }

            private void ConMenItem_MouseDown(System.Object sender, System.Windows.Forms.MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (((Control)sender).Tag is Info)
                    {
                        if (frmMain.Default.Visible == false)
                        {
                            ShowForm();
                        }
                        Runtime.OpenConnection((Info)((Control)sender).Tag);
                    }
                }
            }

            private void cMenExit_Click(System.Object sender, System.EventArgs e)
            {
                Runtime.Shutdown.Quit();
            }
        }

        public static SaveFileDialog ConnectionsSaveAsDialog()
        {
            SaveFileDialog sDlg = new SaveFileDialog();
            sDlg.CheckPathExists = true;
            sDlg.InitialDirectory = (string)mRemoteNC.AppInfo.Connections.DefaultConnectionsPath;
            sDlg.FileName = (string)mRemoteNC.AppInfo.Connections.DefaultConnectionsFile;
            sDlg.OverwritePrompt = true;

            sDlg.Filter = Language.strFiltermRemoteXML + "|*.xml|" + Language.strFiltermRemoteCSV + "|*.csv|" +
                          Language.strFiltervRD2008CSV + "|*.csv|" + Language.strFilterAll + "|*.*";

            return sDlg;
        }

        public static OpenFileDialog ConnectionsLoadDialog()
        {
            OpenFileDialog lDlg = new OpenFileDialog();
            lDlg.CheckFileExists = true;
            lDlg.InitialDirectory = (string)mRemoteNC.AppInfo.Connections.DefaultConnectionsPath;
            lDlg.Filter = Language.strFiltermRemoteXML + "|*.xml|" + Language.strFilterAll + "|*.*";

            return lDlg;
        }

        public static OpenFileDialog ConnectionsRDPImportDialog()
        {
            OpenFileDialog lDlg = new OpenFileDialog();
            lDlg.CheckFileExists = true;
            //lDlg.InitialDirectory = App.AppInfo.Connections.DefaultConnectionsPath
            lDlg.Filter = Language.strFilterRDP + "|*.rdp|" + Language.strFilterAll + "|*.*";

            return lDlg;
        }

        public class TreeNodeSorter : IComparer
        {
            private TreeNode _nodeToSort;
            private SortType _sortType;

            public TreeNodeSorter(TreeNode node, SortType sortType)
            {
                this._nodeToSort = node;
                this._sortType = sortType;
            }

            public int Compare(object x, object y)
            {
                TreeNode tx = (TreeNode)x;
                TreeNode ty = (TreeNode)y;

                if ((tx.Parent == this._nodeToSort) && (ty.Parent == this._nodeToSort))
                {
                    // Ascending
                    if (this._sortType == SortType.Ascending)
                    {
                        return string.Compare(tx.Text, ty.Text);
                    }

                    // Descending
                    if (this._sortType == SortType.Descending)
                    {
                        return string.Compare(ty.Text, tx.Text);
                    }
                }

                return 0;
            }

            public enum SortType
            {
                Ascending = 0,
                Descending = 1
            }
        }
    }
}