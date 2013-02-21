using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using mRemoteNC.Connection;
using My;

namespace mRemoteNC.Config
{
    namespace Connections
    {
        public class Load
        {
            #region Private Properties

            private XmlDocument xDom;
            private double confVersion;

            private string pW = mRemoteNC.AppInfo.General.EncryptionKey;

            private SqlConnection sqlCon;
            private SqlCommand sqlQuery;
            private SqlDataReader sqlRd;

            private TreeNode selNode;

            #endregion Private Properties

            #region Public Properties

            private bool _UseSQL;

            public bool UseSQL
            {
                get { return _UseSQL; }
                set { _UseSQL = value; }
            }

            private string _SQLHost;

            public string SQLHost
            {
                get { return _SQLHost; }
                set { _SQLHost = value; }
            }

            private string _SQLDatabaseName;

            public string SQLDatabaseName
            {
                get { return _SQLDatabaseName; }
                set { _SQLDatabaseName = value; }
            }

            private string _SQLUsername;

            public string SQLUsername
            {
                get { return _SQLUsername; }
                set { _SQLUsername = value; }
            }

            private string _SQLPassword;

            public string SQLPassword
            {
                get { return _SQLPassword; }
                set { _SQLPassword = value; }
            }

            private bool _SQLUpdate;

            public bool SQLUpdate
            {
                get { return _SQLUpdate; }
                set { _SQLUpdate = value; }
            }

            private string _PreviousSelected;

            public string PreviousSelected
            {
                get { return _PreviousSelected; }
                set { _PreviousSelected = value; }
            }

            private string _ConnectionFileName;

            public string ConnectionFileName
            {
                get { return this._ConnectionFileName; }
                set { this._ConnectionFileName = value; }
            }

            private TreeNode _RootTreeNode;

            public TreeNode RootTreeNode
            {
                get { return this._RootTreeNode; }
                set { this._RootTreeNode = value; }
            }

            private bool _Import;

            public bool Import
            {
                get { return this._Import; }
                set { this._Import = value; }
            }

            private Connection.List _ConnectionList;

            public Connection.List ConnectionList
            {
                get { return this._ConnectionList; }
                set { this._ConnectionList = value; }
            }

            private Container.List _ContainerList;

            public Container.List ContainerList
            {
                get { return this._ContainerList; }
                set { this._ContainerList = value; }
            }

            private Connection.List _PreviousConnectionList;

            public Connection.List PreviousConnectionList
            {
                get { return _PreviousConnectionList; }
                set { _PreviousConnectionList = value; }
            }

            private Container.List _PreviousContainerList;

            public Container.List PreviousContainerList
            {
                get { return _PreviousContainerList; }
                set { _PreviousContainerList = value; }
            }

            #endregion Public Properties

            #region Public Methods

            public void LoadConnections()
            {
                if (_UseSQL)
                {
                    LoadFromSQL();
                    Runtime.SetMainFormText("SQL Server");
                }
                else
                {
                    string strCons = DecryptCompleteFile();
                    LoadFromXML(strCons);
                }

                if (Import == false)
                {
                    Runtime.SetMainFormText(ConnectionFileName);
                }
            }

            #endregion Public Methods

            #region SQL

            private void LoadFromSQL()
            {
                try
                {
                    Runtime.IsConnectionsFileLoaded = false;

                    if (_SQLUsername != "")
                    {
                        sqlCon =
                            new SqlConnection(
                                (string)
                                ("Data Source=" + _SQLHost + ";Initial Catalog=" + _SQLDatabaseName + ";User Id=" +
                                 _SQLUsername + ";Password=" + _SQLPassword));
                    }
                    else
                    {
                        sqlCon =
                            new SqlConnection("Data Source=" + _SQLHost + ";Initial Catalog=" + _SQLDatabaseName +
                                              ";Integrated Security=True");
                    }

                    sqlCon.Open();

                    sqlQuery = new SqlCommand("SELECT * FROM tblRoot", sqlCon);
                    sqlRd = sqlQuery.ExecuteReader(CommandBehavior.CloseConnection);

                    sqlRd.Read();

                    if (sqlRd.HasRows == false)
                    {
                        Runtime.SaveConnections();

                        sqlQuery = new SqlCommand("SELECT * FROM tblRoot", sqlCon);
                        sqlRd = sqlQuery.ExecuteReader(CommandBehavior.CloseConnection);

                        sqlRd.Read();
                    }

                    this.confVersion = Convert.ToDouble(sqlRd["confVersion"], CultureInfo.InvariantCulture);

                    TreeNode rootNode;
                    rootNode = new TreeNode((string)(sqlRd["Name"]));

                    Root.Info rInfo = new Root.Info(Root.Info.RootType.Connection);
                    rInfo.Name = rootNode.Text;
                    rInfo.TreeNode = rootNode;

                    rootNode.Tag = rInfo;
                    rootNode.ImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.Root);
                    rootNode.SelectedImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.Root);

                    if (Security.Crypt.Decrypt((string)(sqlRd["Protected"]), pW) != "ThisIsNotProtected")
                    {
                        if (Authenticate((string)(sqlRd["Protected"]), false, rInfo) == false)
                        {
                            Settings.Default.LoadConsFromCustomLocation = false;
                            Settings.Default.CustomConsPath = "";
                            rootNode.Remove();
                            return;
                        }
                    }

                    //Me._RootTreeNode.Text = rootNode.Text
                    //Me._RootTreeNode.Tag = rootNode.Tag
                    //Me._RootTreeNode.ImageIndex = Images.Enums.TreeImage.Root
                    //Me._RootTreeNode.SelectedImageIndex = Images.Enums.TreeImage.Root

                    sqlRd.Close();

                    // SECTION 3. Populate the TreeView with the DOM nodes.
                    AddNodesFromSQL(rootNode);
                    //AddNodeFromXml(xDom.DocumentElement, Me._RootTreeNode)

                    rootNode.Expand();

                    //expand containers
                    foreach (Container.Info contI in this._ContainerList)
                    {
                        if (contI.IsExpanded == true)
                        {
                            contI.TreeNode.Expand();
                        }
                    }

                    //open connections from last mremote session
                    if (Settings.Default.OpenConsFromLastSession == true && Settings.Default.NoReconnect == false)
                    {
                        foreach (Connection.Info conI in this._ConnectionList)
                        {
                            if (conI.PleaseConnect == true)
                            {
                                Runtime.OpenConnection(conI);
                            }
                        }
                    }

                    //Tree.Node.TreeView.Nodes.Clear()
                    //Tree.Node.TreeView.Nodes.Add(rootNode)

                    AddNodeToTree(rootNode);
                    SetSelectedNode(selNode);

                    Runtime.IsConnectionsFileLoaded = true;
                    //Runtime.Windows.treeForm.InitialRefresh()

                    sqlCon.Close();
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strLoadFromSqlFailed + Constants.vbNewLine +
                                                        ex.Message, true);
                }
            }

            private delegate void AddNodeToTreeCB(TreeNode TreeNode);

            private void AddNodeToTree(TreeNode TreeNode)
            {
                if (Tree.Node.TreeView.InvokeRequired)
                {
                    AddNodeToTreeCB d = new AddNodeToTreeCB(AddNodeToTree);
                    Runtime.Windows.treeForm.Invoke(d, new object[] { TreeNode });
                }
                else
                {
                    Runtime.Windows.treeForm.tvConnections.Nodes.Clear();
                    Runtime.Windows.treeForm.tvConnections.Nodes.Add(TreeNode);
                    Runtime.Windows.treeForm.InitialRefresh();
                }
            }

            private delegate void SetSelectedNodeCB(TreeNode TreeNode);

            private void SetSelectedNode(TreeNode TreeNode)
            {
                if (Tree.Node.TreeView.InvokeRequired)
                {
                    SetSelectedNodeCB d = new SetSelectedNodeCB(SetSelectedNode);
                    Runtime.Windows.treeForm.Invoke(d, new object[] { TreeNode });
                }
                else
                {
                    Runtime.Windows.treeForm.tvConnections.SelectedNode = TreeNode;
                }
            }

            private void AddNodesFromSQL(TreeNode baseNode)
            {
                try
                {
                    sqlCon.Open();
                    sqlQuery = new SqlCommand("SELECT * FROM tblCons ORDER BY PositionID ASC", sqlCon);
                    sqlRd = sqlQuery.ExecuteReader(CommandBehavior.CloseConnection);

                    if (sqlRd.HasRows == false)
                    {
                        return;
                    }

                    TreeNode tNode;

                    while (sqlRd.Read())
                    {
                        tNode = new TreeNode((string)(sqlRd["Name"]));
                        //baseNode.Nodes.Add(tNode)

                        if (Tree.Node.GetNodeTypeFromString((string)(sqlRd["Type"])) == Tree.Node.Type.Connection)
                        {
                            Connection.Info conI = GetConnectionInfoFromSQL();
                            conI.TreeNode = tNode;
                            //conI.Parent = _previousContainer 'NEW

                            this._ConnectionList.Add(conI);

                            tNode.Tag = conI;

                            if (SQLUpdate == true)
                            {
                                Connection.Info prevCon = PreviousConnectionList.FindByConstantID(conI.ConstantID);

                                if (prevCon != null)
                                {
                                    foreach (mRemoteNC.Base prot in prevCon.OpenConnections)
                                    {
                                        prot.InterfaceControl.Info = conI;
                                        conI.OpenConnections.Add(prot);
                                    }

                                    if (conI.OpenConnections.Count > 0)
                                    {
                                        tNode.ImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.ConnectionOpen);
                                        tNode.SelectedImageIndex =
                                            System.Convert.ToInt32(Images.Enums.TreeImage.ConnectionOpen);
                                    }
                                    else
                                    {
                                        tNode.ImageIndex =
                                            System.Convert.ToInt32(Images.Enums.TreeImage.ConnectionClosed);
                                        tNode.SelectedImageIndex =
                                            System.Convert.ToInt32(Images.Enums.TreeImage.ConnectionClosed);
                                    }
                                }
                                else
                                {
                                    tNode.ImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.ConnectionClosed);
                                    tNode.SelectedImageIndex =
                                        System.Convert.ToInt32(Images.Enums.TreeImage.ConnectionClosed);
                                }

                                if (conI.ConstantID == _PreviousSelected)
                                {
                                    selNode = tNode;
                                }
                            }
                            else
                            {
                                tNode.ImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.ConnectionClosed);
                                tNode.SelectedImageIndex =
                                    System.Convert.ToInt32(Images.Enums.TreeImage.ConnectionClosed);
                            }
                        }
                        else if (Tree.Node.GetNodeTypeFromString((string)(sqlRd["Type"])) == Tree.Node.Type.Container)
                        {
                            Container.Info contI = new Container.Info();
                            //If tNode.Parent IsNot Nothing Then
                            //    If Tree.Node.GetNodeType(tNode.Parent) = Tree.Node.Type.Container Then
                            //        contI.Parent = tNode.Parent.Tag
                            //    End If
                            //End If
                            //_previousContainer = contI 'NEW
                            contI.TreeNode = tNode;

                            contI.Name = (string)(sqlRd["Name"]);

                            Connection.Info conI;

                            conI = GetConnectionInfoFromSQL();

                            conI.Parent = contI;
                            conI.IsContainer = true;
                            contI.ConnectionInfo = conI;

                            if (SQLUpdate == true)
                            {
                                Container.Info prevCont = PreviousContainerList.FindByConstantID(conI.ConstantID);
                                if (prevCont != null)
                                {
                                    contI.IsExpanded = prevCont.IsExpanded;
                                }

                                if (conI.ConstantID == _PreviousSelected)
                                {
                                    selNode = tNode;
                                }
                            }
                            else
                            {
                                if (Convert.ToBoolean(sqlRd["Expanded"]) == true)
                                {
                                    contI.IsExpanded = true;
                                }
                                else
                                {
                                    contI.IsExpanded = false;
                                }
                            }

                            this._ContainerList.Add(contI);
                            this._ConnectionList.Add(conI);

                            tNode.Tag = contI;
                            tNode.ImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.Container);
                            tNode.SelectedImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.Container);
                        }

                        if (Convert.ToInt32(sqlRd["ParentID"]) != 0)
                        {
                            TreeNode pNode = Tree.Node.GetNodeFromConstantID((string)(sqlRd["ParentID"]));

                            if (pNode != null)
                            {
                                pNode.Nodes.Add(tNode);

                                if (Tree.Node.GetNodeType(tNode) == Tree.Node.Type.Connection)
                                {
                                    (tNode.Tag as Connection.Info).Parent = pNode.Tag;
                                }
                                else if (Tree.Node.GetNodeType(tNode) == Tree.Node.Type.Container)
                                {
                                    (tNode.Tag as Container.Info).Parent = pNode.Tag;
                                }
                            }
                            else
                            {
                                baseNode.Nodes.Add(tNode);
                            }
                        }
                        else
                        {
                            baseNode.Nodes.Add(tNode);
                        }

                        //AddNodesFromSQL(tNode)
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strAddNodesFromSqlFailed + Constants.vbNewLine +
                                                        ex.Message, true);
                }
            }

            private Connection.Info GetConnectionInfoFromSQL()
            {
                try
                {
                    Connection.Info conI = new Connection.Info();

                    conI.PositionID = Convert.ToInt32(sqlRd["PositionID"]);
                    conI.ConstantID = sqlRd["ConstantID"].ToString();
                    conI.Name = sqlRd["Name"].ToString();
                    conI.Description = sqlRd["Description"].ToString();
                    conI.Hostname = sqlRd["Hostname"].ToString();
                    conI.Username = sqlRd["Username"].ToString();
                    conI.Password = Security.Crypt.Decrypt((string)(sqlRd["Password"]), pW);
                    conI.Domain = sqlRd["DomainName"].ToString();
                    conI.DisplayWallpaper = (bool)sqlRd["DisplayWallpaper"];
                    conI.DisplayThemes = (bool)sqlRd["DisplayThemes"];
                    conI.CacheBitmaps = (bool)sqlRd["CacheBitmaps"];
                    conI.UseConsoleSession = (bool)sqlRd["ConnectToConsole"];

                    conI.RedirectDiskDrives = (bool)sqlRd["RedirectDiskDrives"];
                    conI.RedirectPrinters = (bool)sqlRd["RedirectPrinters"];
                    conI.RedirectPorts = (bool)sqlRd["RedirectPorts"];
                    conI.RedirectSmartCards = (bool)sqlRd["RedirectSmartCards"];
                    conI.RedirectKeys = (bool)sqlRd["RedirectKeys"];
                    conI.RedirectSound =
                        (RDP.RDPSounds)Tools.Misc.StringToEnum(typeof(mRemoteNC.RDP.RDPSounds),
                                                                sqlRd["RedirectSound"].ToString());

                    conI.Protocol =
                        (Protocols)
                        Tools.Misc.StringToEnum(typeof(mRemoteNC.Protocols), sqlRd["Protocol"].ToString());
                    conI.Port = Convert.ToInt32(sqlRd["Port"]);
                    conI.PuttySession = sqlRd["PuttySession"].ToString();

                    conI.Colors =
                        (RDP.RDPColors)
                        Tools.Misc.StringToEnum(typeof(mRemoteNC.RDP.RDPColors), sqlRd["Colors"].ToString());
                    conI.Resolution =
                        (RDP.RDPResolutions)Tools.Misc.StringToEnum(typeof(mRemoteNC.RDP.RDPResolutions),
                                                                     sqlRd["Resolution"].ToString());

                    conI.Inherit = new Connection.Info.Inheritance(conI);
                    conI.Inherit.CacheBitmaps = (bool)sqlRd["InheritCacheBitmaps"];
                    conI.Inherit.Colors = (bool)sqlRd["InheritColors"];
                    conI.Inherit.Description = (bool)sqlRd["InheritDescription"];
                    conI.Inherit.DisplayThemes = (bool)sqlRd["InheritDisplayThemes"];
                    conI.Inherit.DisplayWallpaper = (bool)sqlRd["InheritDisplayWallpaper"];
                    conI.Inherit.Domain = (bool)sqlRd["InheritDomain"];
                    conI.Inherit.Icon = (bool)sqlRd["InheritIcon"];
                    conI.Inherit.Panel = (bool)sqlRd["InheritPanel"];
                    conI.Inherit.Password = (bool)sqlRd["InheritPassword"];
                    conI.Inherit.Port = (bool)sqlRd["InheritPort"];
                    conI.Inherit.Protocol = (bool)sqlRd["InheritProtocol"];
                    conI.Inherit.PuttySession = (bool)sqlRd["InheritPuttySession"];
                    conI.Inherit.RedirectDiskDrives = (bool)sqlRd["InheritRedirectDiskDrives"];
                    conI.Inherit.RedirectKeys = (bool)sqlRd["InheritRedirectKeys"];
                    conI.Inherit.RedirectPorts = (bool)sqlRd["InheritRedirectPorts"];
                    conI.Inherit.RedirectPrinters = (bool)sqlRd["InheritRedirectPrinters"];
                    conI.Inherit.RedirectSmartCards = (bool)sqlRd["InheritRedirectSmartCards"];
                    conI.Inherit.RedirectSound = (bool)sqlRd["InheritRedirectSound"];
                    conI.Inherit.Resolution = (bool)sqlRd["InheritResolution"];
                    conI.Inherit.UseConsoleSession = (bool)sqlRd["InheritUseConsoleSession"];
                    conI.Inherit.Username = (bool)sqlRd["InheritUsername"];

                    conI.Icon = sqlRd["Icon"].ToString();
                    conI.Panel = sqlRd["Panel"].ToString();

                    if (this.confVersion > 1.5) //1.6
                    {
                        conI.ICAEncryption = (ICA.EncryptionStrength)Tools.Misc.StringToEnum(
                            typeof(mRemoteNC.ICA.EncryptionStrength),
                            sqlRd["ICAEncryptionStrength"].ToString());
                        conI.Inherit.ICAEncryption = (bool)sqlRd["InheritICAEncryptionStrength"];

                        conI.PreExtApp = sqlRd["PreExtApp"].ToString();
                        conI.PostExtApp = sqlRd["PostExtApp"].ToString();
                        conI.Inherit.PreExtApp = (bool)sqlRd["InheritPreExtApp"];
                        conI.Inherit.PostExtApp = (bool)sqlRd["InheritPostExtApp"];
                    }

                    if (this.confVersion > 1.6) //1.7
                    {
                        conI.VNCCompression =
                            (VNC.Compression)Tools.Misc.StringToEnum(typeof(mRemoteNC.VNC.Compression),
                                                                      sqlRd["VNCCompression"].ToString());
                        conI.VNCEncoding =
                            (mRemoteNC.VNC.Encoding)
                            Tools.Misc.StringToEnum(typeof(mRemoteNC.VNC.Encoding),
                                                    sqlRd["VNCEncoding"].ToString());
                        conI.VNCAuthMode =
                            (VNC.AuthMode)Tools.Misc.StringToEnum(typeof(mRemoteNC.VNC.AuthMode),
                                                                   sqlRd["VNCAuthMode"].ToString());
                        conI.VNCProxyType =
                            (VNC.ProxyType)Tools.Misc.StringToEnum(typeof(mRemoteNC.VNC.ProxyType),
                                                                    sqlRd["VNCProxyType"].ToString());
                        conI.VNCProxyIP = sqlRd["VNCProxyIP"].ToString();
                        conI.VNCProxyPort = Convert.ToInt32(sqlRd["VNCProxyPort"].ToString());
                        conI.VNCProxyUsername = sqlRd["VNCProxyUsername"].ToString();
                        conI.VNCProxyPassword = Security.Crypt.Decrypt((string)(sqlRd["VNCProxyPassword"]), pW);
                        conI.VNCColors = (VNC.Colors)Tools.Misc.StringToEnum(typeof(mRemoteNC.VNC.Colors),
                                                                              sqlRd["VNCColors"].ToString());
                        conI.VNCSmartSizeMode =
                            (VNC.SmartSizeMode)Tools.Misc.StringToEnum(typeof(mRemoteNC.VNC.SmartSizeMode),
                                                                        sqlRd["VNCSmartSizeMode"].ToString());
                        conI.VNCViewOnly = (bool)sqlRd["VNCViewOnly"];

                        conI.Inherit.VNCCompression = (bool)sqlRd["InheritVNCCompression"];
                        conI.Inherit.VNCEncoding = (bool)sqlRd["InheritVNCEncoding"];
                        conI.Inherit.VNCAuthMode = (bool)sqlRd["InheritVNCAuthMode"];
                        conI.Inherit.VNCProxyType = (bool)sqlRd["InheritVNCProxyType"];
                        conI.Inherit.VNCProxyIP = (bool)sqlRd["InheritVNCProxyIP"];
                        conI.Inherit.VNCProxyPort = (bool)sqlRd["InheritVNCProxyPort"];
                        conI.Inherit.VNCProxyUsername = (bool)sqlRd["InheritVNCProxyUsername"];
                        conI.Inherit.VNCProxyPassword = (bool)sqlRd["InheritVNCProxyPassword"];
                        conI.Inherit.VNCColors = (bool)sqlRd["InheritVNCColors"];
                        conI.Inherit.VNCSmartSizeMode = (bool)sqlRd["InheritVNCSmartSizeMode"];
                        conI.Inherit.VNCViewOnly = (bool)sqlRd["InheritVNCViewOnly"];
                    }

                    if (this.confVersion > 1.7) //1.8
                    {
                        conI.RDPAuthenticationLevel = (RDP.AuthenticationLevel)
                                                      Tools.Misc.StringToEnum(
                                                          typeof(mRemoteNC.RDP.AuthenticationLevel),
                                                          sqlRd["RDPAuthenticationLevel"].ToString());

                        conI.Inherit.RDPAuthenticationLevel = (bool)sqlRd["InheritRDPAuthenticationLevel"];
                    }

                    if (this.confVersion > 1.8) //1.9
                    {
                        conI.RenderingEngine = (HTTPBase.RenderingEngine)
                                               Tools.Misc.StringToEnum(
                                                   typeof(mRemoteNC.HTTPBase.RenderingEngine),
                                                   sqlRd["RenderingEngine"].ToString());
                        conI.MacAddress = sqlRd["MacAddress"].ToString();

                        conI.Inherit.RenderingEngine = (bool)sqlRd["InheritRenderingEngine"];
                        conI.Inherit.MacAddress = (bool)sqlRd["InheritMacAddress"];
                    }

                    if (this.confVersion > 1.9) //2.0
                    {
                        conI.UserField = sqlRd["UserField"].ToString();

                        conI.Inherit.UserField = (bool)sqlRd["InheritUserField"];
                    }

                    if (this.confVersion > 2.0) //2.1
                    {
                        conI.ExtApp = sqlRd["ExtApp"].ToString();

                        conI.Inherit.ExtApp = (bool)sqlRd["InheritExtApp"];
                    }

                    if (this.confVersion >= 2.2)
                    {
                        conI.RDGatewayUsageMethod = (RDP.RDGatewayUsageMethod)
                                                    Tools.Misc.StringToEnum(typeof(RDP.RDGatewayUsageMethod),
                                                                            sqlRd["RDGatewayUsageMethod"].ToString());
                        conI.RDGatewayHostname = sqlRd["RDGatewayHostname"].ToString();
                        conI.RDGatewayUseConnectionCredentials = (RDP.RDGatewayUseConnectionCredentials)
                                                                 Tools.Misc.StringToEnum(
                                                                     typeof(RDP.RDGatewayUseConnectionCredentials),
                                                                     sqlRd["RDGatewayUseConnectionCredentials"].ToString
                                                                         ());
                        conI.RDGatewayUsername = sqlRd["RDGatewayUsername"].ToString();
                        conI.RDGatewayPassword = Security.Crypt.Decrypt((string)(sqlRd["RDGatewayPassword"]), pW);
                        conI.RDGatewayDomain = sqlRd["RDGatewayDomain"].ToString();
                        conI.Inherit.RDGatewayUsageMethod = (bool)sqlRd["InheritRDGatewayUsageMethod"];
                        conI.Inherit.RDGatewayHostname = (bool)sqlRd["InheritRDGatewayHostname"];
                        conI.Inherit.RDGatewayUsername = (bool)sqlRd["InheritRDGatewayUsername"];
                        conI.Inherit.RDGatewayPassword = (bool)sqlRd["InheritRDGatewayPassword"];
                        conI.Inherit.RDGatewayDomain = (bool)sqlRd["InheritRDGatewayDomain"];
                    }

                    if (this.confVersion >= 2.3)
                    {
                        conI.EnableFontSmoothing = (bool)sqlRd["EnableFontSmoothing"];
                        conI.EnableDesktopComposition = (bool)sqlRd["EnableDesktopComposition"];
                        conI.Inherit.EnableFontSmoothing = (bool)sqlRd["InheritEnableFontSmoothing"];
                        conI.Inherit.EnableDesktopComposition = (bool)sqlRd["InheritEnableDesktopComposition"];
                    }

                    if (confVersion >= 2.4)
                    {
                        conI.UseCredSsp = (bool)sqlRd["UseCredSsp"];
                        conI.Inherit.UseCredSsp = (bool)sqlRd["InheritUseCredSsp"];
                    }

                    if (confVersion >= 2.5)
                    {
                        conI.ConnectOnStartup = (bool)sqlRd["ConnectOnStartup"];
                    }

                    if (SQLUpdate == true)
                    {
                        conI.PleaseConnect = (bool)sqlRd["Connected"];
                    }

                    return conI;
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strGetConnectionInfoFromSqlFailed +
                                                        Constants.vbNewLine + ex.Message, true);
                }

                return null;
            }

            #endregion SQL

            #region XML

            private string DecryptCompleteFile()
            {
                StreamReader sRd = new StreamReader(this._ConnectionFileName);

                string strCons;
                strCons = sRd.ReadToEnd();
                sRd.Close();

                if (strCons != "")
                {
                    string strDecr = "";
                    bool notDecr = true;

                    if (strCons.Contains("<?xml version=\"1.0\" encoding=\"utf-8\"?>"))
                    {
                        strDecr = strCons;
                        return strDecr;
                    }

                    try
                    {
                        strDecr = Security.Crypt.Decrypt(strCons, pW);

                        if (strDecr != strCons)
                        {
                            notDecr = false;
                        }
                        else
                        {
                            notDecr = true;
                        }
                    }
                    catch (Exception)
                    {
                        notDecr = true;
                    }

                    if (notDecr)
                    {
                        if (Authenticate(strCons, true) == true)
                        {
                            strDecr = Security.Crypt.Decrypt(strCons, pW);
                            notDecr = false;
                        }
                        else
                        {
                            notDecr = true;
                        }

                        if (notDecr == false)
                        {
                            return strDecr;
                        }
                    }
                    else
                    {
                        return strDecr;
                    }
                }

                return "";
            }

            private void LoadFromXML(string cons = "")
            {
                try
                {
                    Runtime.IsConnectionsFileLoaded = false;

                    // SECTION 1. Create a DOM Document and load the XML data into it.
                    this.xDom = new XmlDocument();
                    if (cons != "")
                    {
                        xDom.LoadXml(cons);
                    }
                    else
                    {
                        xDom.Load(this._ConnectionFileName);
                    }

                    if (xDom.DocumentElement.HasAttribute("ConfVersion"))
                    {
                        this.confVersion = Convert.ToDouble(xDom.DocumentElement.Attributes["ConfVersion"].Value.Replace(",", "."),
                                                            CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                            Language.strOldConffile);
                    }

                    // SECTION 2. Initialize the treeview control.
                    TreeNode rootNode;

                    string rootNodeName = "";
                    if (xDom.DocumentElement.HasAttribute("Name"))
                    {
                        rootNodeName = (string)(xDom.DocumentElement.Attributes["Name"].Value.Trim());
                    }
                    if (!string.IsNullOrEmpty(rootNodeName))
                    {
                        rootNode = new TreeNode(rootNodeName);
                    }
                    else
                    {
                        rootNode = new TreeNode(xDom.DocumentElement.Name);
                    }

                    Root.Info rInfo = new Root.Info(Root.Info.RootType.Connection);
                    rInfo.Name = rootNode.Text;
                    rInfo.TreeNode = rootNode;

                    rootNode.Tag = rInfo;

                    if (this.confVersion > 1.3) //1.4
                    {
                        if (Security.Crypt.Decrypt((string)(xDom.DocumentElement.Attributes["Protected"].Value), pW) !=
                            "ThisIsNotProtected")
                        {
                            if (Authenticate(xDom.DocumentElement.Attributes["Protected"].Value, false, rInfo) == false)
                            {
                                Settings.Default.LoadConsFromCustomLocation = false;
                                Settings.Default.CustomConsPath = "";
                                //_RootTreeNode.Remove();
                                return;
                            }
                        }
                    }

                    bool imp = false;

                    if (this.confVersion > 0.9) //1.0
                    {
                        if (Convert.ToBoolean(xDom.DocumentElement.Attributes["Export"].Value) == true)
                        {
                            imp = true;
                        }
                    }

                    if (this._Import == true && imp == false)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.InformationMsg,
                                                            Language.strCannotImportNormalSessionFile);

                        return;
                    }

                    if (imp == false)
                    {
                        this._RootTreeNode.Text = rootNode.Text;
                        this._RootTreeNode.Tag = rootNode.Tag;
                        this._RootTreeNode.ImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.Root);
                        this._RootTreeNode.SelectedImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.Root);
                    }

                    // SECTION 3. Populate the TreeView with the DOM nodes.
                    AddNodeFromXml(xDom.DocumentElement, this._RootTreeNode);

                    this._RootTreeNode.Expand();

                    //expand containers
                    foreach (Container.Info contI in this._ContainerList)
                    {
                        if (contI.IsExpanded == true)
                        {
                            contI.TreeNode.Expand();
                        }
                    }

                    //open connections from last mremote session
                    if (Settings.Default.OpenConsFromLastSession == true && Settings.Default.NoReconnect == false)
                    {
                        foreach (Connection.Info conI in this._ConnectionList)
                        {
                            if (conI.PleaseConnect == true)
                            {
                                Runtime.OpenConnection(conI);
                            }
                        }
                    }

                    this._RootTreeNode.EnsureVisible();

                    Runtime.IsConnectionsFileLoaded = true;
                    Runtime.Windows.treeForm.InitialRefresh();
                }
                catch (Exception ex)
                {
                    Runtime.IsConnectionsFileLoaded = false;
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strLoadFromXmlFailed + Constants.vbNewLine +
                                                        ex.Message + ex.StackTrace, true);
                    throw;
                }
            }

            private Container.Info _previousContainer;

            private void AddNodeFromXml(XmlNode parentXmlNode, TreeNode parentTreeNode)
            {
                try
                {
                    // Loop through the XML nodes until the leaf is reached.
                    // Add the nodes to the TreeView during the looping process.
                    if (parentXmlNode.HasChildNodes)
                    {
                        foreach (XmlNode xmlNode in parentXmlNode.ChildNodes)
                        {
                            TreeNode treeNode = new TreeNode((string)(xmlNode.Attributes["Name"].Value));
                            parentTreeNode.Nodes.Add(treeNode);

                            if (Tree.Node.GetNodeTypeFromString(xmlNode.Attributes["Type"].Value) ==
                                Tree.Node.Type.Connection) //connection info
                            {
                                Connection.Info connectionInfo = GetConnectionInfoFromXml(xmlNode);
                                connectionInfo.TreeNode = treeNode;
                                connectionInfo.Parent = _previousContainer; //NEW

                                ConnectionList.Add(connectionInfo);

                                treeNode.Tag = connectionInfo;
                                treeNode.ImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.ConnectionClosed);
                                treeNode.SelectedImageIndex =
                                    System.Convert.ToInt32(Images.Enums.TreeImage.ConnectionClosed);
                            }
                            else if (Tree.Node.GetNodeTypeFromString((string)(xmlNode.Attributes["Type"].Value)) ==
                                     Tree.Node.Type.Container) //container info
                            {
                                Container.Info containerInfo = new Container.Info();
                                if (treeNode.Parent != null)
                                {
                                    if (Tree.Node.GetNodeType(treeNode.Parent) == Tree.Node.Type.Container)
                                    {
                                        containerInfo.Parent = treeNode.Parent.Tag;
                                    }
                                }
                                _previousContainer = containerInfo; //NEW
                                containerInfo.TreeNode = treeNode;

                                containerInfo.Name = xmlNode.Attributes["Name"].Value;

                                if (confVersion >= 0.8)
                                {
                                    if (xmlNode.Attributes["Expanded"].Value == "True")
                                    {
                                        containerInfo.IsExpanded = true;
                                    }
                                    else
                                    {
                                        containerInfo.IsExpanded = false;
                                    }
                                }

                                Connection.Info connectionInfo;
                                if (confVersion >= 0.9)
                                {
                                    connectionInfo = GetConnectionInfoFromXml(xmlNode);
                                }
                                else
                                {
                                    connectionInfo = new Connection.Info();
                                }

                                connectionInfo.Parent = containerInfo;
                                connectionInfo.IsContainer = true;
                                containerInfo.ConnectionInfo = connectionInfo;

                                ContainerList.Add(containerInfo);

                                treeNode.Tag = containerInfo;
                                treeNode.ImageIndex = System.Convert.ToInt32(Images.Enums.TreeImage.Container);
                                treeNode.SelectedImageIndex =
                                    System.Convert.ToInt32(Images.Enums.TreeImage.Container);
                            }

                            AddNodeFromXml(xmlNode, treeNode);
                        }
                    }
                    else
                    {
                        string nodeName = "";
                        XmlAttribute nameAttribute = parentXmlNode.Attributes["Name"];
                        if (nameAttribute != null)
                        {
                            nodeName = nameAttribute.Value.Trim();
                        }
                        if (!string.IsNullOrEmpty(nodeName))
                        {
                            parentTreeNode.Text = nodeName;
                        }
                        else
                        {
                            parentTreeNode.Text = parentXmlNode.Name;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strAddNodeFromXmlFailed + Constants.vbNewLine +
                                                        ex.Message + ex.StackTrace, true);
                    throw;
                }
            }

            private Info GetConnectionInfoFromXml(XmlNode xxNode)
            {
                Connection.Info conI = new Connection.Info();

                try
                {
                    XmlNode with_1 = xxNode;
                    if (this.confVersion > 0.1) //0.2
                    {
                        conI.Name = with_1.Attributes["Name"].Value;
                        conI.Description = with_1.Attributes["Descr"].Value;
                        conI.Hostname = with_1.Attributes["Hostname"].Value;
                        conI.Username = with_1.Attributes["Username"].Value;
                        conI.Password = Security.Crypt.Decrypt((string)(with_1.Attributes["Password"].Value), pW);
                        conI.Domain = with_1.Attributes["Domain"].Value;
                        conI.DisplayWallpaper = Convert.ToBoolean(with_1.Attributes["DisplayWallpaper"].Value);
                        conI.DisplayThemes = Convert.ToBoolean(with_1.Attributes["DisplayThemes"].Value);
                        conI.CacheBitmaps = Convert.ToBoolean(with_1.Attributes["CacheBitmaps"].Value);

                        if (this.confVersion < 1.1) //1.0 - 0.1
                        {
                            if ((Convert.ToBoolean(with_1.Attributes["Fullscreen"].Value)))
                            {
                                conI.Resolution = mRemoteNC.RDP.RDPResolutions.Fullscreen;
                            }
                            else
                            {
                                conI.Resolution = mRemoteNC.RDP.RDPResolutions.FitToWindow;
                            }
                        }
                    }

                    if (this.confVersion > 0.2) //0.3
                    {
                        if (this.confVersion < 0.7)
                        {
                            if (System.Convert.ToBoolean(with_1.Attributes["UseVNC"].Value) == true)
                            {
                                conI.Protocol = mRemoteNC.Protocols.VNC;
                                conI.Port = Convert.ToInt32(with_1.Attributes["VNCPort"].Value);
                            }
                            else
                            {
                                conI.Protocol = mRemoteNC.Protocols.RDP;
                            }
                        }
                    }
                    else
                    {
                        conI.Port = Convert.ToInt32(mRemoteNC.RDP.Defaults.Port);
                        conI.Protocol = mRemoteNC.Protocols.RDP;
                    }

                    if (this.confVersion > 0.3) //0.4
                    {
                        if (this.confVersion < 0.7)
                        {
                            if (System.Convert.ToBoolean(with_1.Attributes["UseVNC"].Value) == true)
                            {
                                conI.Port = Convert.ToInt32(with_1.Attributes["VNCPort"].Value);
                            }
                            else
                            {
                                conI.Port = Convert.ToInt32(with_1.Attributes["RDPPort"].Value);
                            }
                        }

                        conI.UseConsoleSession = Convert.ToBoolean(with_1.Attributes["ConnectToConsole"].Value);
                    }
                    else
                    {
                        if (this.confVersion < 0.7)
                        {
                            if (System.Convert.ToBoolean(with_1.Attributes["UseVNC"].Value) == true)
                            {
                                conI.Port = Convert.ToInt32(mRemoteNC.VNC.Defaults.Port);
                            }
                            else
                            {
                                conI.Port = Convert.ToInt32(mRemoteNC.RDP.Defaults.Port);
                            }
                        }
                        conI.UseConsoleSession = false;
                    }

                    if (this.confVersion > 0.4) //0.5 and 0.6
                    {
                        conI.RedirectDiskDrives = Convert.ToBoolean(with_1.Attributes["RedirectDiskDrives"].Value);
                        conI.RedirectPrinters = Convert.ToBoolean(with_1.Attributes["RedirectPrinters"].Value);
                        conI.RedirectPorts = Convert.ToBoolean(with_1.Attributes["RedirectPorts"].Value);
                        conI.RedirectSmartCards = Convert.ToBoolean(with_1.Attributes["RedirectSmartCards"].Value);
                    }
                    else
                    {
                        conI.RedirectDiskDrives = false;
                        conI.RedirectPrinters = false;
                        conI.RedirectPorts = false;
                        conI.RedirectSmartCards = false;
                    }

                    if (this.confVersion > 0.6) //0.7
                    {
                        conI.Protocol = (Protocols)Tools.Misc.StringToEnum(typeof(mRemoteNC.Protocols),
                                                                            with_1.Attributes["Protocol"].Value);
                        conI.Port = Convert.ToInt32(with_1.Attributes["Port"].Value);
                    }

                    if (this.confVersion > 0.9) //1.0
                    {
                        conI.RedirectKeys = Convert.ToBoolean(with_1.Attributes["RedirectKeys"].Value);
                    }

                    if (this.confVersion > 1.1) //1.2
                    {
                        conI.PuttySession = with_1.Attributes["PuttySession"].Value.ToString();
                    }

                    if (this.confVersion > 1.2) //1.3
                    {
                        conI.Colors =
                            (RDP.RDPColors)Tools.Misc.StringToEnum(typeof(mRemoteNC.RDP.RDPColors),
                                                                    with_1.Attributes["Colors"].Value);
                        conI.Resolution =
                            (RDP.RDPResolutions)
                            Tools.Misc.StringToEnum(typeof(mRemoteNC.RDP.RDPResolutions),
                                                    with_1.Attributes["Resolution"].Value);
                        conI.RedirectSound =
                            (RDP.RDPSounds)Tools.Misc.StringToEnum(typeof(mRemoteNC.RDP.RDPSounds),
                                                                    with_1.Attributes["RedirectSound"].Value);
                    }
                    else
                    {
                        switch (Convert.ToInt32(with_1.Attributes["Colors"].Value))
                        {
                            case 0:
                                conI.Colors = mRemoteNC.RDP.RDPColors.Colors256;
                                break;
                            case 1:
                                conI.Colors = mRemoteNC.RDP.RDPColors.Colors16Bit;
                                break;
                            case 2:
                                conI.Colors = mRemoteNC.RDP.RDPColors.Colors24Bit;
                                break;
                            case 3:
                                conI.Colors = mRemoteNC.RDP.RDPColors.Colors32Bit;
                                break;
                            case 4:
                                conI.Colors = mRemoteNC.RDP.RDPColors.Colors15Bit;
                                break;
                        }

                        conI.RedirectSound =
                            (RDP.RDPSounds)
                            Tools.Misc.StringToEnum(typeof(RDP.RDPSounds), with_1.Attributes["RedirectSound"].Value);
                    }

                    if (this.confVersion > 1.2) //1.3
                    {
                        conI.Inherit = new Connection.Info.Inheritance(conI);
                        conI.Inherit.CacheBitmaps = Convert.ToBoolean(with_1.Attributes["InheritCacheBitmaps"].Value);
                        conI.Inherit.Colors = Convert.ToBoolean(with_1.Attributes["InheritColors"].Value);
                        conI.Inherit.Description = Convert.ToBoolean(with_1.Attributes["InheritDescription"].Value);
                        conI.Inherit.DisplayThemes = Convert.ToBoolean(with_1.Attributes["InheritDisplayThemes"].Value);
                        conI.Inherit.DisplayWallpaper =
                            Convert.ToBoolean(with_1.Attributes["InheritDisplayWallpaper"].Value);
                        conI.Inherit.Domain = Convert.ToBoolean(with_1.Attributes["InheritDomain"].Value);
                        conI.Inherit.Icon = Convert.ToBoolean(with_1.Attributes["InheritIcon"].Value);
                        conI.Inherit.Panel = Convert.ToBoolean(with_1.Attributes["InheritPanel"].Value);
                        conI.Inherit.Password = Convert.ToBoolean(with_1.Attributes["InheritPassword"].Value);
                        conI.Inherit.Port = Convert.ToBoolean(with_1.Attributes["InheritPort"].Value);
                        conI.Inherit.Protocol = Convert.ToBoolean(with_1.Attributes["InheritProtocol"].Value);
                        conI.Inherit.PuttySession = Convert.ToBoolean(with_1.Attributes["InheritPuttySession"].Value);
                        conI.Inherit.RedirectDiskDrives =
                            Convert.ToBoolean(with_1.Attributes["InheritRedirectDiskDrives"].Value);
                        conI.Inherit.RedirectKeys = Convert.ToBoolean(with_1.Attributes["InheritRedirectKeys"].Value);
                        conI.Inherit.RedirectPorts = Convert.ToBoolean(with_1.Attributes["InheritRedirectPorts"].Value);
                        conI.Inherit.RedirectPrinters =
                            Convert.ToBoolean(with_1.Attributes["InheritRedirectPrinters"].Value);
                        conI.Inherit.RedirectSmartCards =
                            Convert.ToBoolean(with_1.Attributes["InheritRedirectSmartCards"].Value);
                        conI.Inherit.RedirectSound = Convert.ToBoolean(with_1.Attributes["InheritRedirectSound"].Value);
                        conI.Inherit.Resolution = Convert.ToBoolean(with_1.Attributes["InheritResolution"].Value);
                        conI.Inherit.UseConsoleSession =
                            Convert.ToBoolean(with_1.Attributes["InheritUseConsoleSession"].Value);
                        conI.Inherit.Username = Convert.ToBoolean(with_1.Attributes["InheritUsername"].Value);

                        conI.Icon = with_1.Attributes["Icon"].Value;
                        conI.Panel = with_1.Attributes["Panel"].Value;
                    }
                    else
                    {
                        conI.Inherit = new Connection.Info.Inheritance(conI,
                                                                       Convert.ToBoolean(
                                                                           with_1.Attributes["Inherit"].Value));

                        conI.Icon = with_1.Attributes["Icon"].Value.Replace(".ico", "");
                        conI.Panel = Language.strGeneral;
                    }

                    if (this.confVersion > 1.4) //1.5
                    {
                        conI.PleaseConnect = Convert.ToBoolean(with_1.Attributes["Connected"].Value);
                    }

                    if (this.confVersion > 1.5) //1.6
                    {
                        conI.ICAEncryption = (ICA.EncryptionStrength)
                                             Tools.Misc.StringToEnum(typeof(ICA.EncryptionStrength),
                                                                     with_1.Attributes["ICAEncryptionStrength"].Value);
                        conI.Inherit.ICAEncryption =
                            Convert.ToBoolean(with_1.Attributes["InheritICAEncryptionStrength"].Value);

                        conI.PreExtApp = with_1.Attributes["PreExtApp"].Value;
                        conI.PostExtApp = with_1.Attributes["PostExtApp"].Value;
                        conI.Inherit.PreExtApp = Convert.ToBoolean(with_1.Attributes["InheritPreExtApp"].Value);
                        conI.Inherit.PostExtApp = Convert.ToBoolean(with_1.Attributes["InheritPostExtApp"].Value);
                    }

                    if (this.confVersion > 1.6) //1.7
                    {
                        conI.VNCCompression = (VNC.Compression)
                                              Tools.Misc.StringToEnum(typeof(VNC.Compression),
                                                                      with_1.Attributes["VNCCompression"].Value);
                        conI.VNCEncoding = (VNC.Encoding)Tools.Misc.StringToEnum(typeof(VNC.Encoding),
                                                                                  with_1.Attributes["VNCEncoding"].Value);
                        conI.VNCAuthMode = (VNC.AuthMode)Tools.Misc.StringToEnum(typeof(VNC.AuthMode),
                                                                                  with_1.Attributes["VNCAuthMode"].Value);
                        conI.VNCProxyType = (VNC.ProxyType)Tools.Misc.StringToEnum(
                            typeof(VNC.ProxyType),
                            with_1.Attributes["VNCProxyType"].Value);
                        conI.VNCProxyIP = with_1.Attributes["VNCProxyIP"].Value;
                        conI.VNCProxyPort = Convert.ToInt32(with_1.Attributes["VNCProxyPort"].Value);
                        conI.VNCProxyUsername = with_1.Attributes["VNCProxyUsername"].Value;
                        conI.VNCProxyPassword = Security.Crypt.Decrypt(with_1.Attributes["VNCProxyPassword"].Value, pW);
                        conI.VNCColors = (VNC.Colors)Tools.Misc.StringToEnum(typeof(VNC.Colors),
                                                                              with_1.Attributes["VNCColors"].Value);
                        conI.VNCSmartSizeMode = (VNC.SmartSizeMode)
                                                Tools.Misc.StringToEnum(typeof(VNC.SmartSizeMode),
                                                                        with_1.Attributes["VNCSmartSizeMode"].Value);
                        conI.VNCViewOnly = Convert.ToBoolean(with_1.Attributes["VNCViewOnly"].Value);

                        conI.Inherit.VNCCompression = Convert.ToBoolean(with_1.Attributes["InheritVNCCompression"].Value);
                        conI.Inherit.VNCEncoding = Convert.ToBoolean(with_1.Attributes["InheritVNCEncoding"].Value);
                        conI.Inherit.VNCAuthMode = Convert.ToBoolean(with_1.Attributes["InheritVNCAuthMode"].Value);
                        conI.Inherit.VNCProxyType = Convert.ToBoolean(with_1.Attributes["InheritVNCProxyType"].Value);
                        conI.Inherit.VNCProxyIP = Convert.ToBoolean(with_1.Attributes["InheritVNCProxyIP"].Value);
                        conI.Inherit.VNCProxyPort = Convert.ToBoolean(with_1.Attributes["InheritVNCProxyPort"].Value);
                        conI.Inherit.VNCProxyUsername =
                            Convert.ToBoolean(with_1.Attributes["InheritVNCProxyUsername"].Value);
                        conI.Inherit.VNCProxyPassword =
                            Convert.ToBoolean(with_1.Attributes["InheritVNCProxyPassword"].Value);
                        conI.Inherit.VNCColors = Convert.ToBoolean(with_1.Attributes["InheritVNCColors"].Value);
                        conI.Inherit.VNCSmartSizeMode =
                            Convert.ToBoolean(with_1.Attributes["InheritVNCSmartSizeMode"].Value);
                        conI.Inherit.VNCViewOnly = Convert.ToBoolean(with_1.Attributes["InheritVNCViewOnly"].Value);
                    }

                    if (this.confVersion > 1.7) //1.8
                    {
                        conI.RDPAuthenticationLevel = (RDP.AuthenticationLevel)
                                                      Tools.Misc.StringToEnum(typeof(RDP.AuthenticationLevel),
                                                                              with_1.Attributes["RDPAuthenticationLevel"
                                                                                  ].Value);

                        conI.Inherit.RDPAuthenticationLevel =
                            Convert.ToBoolean(with_1.Attributes["InheritRDPAuthenticationLevel"].Value);
                    }

                    if (this.confVersion > 1.8) //1.9
                    {
                        conI.RenderingEngine = (HTTPBase.RenderingEngine)
                                               Tools.Misc.StringToEnum(typeof(HTTPBase.RenderingEngine),
                                                                       with_1.Attributes["RenderingEngine"].Value);
                        conI.MacAddress = with_1.Attributes["MacAddress"].Value;

                        conI.Inherit.RenderingEngine =
                            Convert.ToBoolean(with_1.Attributes["InheritRenderingEngine"].Value);
                        conI.Inherit.MacAddress = Convert.ToBoolean(with_1.Attributes["InheritMacAddress"].Value);
                    }

                    if (this.confVersion > 1.9) //2.0
                    {
                        conI.UserField = with_1.Attributes["UserField"].Value;
                        conI.Inherit.UserField = Convert.ToBoolean(with_1.Attributes["InheritUserField"].Value);
                    }

                    if (this.confVersion > 2.0) //2.1
                    {
                        conI.ExtApp = with_1.Attributes["ExtApp"].Value;
                        conI.Inherit.ExtApp = Convert.ToBoolean(with_1.Attributes["InheritExtApp"].Value);
                    }

                    if (this.confVersion > 2.1) //2.2
                    {
                        // Get settings
                        conI.RDGatewayUsageMethod = (RDP.RDGatewayUsageMethod)
                                                    Tools.Misc.StringToEnum(typeof(RDP.RDGatewayUsageMethod),
                                                                            with_1.Attributes["RDGatewayUsageMethod"].
                                                                                Value);
                        conI.RDGatewayHostname = with_1.Attributes["RDGatewayHostname"].Value;
                        conI.RDGatewayUseConnectionCredentials = (RDP.RDGatewayUseConnectionCredentials)
                                                                 Tools.Misc.StringToEnum(
                                                                     typeof(RDP.RDGatewayUseConnectionCredentials),
                                                                     with_1.Attributes[
                                                                         "RDGatewayUseConnectionCredentials"].Value);
                        conI.RDGatewayUsername = with_1.Attributes["RDGatewayUsername"].Value;
                        conI.RDGatewayPassword = Security.Crypt.Decrypt(with_1.Attributes["RDGatewayPassword"].Value, pW);
                        conI.RDGatewayDomain = with_1.Attributes["RDGatewayDomain"].Value;

                        // Get inheritance settings
                        conI.Inherit.RDGatewayUsageMethod =
                            Convert.ToBoolean(with_1.Attributes["InheritRDGatewayUsageMethod"].Value);
                        conI.Inherit.RDGatewayHostname =
                            Convert.ToBoolean(with_1.Attributes["InheritRDGatewayHostname"].Value);
                        conI.Inherit.RDGatewayUseConnectionCredentials =
                            Convert.ToBoolean(with_1.Attributes["InheritRDGatewayUseConnectionCredentials"].Value);
                        conI.Inherit.RDGatewayUsername =
                            Convert.ToBoolean(with_1.Attributes["InheritRDGatewayUsername"].Value);
                        conI.Inherit.RDGatewayPassword =
                            Convert.ToBoolean(with_1.Attributes["InheritRDGatewayPassword"].Value);
                        conI.Inherit.RDGatewayDomain =
                            Convert.ToBoolean(with_1.Attributes["InheritRDGatewayDomain"].Value);
                    }

                    if (this.confVersion > 2.2) //2.3
                    {
                        // Get settings
                        conI.EnableFontSmoothing = Convert.ToBoolean(with_1.Attributes["EnableFontSmoothing"].Value);
                        conI.EnableDesktopComposition =
                            Convert.ToBoolean(with_1.Attributes["EnableDesktopComposition"].Value);

                        // Get inheritance settings
                        conI.Inherit.EnableFontSmoothing =
                            Convert.ToBoolean(with_1.Attributes["InheritEnableFontSmoothing"].Value);
                        conI.Inherit.EnableDesktopComposition = Convert.ToBoolean(
                            with_1.Attributes["InheritEnableDesktopComposition"].Value);
                    }

                    if (confVersion >= 2.4)
                    {
                        conI.UseCredSsp = Convert.ToBoolean(with_1.Attributes["UseCredSsp"].Value);
                        conI.Inherit.UseCredSsp = Convert.ToBoolean(with_1.Attributes["InheritUseCredSsp"].Value);
                    }

                    if (confVersion >= 2.5)
                    {
                        conI.ConnectOnStartup = Convert.ToBoolean(with_1.Attributes["ConnectOnStartup"].Value);
                    }

                    //Placeholder: ConnectionOption
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        string.Format(
                                                            Language.strGetConnectionInfoFromXmlFailed, conI.Name,
                                                            this.ConnectionFileName, ex.Message), false);
                }
                return conI;
            }

            private bool Authenticate(string Value, bool CompareToOriginalValue, Root.Info RootInfo = null)
            {
                if (CompareToOriginalValue)
                {
                    while (!(Security.Crypt.Decrypt(Value, pW) != Value))
                    {
                        pW = (string)(Tools.Misc.PasswordDialog(false));

                        if (pW == "")
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    while (!(Security.Crypt.Decrypt(Value, pW) == "ThisIsProtected"))
                    {
                        pW = (string)(Tools.Misc.PasswordDialog(false));

                        if (pW == "")
                        {
                            return false;
                        }
                    }

                    RootInfo.Password = true;
                    RootInfo.PasswordString = pW;
                }

                return true;
            }

            #endregion XML
        }
    }
}