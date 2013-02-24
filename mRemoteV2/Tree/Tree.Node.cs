using System;
using System.DirectoryServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using mRemoteNC.App;
using mRemoteNC.Connection;
using My;

namespace mRemoteNC
{
    namespace Tree
    {
        public class Node
        {
            public enum Type
            {
                Root = 0,
                Container = 1,
                Connection = 2,
                NONE = 66
            }

            private static TreeView _TreeView;

            public static TreeView TreeView
            {
                get { return _TreeView; }
                set { _TreeView = value; }
            }

            public static TreeNode SelectedNode
            {
                get { return _TreeView.SelectedNode; }
                set
                {
                    treeNodeToBeSelected = value;
                    SelectNode();
                }
            }

            private static TreeNode treeNodeToBeSelected;

            private delegate void SelectNodeCB();

            private static void SelectNode()
            {
                if (_TreeView.InvokeRequired == true)
                {
                    SelectNodeCB d = new SelectNodeCB(SelectNode);
                    _TreeView.Invoke(d);
                }
                else
                {
                    _TreeView.SelectedNode = treeNodeToBeSelected;
                }
            }

            public static string GetConstantID(TreeNode node)
            {
                if (GetNodeType(node) == Type.Connection)
                {
                    return (node.Tag as Info).ConstantID;
                }
                else if (GetNodeType(node) == Type.Container)
                {
                    return (node.Tag as Container.Info).ConnectionInfo.ConstantID;
                }

                return "";
            }

            public static TreeNode GetNodeFromPositionID(int id)
            {
                foreach (Connection.Info conI in Runtime.ConnectionList)
                {
                    if (conI.PositionID == id)
                    {
                        if (conI.IsContainer)
                        {
                            return (conI.Parent as Container.Info).TreeNode;
                        }
                        else
                        {
                            return conI.TreeNode;
                        }
                    }
                }

                return null;
            }

            public static TreeNode GetNodeFromConstantID(string id)
            {
                foreach (Connection.Info conI in Runtime.ConnectionList)
                {
                    if (conI.ConstantID == id)
                    {
                        if (conI.IsContainer)
                        {
                            return (conI.Parent as Container.Info).TreeNode;
                        }
                        else
                        {
                            return conI.TreeNode;
                        }
                    }
                }

                return null;
            }

            public static Tree.Node.Type GetNodeType(TreeNode treeNode)
            {
                try
                {
                    if (treeNode == null)
                    {
                        return Type.NONE;
                    }

                    if (treeNode.Tag == null)
                    {
                        return Type.NONE;
                    }

                    if (treeNode.Tag is Root.Info)
                    {
                        return Type.Root;
                    }
                    else if (treeNode.Tag is Container.Info)
                    {
                        return Type.Container;
                    }
                    else if (treeNode.Tag is Connection.Info)
                    {
                        return Type.Connection;
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("Couldn\'t get node type" + Constants.vbNewLine + ex.Message),
                                                        true);
                }

                return Type.NONE;
            }

            public static Tree.Node.Type GetNodeTypeFromString(string str)
            {
                try
                {
                    switch (str.ToLower())
                    {
                        case "root":
                            return Type.Root;
                        case "container":
                            return Type.Container;
                        case "connection":
                            return Type.Connection;
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("Couldn\'t get node type from string" + Constants.vbNewLine +
                                                         ex.Message), true);
                }

                return Type.NONE;
            }

            public static TreeNode Find(TreeNode treeNode, string searchFor)
            {
                TreeNode tmpNode;

                try
                {
                    if ((treeNode.Text.ToLower()).IndexOf(searchFor.ToLower()) + 1 > 0)
                    {
                        return treeNode;
                    }
                    else
                    {
                        foreach (TreeNode childNode in treeNode.Nodes)
                        {
                            tmpNode = Find(childNode, searchFor);
                            if (tmpNode != null)
                            {
                                return tmpNode;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)("Find node failed" + Constants.vbNewLine + ex.Message),
                                                        true);
                }

                return null;
            }

            public static TreeNode Find(TreeNode treeNode, Info conInfo)
            {
                TreeNode tmpNode;

                try
                {
                    if (treeNode.Tag == conInfo)
                    {
                        return treeNode;
                    }
                    else
                    {
                        foreach (TreeNode childNode in treeNode.Nodes)
                        {
                            tmpNode = Find(childNode, conInfo);
                            if (tmpNode != null)
                            {
                                return tmpNode;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)("Find node failed" + Constants.vbNewLine + ex.Message),
                                                        true);
                }

                return null;
            }

            public static bool IsEmpty(TreeNode treeNode)
            {
                try
                {
                    if (treeNode.Nodes.Count <= 0)
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("IsEmpty (Tree.Node) failed" + Constants.vbNewLine + ex.Message),
                                                        true);
                }

                return true;
            }

            public static TreeNode AddNode(Tree.Node.Type NodeType, string Text = "")
            {
                try
                {
                    TreeNode nNode = new TreeNode();

                    switch (NodeType)
                    {
                        case Type.Connection:
                            nNode.Text = Language.strNewConnection;
                            nNode.ImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.ConnectionClosed);
                            nNode.SelectedImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.ConnectionClosed);
                            break;
                        case Type.Container:
                            nNode.Text = Language.strNewFolder;
                            nNode.ImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.Container);
                            nNode.SelectedImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.Container);
                            break;
                        case Type.Root:
                            nNode.Text = Language.strNewRoot;
                            nNode.ImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.Root);
                            nNode.SelectedImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.Root);
                            break;
                    }

                    if (Text != "")
                    {
                        nNode.Text = Text;
                    }

                    return nNode;
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)("AddNode failed" + Constants.vbNewLine + ex.Message),
                                                        true);
                }

                return null;
            }

            public static void AddADNodes(string ldapPath)
            {
                try
                {
                    TreeNode adCNode = Tree.Node.AddNode(Type.Container);

                    Container.Info nContI = new Container.Info();
                    nContI.TreeNode = adCNode;
                    nContI.ConnectionInfo = new Info(nContI);

                    if (Tree.Node.SelectedNode != null)
                    {
                        if (Tree.Node.GetNodeType(Tree.Node.SelectedNode) == Tree.Node.Type.Container)
                        {
                            nContI.Parent = Tree.Node.SelectedNode.Tag;
                        }
                    }

                    string strDisplayName;
                    strDisplayName = ldapPath.Remove(0, System.Convert.ToInt32(ldapPath.IndexOf("OU=") + 3));
                    strDisplayName = strDisplayName.Substring(0, strDisplayName.IndexOf(","));

                    nContI.Name = strDisplayName;
                    nContI.TreeNode.Text = strDisplayName;

                    adCNode.Tag = nContI;
                    Runtime.ContainerList.Add(nContI);

                    CreateADSubNodes(adCNode, ldapPath);

                    SelectedNode.Nodes.Add(adCNode);
                    SelectedNode = SelectedNode.Nodes[SelectedNode.Nodes.Count - 1];
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("AddADNodes failed" + Constants.vbNewLine + ex.Message), true);
                }
            }

            private static void CreateADSubNodes(TreeNode rNode, string ldapPath)
            {
                try
                {
                    string strDisplayName;
                    string strDescription;
                    string strHostName;

                    string ldapFilter = "(objectClass=computer)"; //"sAMAccountName=*"

                    DirectorySearcher ldapSearcher = new DirectorySearcher();
                    SearchResultCollection ldapResults;

                    string[] ResultFields = new string[] { "securityEquals", "cn" };

                    ldapSearcher.SearchRoot = new DirectoryEntry(ldapPath);
                    ldapSearcher.PropertiesToLoad.AddRange(ResultFields);
                    ldapSearcher.Filter = ldapFilter;
                    ldapSearcher.SearchScope = SearchScope.OneLevel;
                    ldapResults = ldapSearcher.FindAll();

                    foreach (SearchResult ldapResult in ldapResults)
                    {
                        System.DirectoryServices.DirectoryEntry with_2 = ldapResult.GetDirectoryEntry();
                        strDisplayName = (string)(with_2.Properties["cn"].Value);
                        strDescription = (string)(with_2.Properties["Description"].Value);
                        strHostName = (string)(with_2.Properties["dNSHostName"].Value);

                        TreeNode adNode = Tree.Node.AddNode(Type.Connection, strDisplayName);

                        Info nConI = new Info();
                        Info.Inheritance nInh = new Info.Inheritance(nConI, true);
                        nInh.Description = false;
                        if (rNode.Tag is Container.Info)
                        {
                            nConI.Parent = rNode.Tag;
                        }
                        nConI.Inherit = nInh;
                        nConI.Name = strDisplayName;
                        nConI.Hostname = strHostName;
                        nConI.Description = strDescription;
                        nConI.TreeNode = adNode;
                        adNode.Tag = nConI; //set the nodes tag to the conI
                        //add connection to connections
                        Runtime.ConnectionList.Add(nConI);

                        rNode.Nodes.Add(adNode);
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("CreateADSubNodes failed" + Constants.vbNewLine + ex.Message),
                                                        true);
                }
            }

            public static void CloneNode(TreeNode oldTreeNode, TreeNode parentNode = null)
            {
                try
                {
                    if (GetNodeType(oldTreeNode) == Type.Connection)
                    {
                        Connection.Info oldConnectionInfo = (Info)oldTreeNode.Tag;

                        Connection.Info newConnectionInfo = oldConnectionInfo.Copy();
                        Connection.Info.Inheritance newInheritance = oldConnectionInfo.Inherit.Copy();
                        newInheritance.Parent = newConnectionInfo;
                        newConnectionInfo.Inherit = newInheritance;

                        Runtime.ConnectionList.Add(newConnectionInfo);

                        TreeNode newTreeNode = new TreeNode((string)newConnectionInfo.Name);
                        newTreeNode.Tag = newConnectionInfo;
                        newTreeNode.ImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.ConnectionClosed);
                        newTreeNode.SelectedImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.ConnectionClosed);

                        newConnectionInfo.TreeNode = newTreeNode;

                        if (parentNode == null)
                        {
                            oldTreeNode.Parent.Nodes.Add(newTreeNode);
                            Tree.Node.TreeView.SelectedNode = newTreeNode;
                        }
                        else
                        {
                            var parentContainerInfo = parentNode.Tag as Container.Info;
                            if (parentContainerInfo != null)
                            {
                                newConnectionInfo.Parent = parentContainerInfo;
                            }
                            parentNode.Nodes.Add(newTreeNode);
                        }
                    }
                    else if (GetNodeType(oldTreeNode) == Type.Container)
                    {
                        Container.Info newContainerInfo = (oldTreeNode.Tag as Container.Info).Copy();
                        Connection.Info newConnectionInfo = (oldTreeNode.Tag as Container.Info).ConnectionInfo.Copy();
                        newContainerInfo.ConnectionInfo = newConnectionInfo;

                        TreeNode newTreeNode = new TreeNode(newContainerInfo.Name);
                        newTreeNode.Tag = newContainerInfo;
                        newTreeNode.ImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.Container);
                        newTreeNode.SelectedImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.Container);
                        newContainerInfo.ConnectionInfo.Parent = newContainerInfo;

                        Runtime.ContainerList.Add(newContainerInfo);

                        oldTreeNode.Parent.Nodes.Add(newTreeNode);

                        Tree.Node.TreeView.SelectedNode = newTreeNode;

                        foreach (TreeNode childTreeNode in oldTreeNode.Nodes)
                        {
                            CloneNode(childTreeNode, newTreeNode);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                        string.Format(Language.strErrorCloneNodeFailed, ex.Message));
                }
            }

            public static void SetNodeImage(TreeNode treeNode, Images.Enums.TreeImage Img)
            {
                SetNodeImageIndex(treeNode, (int)Img);
            }

            private delegate void SetNodeImageIndexCB(TreeNode tNode, int ImgIndex);

            private static void SetNodeImageIndex(TreeNode tNode, int ImgIndex)
            {
                if (tNode==null)
                {
                    return;//FixME
                }
                if (_TreeView.InvokeRequired)
                {
                    SetNodeImageIndexCB s = new SetNodeImageIndexCB(SetNodeImageIndex);
                    _TreeView.Invoke(s, new object[] { tNode, ImgIndex });
                }
                else
                {
                    tNode.ImageIndex = ImgIndex;
                    tNode.SelectedImageIndex = ImgIndex;
                }
            }

            // VBConversions Note: Former VB local static variables moved to class level.
            private static TreeNode SetNodeToolTip_old_node;

            public static void SetNodeToolTip(MouseEventArgs e, ToolTip tTip)
            {
                try
                {
                    if (Settings.Default.ShowDescriptionTooltipsInTree)
                    {
                        //Find the node under the mouse.
                        // static TreeNode old_node; VBConversions Note: Static variable moved to class level and renamed SetNodeToolTip_old_node. Local static variables are not supported in C#.
                        TreeNode new_node = _TreeView.GetNodeAt(e.X, e.Y);
                        if (new_node == SetNodeToolTip_old_node)
                        {
                            return;
                        }
                        SetNodeToolTip_old_node = new_node;

                        //See if we have a node.
                        if (SetNodeToolTip_old_node == null)
                        {
                            tTip.SetToolTip(_TreeView, "");
                        }
                        else
                        {
                            //Get this node's object data.
                            if (GetNodeType(SetNodeToolTip_old_node) == Type.Connection)
                            {
                                tTip.SetToolTip(_TreeView, (SetNodeToolTip_old_node.Tag as Info).Description);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("SetNodeToolTip failed" + Constants.vbNewLine + ex.Message),
                                                        true);
                }
            }

            public static void DeleteSelectedNode()
            {
                try
                {
                    if (SelectedNode == null)
                    {
                        return;
                    }

                    if (Tree.Node.GetNodeType(SelectedNode) == Type.Root)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                            "The root item cannot be deleted!");
                    }
                    else if (Tree.Node.GetNodeType(SelectedNode) == Type.Container)
                    {
                        if (Tree.Node.IsEmpty(SelectedNode) == false)
                        {
                            if (
                                Interaction.MsgBox(
                                    string.Format(Language.strConfirmDeleteNodeFolder, SelectedNode.Text),
                                    MsgBoxStyle.YesNo | MsgBoxStyle.Question, null) == MsgBoxResult.Yes)
                            {
                                SelectedNode.Remove();
                            }
                        }
                        else
                        {
                            if (
                                Interaction.MsgBox(
                                    string.Format(Language.strConfirmDeleteNodeFolderNotEmpty, SelectedNode.Text),
                                    MsgBoxStyle.YesNo | MsgBoxStyle.Question, null) == MsgBoxResult.Yes)
                            {
                                foreach (TreeNode tNode in SelectedNode.Nodes)
                                {
                                    tNode.Remove();
                                }
                                SelectedNode.Remove();
                            }
                        }
                    }
                    else if (Tree.Node.GetNodeType(SelectedNode) == Type.Connection)
                    {
                        if (
                            Interaction.MsgBox(
                                string.Format(Language.strConfirmDeleteNodeConnection, SelectedNode.Text),
                                MsgBoxStyle.YesNo | MsgBoxStyle.Question, null) == MsgBoxResult.Yes)
                        {
                            SelectedNode.Remove();
                        }
                    }
                    else
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                            "Tree item type is unknown so it cannot be deleted!");
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("Deleting selected node failed" + Constants.vbNewLine +
                                                         ex.Message), true);
                }
            }

            public static void StartRenameSelectedNode()
            {
                if (SelectedNode != null)
                {
                    Runtime.Windows.treeForm.cMenTreeDelete.ShortcutKeys = Keys.None;
                    SelectedNode.BeginEdit();
                }
            }

            public static void FinishRenameSelectedNode(string NewName)
            {
                Runtime.Windows.treeForm.cMenTreeDelete.ShortcutKeys = Keys.Delete;
                if ((NewName != null) && (NewName.Length > 0))
                {
                    NewLateBinding.LateSetComplex(SelectedNode.Tag, null, "Name", new object[] { NewName }, null, null,
                                                  false, true);
                }
            }

            public static void MoveNodeUp()
            {
                try
                {
                    if (SelectedNode != null)
                    {
                        if (!(SelectedNode.PrevNode == null))
                        {
                            TreeView.BeginUpdate();
                            TreeView.Sorted = false;

                            TreeNode newNode = (TreeNode)SelectedNode.Clone();
                            SelectedNode.Parent.Nodes.Insert(SelectedNode.Index - 1, newNode);
                            SelectedNode.Remove();
                            SelectedNode = newNode;

                            TreeView.EndUpdate();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("MoveNodeUp failed" + Constants.vbNewLine + ex.Message), true);
                }
            }

            public static void MoveNodeDown()
            {
                try
                {
                    if (SelectedNode != null)
                    {
                        if (!(SelectedNode.NextNode == null))
                        {
                            TreeView.BeginUpdate();
                            TreeView.Sorted = false;

                            TreeNode newNode = (TreeNode)SelectedNode.Clone();
                            SelectedNode.Parent.Nodes.Insert(SelectedNode.Index + 2, newNode);
                            SelectedNode.Remove();
                            SelectedNode = newNode;

                            TreeView.EndUpdate();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("MoveNodeDown failed" + Constants.vbNewLine + ex.Message), true);
                }
            }

            public static void ExpandAllNodes()
            {
                TreeView.BeginUpdate();
                TreeView.ExpandAll();
                TreeView.EndUpdate();
            }

            public static void CollapseAllNodes()
            {
                TreeView.BeginUpdate();
                foreach (TreeNode treeNode in TreeView.Nodes[0].Nodes)
                {
                    treeNode.Collapse(false);
                }
                TreeView.EndUpdate();
            }

            public static void Sort(TreeNode treeNode, Tools.Controls.TreeNodeSorter.SortType sortType)
            {
                try
                {
                    TreeView.BeginUpdate();
                    if (treeNode == null)
                    {
                        treeNode = TreeView.Nodes[0];
                    }
                    else if (Tree.Node.GetNodeType(treeNode) == Type.Connection)
                    {
                        treeNode = treeNode.Parent;
                    }

                    Tools.Controls.TreeNodeSorter ns = new Tools.Controls.TreeNodeSorter(treeNode, sortType);

                    TreeView.TreeViewNodeSorter = ns;
                    TreeView.Sort();

                    foreach (TreeNode childNode in treeNode.Nodes)
                    {
                        if (GetNodeType(childNode) == Type.Container)
                        {
                            Sort(childNode, sortType);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("Sort nodes failed" + Constants.vbNewLine + ex.Message), true);
                }
                finally
                {
                    TreeView.EndUpdate();
                }
            }

            private delegate void ResetTreeDelegate();

            public static void ResetTree()
            {
                if (TreeView.InvokeRequired)
                {
                    ResetTreeDelegate resetTreeDelegate = new ResetTreeDelegate(ResetTree);
                    Runtime.Windows.treeForm.Invoke(resetTreeDelegate);
                }
                else
                {
                    TreeView.BeginUpdate();
                    TreeView.Nodes.Clear();
                    TreeView.Nodes.Add(Language.strConnections);
                    TreeView.EndUpdate();
                }
            }
        }
    }
}