using System;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using mRemoteNC.Connection;
using My;

//using mRemoteNC.Runtime;
//using mRemoteNC.Tools.Misc;

namespace mRemoteNC.Config
{
    namespace Connections
    {
        public class Save
        {
            #region Public Enums

            public enum Format
            {
                None,
                mRXML,
                mRCSV,
                vRDvRE,
                vRDCSV,
                SQL
            }

            #endregion Public Enums

            #region Private Properties

            private XmlTextWriter _xmlTextWriter;
            private string _password = (string)mRemoteNC.App.Info.General.EncryptionKey;

            private SqlConnection _sqlConnection;
            private SqlCommand _sqlQuery;

            private int _currentNodeIndex = 0;
            private string _parentConstantId = "0";

            #endregion Private Properties

            #region Public Properties

            public string SQLHost { get; set; }

            public string SQLDatabaseName { get; set; }

            public string SQLUsername { get; set; }

            public string SQLPassword { get; set; }

            public string ConnectionFileName { get; set; }

            public TreeNode RootTreeNode { get; set; }

            public bool Export { get; set; }

            public Format SaveFormat { get; set; }

            public Security.Save SaveSecurity { get; set; }

            public Connection.List ConnectionList { get; set; }

            public Container.List ContainerList { get; set; }

            #endregion Public Properties

            #region Public Methods

            public void SaveConnections()
            {
                switch (SaveFormat)
                {
                    case Format.SQL:
                        SaveToSQL();
                        Runtime.SetMainFormText("SQL Server");
                        break;
                    case Format.mRCSV:
                        SaveTomRCSV();
                        break;
                    case Format.vRDvRE:
                        SaveToVRE();
                        break;
                    case Format.vRDCSV:
                        SaveTovRDCSV();
                        break;
                    default:
                        SaveToXml();
                        if (Settings.Default.EncryptCompleteConnectionsFile)
                        {
                            EncryptCompleteFile();
                        }
                        if (!Export)
                        {
                            Runtime.SetMainFormText(ConnectionFileName);
                        }
                        break;
                }
            }

            #endregion Public Methods

            #region SQL

            private bool VerifyDatabaseVersion(SqlConnection sqlConnection)
            {
                bool isVerified = false;
                SqlDataReader sqlDataReader = null;
                System.Version databaseVersion = null;
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tblRoot", sqlConnection);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (!sqlDataReader.HasRows)
                    {
                        return true; // assume new empty database
                    }
                    sqlDataReader.Read();

                    databaseVersion =
                        new System.Version(
                            System.Convert.ToString(Convert.ToDouble(sqlDataReader["confVersion"],
                                                                     CultureInfo.InvariantCulture)));

                    sqlDataReader.Close();

                    if (databaseVersion.CompareTo(new System.Version(2, 2)) == 0) // 2.2
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.InformationMsg,
                                                            string.Format(
                                                                "Upgrading database from version {0} to version {1}.",
                                                                databaseVersion.ToString(), "2.3"));
                        sqlCommand =
                            new SqlCommand(
                                "ALTER TABLE tblCons ADD EnableFontSmoothing bit NOT NULL DEFAULT 0, EnableDesktopComposition bit NOT NULL DEFAULT 0, InheritEnableFontSmoothing bit NOT NULL DEFAULT 0, InheritEnableDesktopComposition bit NOT NULL DEFAULT 0;",
                                sqlConnection);
                        sqlCommand.ExecuteNonQuery();
                        databaseVersion = new System.Version(2, 3);
                    }

                    if (databaseVersion.CompareTo(new System.Version(2, 3)) == 0) // 2.3
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.InformationMsg,
                                                            string.Format(
                                                                "Upgrading database from version {0} to version {1}.",
                                                                databaseVersion.ToString(), "2.4"));
                        sqlCommand =
                            new SqlCommand(
                                "ALTER TABLE tblCons ADD UseCredSsp bit NOT NULL DEFAULT 1, InheritUseCredSsp bit NOT NULL DEFAULT 0;",
                                sqlConnection);
                        sqlCommand.ExecuteNonQuery();
                        databaseVersion = new Version(2, 4);
                    }

                    if (databaseVersion.CompareTo(new System.Version(2, 4)) == 0) // 2.4
                    {
                        isVerified = true;
                    }

                    if (isVerified == false)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                            string.Format(Language.strErrorBadDatabaseVersion,
                                                                          databaseVersion.ToString(),
                                                                          (new Microsoft.VisualBasic.ApplicationServices
                                                                              .WindowsFormsApplicationBase()).Info.
                                                                              ProductName));
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        string.Format(Language.strErrorVerifyDatabaseVersionFailed,
                                                                      ex.Message));
                }
                finally
                {
                    if (sqlDataReader != null)
                    {
                        if (!sqlDataReader.IsClosed)
                        {
                            sqlDataReader.Close();
                        }
                    }
                }
                return isVerified;
            }

            private void SaveToSQL()
            {
                if (SQLUsername != "")
                {
                    _sqlConnection =
                        new SqlConnection(
                            (string)
                            ("Data Source=" + SQLHost + ";Initial Catalog=" + SQLDatabaseName + ";User Id=" +
                             SQLUsername + ";Password=" + SQLPassword));
                }
                else
                {
                    _sqlConnection =
                        new SqlConnection("Data Source=" + SQLHost + ";Initial Catalog=" + SQLDatabaseName +
                                          ";Integrated Security=True");
                }

                _sqlConnection.Open();

                if (!VerifyDatabaseVersion(_sqlConnection))
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strErrorConnectionListSaveFailed);
                    return;
                }

                TreeNode tN;
                tN = (TreeNode)RootTreeNode.Clone();

                string strProtected;
                if (tN.Tag != null)
                {
                    if ((tN.Tag as Root.Info).Password == true)
                    {
                        _password = (tN.Tag as Root.Info).PasswordString;
                        strProtected = Security.Crypt.Encrypt("ThisIsProtected", _password);
                    }
                    else
                    {
                        strProtected = Security.Crypt.Encrypt("ThisIsNotProtected", _password);
                    }
                }
                else
                {
                    strProtected = Security.Crypt.Encrypt("ThisIsNotProtected", _password);
                }

                _sqlQuery = new SqlCommand("DELETE FROM tblRoot", _sqlConnection);
                _sqlQuery.ExecuteNonQuery();

                _sqlQuery =
                    new SqlCommand(
                        "INSERT INTO tblRoot (Name, Export, Protected, ConfVersion) VALUES(\'" +
                        Tools.Misc.PrepareValueForDB(tN.Text) + "\', 0, \'" + strProtected + "\'," +
                        mRemoteNC.App.Info.Connections.ConnectionFileVersion.ToString(CultureInfo.InvariantCulture) +
                        ")", _sqlConnection);
                _sqlQuery.ExecuteNonQuery();

                _sqlQuery = new SqlCommand("DELETE FROM tblCons", _sqlConnection);
                _sqlQuery.ExecuteNonQuery();

                TreeNodeCollection tNC;
                tNC = tN.Nodes;

                SaveNodesSQL(tNC);

                _sqlQuery = new SqlCommand("DELETE FROM tblUpdate", _sqlConnection);
                _sqlQuery.ExecuteNonQuery();
                _sqlQuery =
                    new SqlCommand(
                        "INSERT INTO tblUpdate (LastUpdate) VALUES(\'" + Tools.Misc.DBDate(DateTime.Now) + "\')",
                        _sqlConnection);
                _sqlQuery.ExecuteNonQuery();

                _sqlConnection.Close();
            }

            private void SaveNodesSQL(TreeNodeCollection tnc)
            {
                foreach (TreeNode node in tnc)
                {
                    _currentNodeIndex++;

                    Connection.Info curConI;
                    _sqlQuery =
                        new SqlCommand(
                            "INSERT INTO tblCons (Name, Type, Expanded, Description, Icon, Panel, Username, " +
                            "DomainName, Password, Hostname, Protocol, PuttySession, " +
                            "Port, ConnectToConsole, RenderingEngine, ICAEncryptionStrength, RDPAuthenticationLevel, Colors, Resolution, DisplayWallpaper, " +
                            "DisplayThemes, EnableFontSmoothing, EnableDesktopComposition, CacheBitmaps, RedirectDiskDrives, RedirectPorts, " +
                            "RedirectPrinters, RedirectSmartCards, RedirectSound, RedirectKeys, " +
                            "Connected, PreExtApp, PostExtApp, MacAddress, UserField, ExtApp, VNCCompression, VNCEncoding, VNCAuthMode, " +
                            "VNCProxyType, VNCProxyIP, VNCProxyPort, VNCProxyUsername, VNCProxyPassword, " +
                            "VNCColors, VNCSmartSizeMode, VNCViewOnly, " +
                            "RDGatewayUsageMethod, RDGatewayHostname, RDGatewayUseConnectionCredentials, RDGatewayUsername, RDGatewayPassword, RDGatewayDomain, " +
                            "UseCredSsp, " + "InheritCacheBitmaps, InheritColors, " +
                            "InheritDescription, InheritDisplayThemes, InheritDisplayWallpaper, InheritEnableFontSmoothing, InheritEnableDesktopComposition, InheritDomain, " +
                            "InheritIcon, InheritPanel, InheritPassword, InheritPort, " +
                            "InheritProtocol, InheritPuttySession, InheritRedirectDiskDrives, " +
                            "InheritRedirectKeys, InheritRedirectPorts, InheritRedirectPrinters, " +
                            "InheritRedirectSmartCards, InheritRedirectSound, InheritResolution, " +
                            "InheritUseConsoleSession, InheritRenderingEngine, InheritUsername, InheritICAEncryptionStrength, InheritRDPAuthenticationLevel, " +
                            "InheritPreExtApp, InheritPostExtApp, InheritMacAddress, InheritUserField, InheritExtApp, InheritVNCCompression, InheritVNCEncoding, " +
                            "InheritVNCAuthMode, InheritVNCProxyType, InheritVNCProxyIP, InheritVNCProxyPort, " +
                            "InheritVNCProxyUsername, InheritVNCProxyPassword, InheritVNCColors, " +
                            "InheritVNCSmartSizeMode, InheritVNCViewOnly, " +
                            "InheritRDGatewayUsageMethod, InheritRDGatewayHostname, InheritRDGatewayUseConnectionCredentials, InheritRDGatewayUsername, InheritRDGatewayPassword, InheritRDGatewayDomain, " +
                            "InheritUseCredSsp, "
                            + "PositionID, _parentConstantId, ConstantID, LastChange)" + "VALUES (", _sqlConnection
                            );

                    if (Tree.Node.GetNodeType(node) == Tree.Node.Type.Connection ||
                        Tree.Node.GetNodeType(node) == Tree.Node.Type.Container)
                    {
                        //_xmlTextWriter.WriteStartElement("Node")
                        _sqlQuery.CommandText += "\'" + Tools.Misc.PrepareValueForDB(node.Text) + "\',"; //Name
                        _sqlQuery.CommandText += "\'" + Tree.Node.GetNodeType(node).ToString() + "\',"; //Type
                    }

                    if (Tree.Node.GetNodeType(node) == Tree.Node.Type.Container) //container
                    {
                        _sqlQuery.CommandText += "\'" + this.ContainerList[node.Tag].IsExpanded + "\',"; //Expanded
                        curConI = this.ContainerList[node.Tag].ConnectionInfo;
                        SaveConnectionFieldsSQL(curConI);

                        _sqlQuery.CommandText = (string)(Tools.Misc.PrepareForDB(_sqlQuery.CommandText));
                        _sqlQuery.ExecuteNonQuery();
                        //_parentConstantId = _currentNodeIndex
                        SaveNodesSQL(node.Nodes);
                        //_xmlTextWriter.WriteEndElement()
                    }

                    if (Tree.Node.GetNodeType(node) == Tree.Node.Type.Connection)
                    {
                        _sqlQuery.CommandText += "\'" + false + "\',";
                        curConI = (Connection.Info)this.ConnectionList[node.Tag];
                        SaveConnectionFieldsSQL(curConI);
                        //_xmlTextWriter.WriteEndElement()
                        _sqlQuery.CommandText = (string)(Tools.Misc.PrepareForDB(_sqlQuery.CommandText));
                        _sqlQuery.ExecuteNonQuery();
                    }

                    //_parentConstantId = 0
                }
            }

            private void SaveConnectionFieldsSQL(Connection.Info curConI)
            {
                Connection.Info with_1 = curConI;
                _sqlQuery.CommandText += "\'" + Tools.Misc.PrepareValueForDB((string)with_1.Description) + "\',";
                _sqlQuery.CommandText += "\'" + Tools.Misc.PrepareValueForDB((string)with_1.Icon) + "\',";
                _sqlQuery.CommandText += "\'" + Tools.Misc.PrepareValueForDB((string)with_1.Panel) + "\',";

                if (this.SaveSecurity.Username == true)
                {
                    _sqlQuery.CommandText += "\'" + Tools.Misc.PrepareValueForDB((string)with_1.Username) + "\',";
                }
                else
                {
                    _sqlQuery.CommandText += "\'" + "" + "\',";
                }

                if (this.SaveSecurity.Domain == true)
                {
                    _sqlQuery.CommandText += "\'" + Tools.Misc.PrepareValueForDB((string)with_1.Domain) + "\',";
                }
                else
                {
                    _sqlQuery.CommandText += "\'" + "" + "\',";
                }

                if (this.SaveSecurity.Password == true)
                {
                    _sqlQuery.CommandText += "\'" +
                                             Tools.Misc.PrepareValueForDB(
                                                 Security.Crypt.Encrypt((string)with_1.Password, _password)) + "\',";
                }
                else
                {
                    _sqlQuery.CommandText += "\'" + "" + "\',";
                }

                _sqlQuery.CommandText += "\'" + Tools.Misc.PrepareValueForDB((string)with_1.Hostname) + "\',";
                _sqlQuery.CommandText += "\'" + with_1.Protocol.ToString() + "\',";
                _sqlQuery.CommandText += "\'" + Tools.Misc.PrepareValueForDB((string)with_1.PuttySession) + "\',";
                _sqlQuery.CommandText += "\'" + with_1.Port + "\',";
                _sqlQuery.CommandText += "\'" + with_1.UseConsoleSession + "\',";
                _sqlQuery.CommandText += "\'" + with_1.RenderingEngine.ToString() + "\',";
                _sqlQuery.CommandText += "\'" + with_1.ICAEncryption.ToString() + "\',";
                _sqlQuery.CommandText += "\'" + with_1.RDPAuthenticationLevel.ToString() + "\',";
                _sqlQuery.CommandText += "\'" + with_1.Colors.ToString() + "\',";
                _sqlQuery.CommandText += "\'" + with_1.Resolution.ToString() + "\',";
                _sqlQuery.CommandText += "\'" + with_1.DisplayWallpaper + "\',";
                _sqlQuery.CommandText += "\'" + with_1.DisplayThemes + "\',";
                _sqlQuery.CommandText += "\'" + with_1.EnableFontSmoothing + "\',";
                _sqlQuery.CommandText += "\'" + with_1.EnableDesktopComposition + "\',";
                _sqlQuery.CommandText += "\'" + with_1.CacheBitmaps + "\',";
                _sqlQuery.CommandText += "\'" + with_1.RedirectDiskDrives + "\',";
                _sqlQuery.CommandText += "\'" + with_1.RedirectPorts + "\',";
                _sqlQuery.CommandText += "\'" + with_1.RedirectPrinters + "\',";
                _sqlQuery.CommandText += "\'" + with_1.RedirectSmartCards + "\',";
                _sqlQuery.CommandText += "\'" + with_1.RedirectSound.ToString() + "\',";
                _sqlQuery.CommandText += "\'" + with_1.RedirectKeys + "\',";

                if (curConI.OpenConnections.Count > 0)
                {
                    _sqlQuery.CommandText += 1 + ",";
                }
                else
                {
                    _sqlQuery.CommandText += 0 + ",";
                }

                _sqlQuery.CommandText += "\'" + with_1.PreExtApp + "\',";
                _sqlQuery.CommandText += "\'" + with_1.PostExtApp + "\',";
                _sqlQuery.CommandText += "\'" + with_1.MacAddress + "\',";
                _sqlQuery.CommandText += "\'" + with_1.UserField + "\',";
                _sqlQuery.CommandText += "\'" + with_1.ExtApp + "\',";

                _sqlQuery.CommandText += "\'" + with_1.VNCCompression.ToString() + "\',";
                _sqlQuery.CommandText += "\'" + with_1.VNCEncoding.ToString() + "\',";
                _sqlQuery.CommandText += "\'" + with_1.VNCAuthMode.ToString() + "\',";
                _sqlQuery.CommandText += "\'" + with_1.VNCProxyType.ToString() + "\',";
                _sqlQuery.CommandText += "\'" + with_1.VNCProxyIP + "\',";
                _sqlQuery.CommandText += "\'" + with_1.VNCProxyPort + "\',";
                _sqlQuery.CommandText += "\'" + with_1.VNCProxyUsername + "\',";
                _sqlQuery.CommandText += "\'" + Security.Crypt.Encrypt((string)with_1.VNCProxyPassword, _password) +
                                         "\',";
                _sqlQuery.CommandText += "\'" + with_1.VNCColors.ToString() + "\',";
                _sqlQuery.CommandText += "\'" + with_1.VNCSmartSizeMode.ToString() + "\',";
                _sqlQuery.CommandText += "\'" + with_1.VNCViewOnly + "\',";

                _sqlQuery.CommandText += "\'" + with_1.RDGatewayUsageMethod.ToString() + "\',";
                _sqlQuery.CommandText += "\'" + with_1.RDGatewayHostname + "\',";
                _sqlQuery.CommandText += "\'" + with_1.RDGatewayUseConnectionCredentials.ToString() + "\',";

                if (this.SaveSecurity.Username == true)
                {
                    _sqlQuery.CommandText += "\'" + with_1.RDGatewayUsername + "\',";
                }
                else
                {
                    _sqlQuery.CommandText += "\'" + "" + "\',";
                }

                if (this.SaveSecurity.Password == true)
                {
                    _sqlQuery.CommandText += "\'" + with_1.RDGatewayPassword + "\',";
                }
                else
                {
                    _sqlQuery.CommandText += "\'" + "" + "\',";
                }

                if (this.SaveSecurity.Domain == true)
                {
                    _sqlQuery.CommandText += "\'" + with_1.RDGatewayDomain + "\',";
                }
                else
                {
                    _sqlQuery.CommandText += "\'" + "" + "\',";
                }

                _sqlQuery.CommandText += "\'" + with_1.UseCredSsp + "\',";

                var with_2 = with_1.Inherit;
                if (this.SaveSecurity.Inheritance == true)
                {
                    _sqlQuery.CommandText += "\'" + with_2.CacheBitmaps + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.Colors + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.Description + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.DisplayThemes + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.DisplayWallpaper + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.EnableFontSmoothing + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.EnableDesktopComposition + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.Domain + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.Icon + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.Panel + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.Password + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.Port + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.Protocol + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.PuttySession + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.RedirectDiskDrives + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.RedirectKeys + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.RedirectPorts + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.RedirectPrinters + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.RedirectSmartCards + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.RedirectSound + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.Resolution + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.UseConsoleSession + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.RenderingEngine + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.Username + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.ICAEncryption + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.RDPAuthenticationLevel + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.PreExtApp + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.PostExtApp + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.MacAddress + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.UserField + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.ExtApp + "\',";

                    _sqlQuery.CommandText += "\'" + with_2.VNCCompression + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.VNCEncoding + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.VNCAuthMode + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.VNCProxyType + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.VNCProxyIP + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.VNCProxyPort + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.VNCProxyUsername + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.VNCProxyPassword + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.VNCColors + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.VNCSmartSizeMode + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.VNCViewOnly + "\',";

                    _sqlQuery.CommandText += "\'" + with_2.RDGatewayUsageMethod + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.RDGatewayHostname + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.RDGatewayUseConnectionCredentials + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.RDGatewayUsername + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.RDGatewayPassword + "\',";
                    _sqlQuery.CommandText += "\'" + with_2.RDGatewayDomain + "\',";

                    _sqlQuery.CommandText += "\'" + with_2.UseCredSsp + "\',";
                }
                else
                {
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";

                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";
                    _sqlQuery.CommandText += "\'" + false + "\',";

                    _sqlQuery.CommandText += "\'" + false + "\',"; // .RDGatewayUsageMethod
                    _sqlQuery.CommandText += "\'" + false + "\',"; // .RDGatewayHostname
                    _sqlQuery.CommandText += "\'" + false + "\',"; // .RDGatewayUseConnectionCredentials
                    _sqlQuery.CommandText += "\'" + false + "\',"; // .RDGatewayUsername
                    _sqlQuery.CommandText += "\'" + false + "\',"; // .RDGatewayPassword
                    _sqlQuery.CommandText += "\'" + false + "\',"; // .RDGatewayDomain

                    _sqlQuery.CommandText += "\'" + false + "\',"; // .UseCredSsp
                }

                with_1.PositionID = _currentNodeIndex;

                if (with_1.IsContainer == false)
                {
                    if (with_1.Parent != null)
                    {
                        _parentConstantId = (with_1.Parent as Container.Info).ConnectionInfo.ConstantID;
                    }
                    else
                    {
                        _parentConstantId = "0";
                    }
                }
                else
                {
                    if ((with_1.Parent as Container.Info).Parent != null)
                    {
                        _parentConstantId =
                            ((with_1.Parent as Container.Info).Parent as Container.Info).ConnectionInfo.ConstantID;
                    }
                    else
                    {
                        _parentConstantId = "0";
                    }
                }

                _sqlQuery.CommandText +=
                    System.Convert.ToString(_currentNodeIndex.ToString() + "," + _parentConstantId + "," +
                                            with_1.ConstantID + ",\'" + Tools.Misc.DBDate(DateTime.Now) + "\')");
            }

            #endregion SQL

            #region XML

            private void EncryptCompleteFile()
            {
                StreamReader streamReader = new StreamReader(ConnectionFileName);

                string fileContents;
                fileContents = streamReader.ReadToEnd();
                streamReader.Close();

                if (!string.IsNullOrEmpty(fileContents))
                {
                    StreamWriter streamWriter = new StreamWriter(ConnectionFileName);
                    streamWriter.Write(Security.Crypt.Encrypt(fileContents, _password));
                    streamWriter.Close();
                }
            }

            private void SaveToXml()
            {
                try
                {
                    if (Runtime.IsConnectionsFileLoaded == false)
                    {
                        return;
                    }

                    TreeNode treeNode;
                    bool isExport = false;

                    if (Tree.Node.GetNodeType(RootTreeNode) == Tree.Node.Type.Root)
                    {
                        treeNode = (TreeNode)RootTreeNode.Clone();
                    }
                    else
                    {
                        treeNode = new TreeNode("mR|Export (" + Tools.Misc.DBDate(DateTime.Now) + ")");
                        treeNode.Nodes.Add((string)(RootTreeNode.Clone()));
                        isExport = true;
                    }

                    string tempFileName = Path.GetTempFileName();
                    _xmlTextWriter = new XmlTextWriter(tempFileName, System.Text.Encoding.UTF8);

                    _xmlTextWriter.Formatting = Formatting.Indented;
                    _xmlTextWriter.Indentation = 4;

                    _xmlTextWriter.WriteStartDocument();

                    _xmlTextWriter.WriteStartElement("Connections"); // Do not localize
                    _xmlTextWriter.WriteAttributeString("Name", "", treeNode.Text);
                    _xmlTextWriter.WriteAttributeString("Export", "", isExport.ToString());

                    if (isExport)
                    {
                        _xmlTextWriter.WriteAttributeString("Protected", "",
                                                            Security.Crypt.Encrypt("ThisIsNotProtected", _password));
                    }
                    else
                    {
                        if ((treeNode.Tag as Root.Info).Password == true)
                        {
                            _password = (treeNode.Tag as Root.Info).PasswordString;
                            _xmlTextWriter.WriteAttributeString("Protected", "",
                                                                Security.Crypt.Encrypt("ThisIsProtected", _password));
                        }
                        else
                        {
                            _xmlTextWriter.WriteAttributeString("Protected", "",
                                                                Security.Crypt.Encrypt("ThisIsNotProtected", _password));
                        }
                    }

                    _xmlTextWriter.WriteAttributeString("ConfVersion", "",
                                                        (string)
                                                        (mRemoteNC.App.Info.Connections.ConnectionFileVersion.ToString(
                                                            CultureInfo.InvariantCulture)));

                    TreeNodeCollection treeNodeCollection;
                    treeNodeCollection = treeNode.Nodes;

                    SaveNode(treeNodeCollection);

                    _xmlTextWriter.WriteEndElement();
                    _xmlTextWriter.Close();

                    string backupFileName = ConnectionFileName + ".backup";
                    File.Delete(backupFileName);
                    File.Move(ConnectionFileName, backupFileName);
                    File.Move(tempFileName, ConnectionFileName);
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)("SaveToXml failed" + Constants.vbNewLine + ex.Message),
                                                        true);
                }
            }

            private void SaveNode(TreeNodeCollection tNC)
            {
                try
                {
                    foreach (TreeNode node in tNC)
                    {
                        Connection.Info curConI;

                        if (Tree.Node.GetNodeType(node) == Tree.Node.Type.Connection ||
                            Tree.Node.GetNodeType(node) == Tree.Node.Type.Container)
                        {
                            _xmlTextWriter.WriteStartElement("Node");
                            _xmlTextWriter.WriteAttributeString("Name", "", node.Text);
                            _xmlTextWriter.WriteAttributeString("Type", "", Tree.Node.GetNodeType(node).ToString());
                        }

                        if (Tree.Node.GetNodeType(node) == Tree.Node.Type.Container) //container
                        {
                            _xmlTextWriter.WriteAttributeString("Expanded", "",
                                                                (string)
                                                                (this.ContainerList[node.Tag].TreeNode.IsExpanded).
                                                                    ToString());
                            curConI = this.ContainerList[node.Tag].ConnectionInfo;
                            SaveConnectionFields(curConI);
                            SaveNode(node.Nodes);
                            _xmlTextWriter.WriteEndElement();
                        }

                        if (Tree.Node.GetNodeType(node) == Tree.Node.Type.Connection)
                        {
                            curConI = (Connection.Info)this.ConnectionList[node.Tag];
                            SaveConnectionFields(curConI);
                            _xmlTextWriter.WriteEndElement();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)("SaveNode failed" + Constants.vbNewLine + ex.Message),
                                                        true);
                }
            }

            private void SaveConnectionFields(Connection.Info curConI)
            {
                try
                {
                    _xmlTextWriter.WriteAttributeString("Descr", "", (string)curConI.Description);

                    _xmlTextWriter.WriteAttributeString("Icon", "", (string)curConI.Icon);

                    _xmlTextWriter.WriteAttributeString("Panel", "", (string)curConI.Panel);

                    if (this.SaveSecurity.Username == true)
                    {
                        _xmlTextWriter.WriteAttributeString("Username", "", (string)curConI.Username);
                    }
                    else
                    {
                        _xmlTextWriter.WriteAttributeString("Username", "", "");
                    }

                    if (this.SaveSecurity.Domain == true)
                    {
                        _xmlTextWriter.WriteAttributeString("Domain", "", (string)curConI.Domain);
                    }
                    else
                    {
                        _xmlTextWriter.WriteAttributeString("Domain", "", "");
                    }

                    if (this.SaveSecurity.Password == true)
                    {
                        _xmlTextWriter.WriteAttributeString("Password", "",
                                                            Security.Crypt.Encrypt((string)curConI.Password, _password));
                    }
                    else
                    {
                        _xmlTextWriter.WriteAttributeString("Password", "", "");
                    }

                    _xmlTextWriter.WriteAttributeString("Hostname", "", (string)curConI.Hostname);

                    _xmlTextWriter.WriteAttributeString("Protocol", "", (string)(curConI.Protocol.ToString()));

                    _xmlTextWriter.WriteAttributeString("PuttySession", "", (string)curConI.PuttySession);

                    _xmlTextWriter.WriteAttributeString("Port", "", (string)curConI.Port.ToString());

                    _xmlTextWriter.WriteAttributeString("ConnectToConsole", "",
                                                        (string)curConI.UseConsoleSession.ToString());

                    _xmlTextWriter.WriteAttributeString("UseCredSsp", "", (string)curConI.UseCredSsp.ToString());

                    _xmlTextWriter.WriteAttributeString("RenderingEngine", "",
                                                        (string)(curConI.RenderingEngine.ToString()));

                    _xmlTextWriter.WriteAttributeString("ICAEncryptionStrength", "",
                                                        (string)(curConI.ICAEncryption.ToString()));

                    _xmlTextWriter.WriteAttributeString("RDPAuthenticationLevel", "",
                                                        (string)(curConI.RDPAuthenticationLevel.ToString()));

                    _xmlTextWriter.WriteAttributeString("Colors", "", (string)(curConI.Colors.ToString()));

                    _xmlTextWriter.WriteAttributeString("Resolution", "", (string)(curConI.Resolution.ToString()));

                    _xmlTextWriter.WriteAttributeString("DisplayWallpaper", "",
                                                        (string)curConI.DisplayWallpaper.ToString());

                    _xmlTextWriter.WriteAttributeString("DisplayThemes", "", (string)curConI.DisplayThemes.ToString());

                    _xmlTextWriter.WriteAttributeString("EnableFontSmoothing", "",
                                                        (string)curConI.EnableFontSmoothing.ToString());

                    _xmlTextWriter.WriteAttributeString("EnableDesktopComposition", "",
                                                        (string)curConI.EnableDesktopComposition.ToString());

                    _xmlTextWriter.WriteAttributeString("CacheBitmaps", "", (string)curConI.CacheBitmaps.ToString());

                    _xmlTextWriter.WriteAttributeString("RedirectDiskDrives", "",
                                                        (string)curConI.RedirectDiskDrives.ToString());

                    _xmlTextWriter.WriteAttributeString("RedirectPorts", "", (string)curConI.RedirectPorts.ToString());

                    _xmlTextWriter.WriteAttributeString("RedirectPrinters", "",
                                                        (string)curConI.RedirectPrinters.ToString());

                    _xmlTextWriter.WriteAttributeString("RedirectSmartCards", "",
                                                        (string)curConI.RedirectSmartCards.ToString());

                    _xmlTextWriter.WriteAttributeString("RedirectSound", "", (string)(curConI.RedirectSound.ToString()));

                    _xmlTextWriter.WriteAttributeString("RedirectKeys", "", (string)curConI.RedirectKeys.ToString());

                    if (curConI.OpenConnections.Count > 0)
                    {
                        _xmlTextWriter.WriteAttributeString("Connected", "", true.ToString());
                    }
                    else
                    {
                        _xmlTextWriter.WriteAttributeString("Connected", "", false.ToString());
                    }

                    _xmlTextWriter.WriteAttributeString("PreExtApp", "", (string)curConI.PreExtApp);
                    _xmlTextWriter.WriteAttributeString("PostExtApp", "", (string)curConI.PostExtApp);
                    _xmlTextWriter.WriteAttributeString("MacAddress", "", (string)curConI.MacAddress);
                    _xmlTextWriter.WriteAttributeString("UserField", "", (string)curConI.UserField);
                    _xmlTextWriter.WriteAttributeString("ExtApp", "", (string)curConI.ExtApp);

                    _xmlTextWriter.WriteAttributeString("VNCCompression", "",
                                                        (string)(curConI.VNCCompression.ToString()));
                    _xmlTextWriter.WriteAttributeString("VNCEncoding", "", (string)(curConI.VNCEncoding.ToString()));
                    _xmlTextWriter.WriteAttributeString("VNCAuthMode", "", (string)(curConI.VNCAuthMode.ToString()));
                    _xmlTextWriter.WriteAttributeString("VNCProxyType", "", (string)(curConI.VNCProxyType.ToString()));
                    _xmlTextWriter.WriteAttributeString("VNCProxyIP", "", (string)curConI.VNCProxyIP);
                    _xmlTextWriter.WriteAttributeString("VNCProxyPort", "", curConI.VNCProxyPort.ToString());
                    _xmlTextWriter.WriteAttributeString("VNCProxyUsername", "", (string)curConI.VNCProxyUsername);
                    _xmlTextWriter.WriteAttributeString("VNCProxyPassword", "",
                                                        Security.Crypt.Encrypt((string)curConI.VNCProxyPassword,
                                                                               _password));
                    _xmlTextWriter.WriteAttributeString("VNCColors", "", (string)(curConI.VNCColors.ToString()));
                    _xmlTextWriter.WriteAttributeString("VNCSmartSizeMode", "",
                                                        (string)(curConI.VNCSmartSizeMode.ToString()));
                    _xmlTextWriter.WriteAttributeString("VNCViewOnly", "", (string)curConI.VNCViewOnly.ToString());

                    _xmlTextWriter.WriteAttributeString("RDGatewayUsageMethod", "",
                                                        (string)(curConI.RDGatewayUsageMethod.ToString()));
                    _xmlTextWriter.WriteAttributeString("RDGatewayHostname", "", (string)curConI.RDGatewayHostname);

                    _xmlTextWriter.WriteAttributeString("RDGatewayUseConnectionCredentials", "",
                                                        (string)(curConI.RDGatewayUseConnectionCredentials.ToString()));

                    _xmlTextWriter.WriteAttributeString("ConnectOnStartup", "", (string)curConI.ConnectOnStartup.ToString());

                    if (this.SaveSecurity.Username == true)
                    {
                        _xmlTextWriter.WriteAttributeString("RDGatewayUsername", "", (string)curConI.RDGatewayUsername);
                    }
                    else
                    {
                        _xmlTextWriter.WriteAttributeString("RDGatewayUsername", "", "");
                    }

                    if (this.SaveSecurity.Password == true)
                    {
                        _xmlTextWriter.WriteAttributeString("RDGatewayPassword", "", (string)curConI.RDGatewayPassword);
                    }
                    else
                    {
                        _xmlTextWriter.WriteAttributeString("RDGatewayPassword", "", "");
                    }

                    if (this.SaveSecurity.Domain == true)
                    {
                        _xmlTextWriter.WriteAttributeString("RDGatewayDomain", "", (string)curConI.RDGatewayDomain);
                    }
                    else
                    {
                        _xmlTextWriter.WriteAttributeString("RDGatewayDomain", "", "");
                    }

                    if (this.SaveSecurity.Inheritance == true)
                    {
                        _xmlTextWriter.WriteAttributeString("InheritCacheBitmaps", "",
                                                            curConI.Inherit.CacheBitmaps.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritColors", "", curConI.Inherit.Colors.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritDescription", "",
                                                            curConI.Inherit.Description.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritDisplayThemes", "",
                                                            curConI.Inherit.DisplayThemes.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritDisplayWallpaper", "",
                                                            curConI.Inherit.DisplayWallpaper.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritEnableFontSmoothing", "",
                                                            curConI.Inherit.EnableFontSmoothing.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritEnableDesktopComposition", "",
                                                            curConI.Inherit.EnableDesktopComposition.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritDomain", "", curConI.Inherit.Domain.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritIcon", "", curConI.Inherit.Icon.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritPanel", "", curConI.Inherit.Panel.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritPassword", "", curConI.Inherit.Password.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritPort", "", curConI.Inherit.Port.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritProtocol", "", curConI.Inherit.Protocol.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritPuttySession", "",
                                                            curConI.Inherit.PuttySession.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRedirectDiskDrives", "",
                                                            curConI.Inherit.RedirectDiskDrives.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRedirectKeys", "",
                                                            curConI.Inherit.RedirectKeys.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRedirectPorts", "",
                                                            curConI.Inherit.RedirectPorts.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRedirectPrinters", "",
                                                            curConI.Inherit.RedirectPrinters.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRedirectSmartCards", "",
                                                            curConI.Inherit.RedirectSmartCards.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRedirectSound", "",
                                                            curConI.Inherit.RedirectSound.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritResolution", "",
                                                            curConI.Inherit.Resolution.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritUseConsoleSession", "",
                                                            curConI.Inherit.UseConsoleSession.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritUseCredSsp", "",
                                                            curConI.Inherit.UseCredSsp.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRenderingEngine", "",
                                                            curConI.Inherit.RenderingEngine.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritUsername", "", curConI.Inherit.Username.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritICAEncryptionStrength", "",
                                                            curConI.Inherit.ICAEncryption.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRDPAuthenticationLevel", "",
                                                            curConI.Inherit.RDPAuthenticationLevel.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritPreExtApp", "", curConI.Inherit.PreExtApp.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritPostExtApp", "",
                                                            curConI.Inherit.PostExtApp.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritMacAddress", "",
                                                            curConI.Inherit.MacAddress.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritUserField", "", curConI.Inherit.UserField.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritExtApp", "", curConI.Inherit.ExtApp.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCCompression", "",
                                                            curConI.Inherit.VNCCompression.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCEncoding", "",
                                                            curConI.Inherit.VNCEncoding.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCAuthMode", "",
                                                            curConI.Inherit.VNCAuthMode.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCProxyType", "",
                                                            curConI.Inherit.VNCProxyType.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCProxyIP", "",
                                                            curConI.Inherit.VNCProxyIP.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCProxyPort", "",
                                                            curConI.Inherit.VNCProxyPort.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCProxyUsername", "",
                                                            curConI.Inherit.VNCProxyUsername.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCProxyPassword", "",
                                                            curConI.Inherit.VNCProxyPassword.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCColors", "", curConI.Inherit.VNCColors.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCSmartSizeMode", "",
                                                            curConI.Inherit.VNCSmartSizeMode.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCViewOnly", "",
                                                            curConI.Inherit.VNCViewOnly.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRDGatewayUsageMethod", "",
                                                            curConI.Inherit.RDGatewayUsageMethod.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRDGatewayHostname", "",
                                                            curConI.Inherit.RDGatewayHostname.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRDGatewayUseConnectionCredentials", "",
                                                            curConI.Inherit.RDGatewayUseConnectionCredentials.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRDGatewayUsername", "",
                                                            curConI.Inherit.RDGatewayUsername.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRDGatewayPassword", "",
                                                            curConI.Inherit.RDGatewayPassword.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRDGatewayDomain", "",
                                                            curConI.Inherit.RDGatewayDomain.ToString());
                    }
                    else
                    {
                        _xmlTextWriter.WriteAttributeString("InheritCacheBitmaps", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritColors", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritDescription", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritDisplayThemes", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritDisplayWallpaper", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritEnableFontSmoothing", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritEnableDesktopComposition", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritDomain", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritIcon", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritPanel", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritPassword", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritPort", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritProtocol", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritPuttySession", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRedirectDiskDrives", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRedirectKeys", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRedirectPorts", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRedirectPrinters", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRedirectSmartCards", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRedirectSound", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritResolution", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritUseConsoleSession", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritUseCredSsp", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRenderingEngine", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritUsername", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritICAEncryptionStrength", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRDPAuthenticationLevel", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritPreExtApp", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritPostExtApp", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritMacAddress", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritUserField", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritExtApp", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCCompression", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCEncoding", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCAuthMode", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCProxyType", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCProxyIP", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCProxyPort", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCProxyUsername", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCProxyPassword", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCColors", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCSmartSizeMode", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritVNCViewOnly", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRDGatewayHostname", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRDGatewayUseConnectionCredentials", "",
                                                            false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRDGatewayUsername", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRDGatewayPassword", "", false.ToString());
                        _xmlTextWriter.WriteAttributeString("InheritRDGatewayDomain", "", false.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("SaveConnectionFields failed" + Constants.vbNewLine +
                                                         ex.Message), true);
                }
            }

            #endregion XML

            #region CSV

            private StreamWriter csvWr;

            private void SaveTomRCSV()
            {
                if (Runtime.IsConnectionsFileLoaded == false)
                {
                    return;
                }

                TreeNode tN;
                tN = (TreeNode)RootTreeNode.Clone();

                TreeNodeCollection tNC;
                tNC = tN.Nodes;

                csvWr = new StreamWriter(ConnectionFileName);

                string csvLn = string.Empty;

                csvLn += "Name;Folder;Description;Icon;Panel;";

                if (SaveSecurity.Username)
                {
                    csvLn += "Username;";
                }

                if (SaveSecurity.Password)
                {
                    csvLn += "Password;";
                }

                if (SaveSecurity.Domain)
                {
                    csvLn += "Domain;";
                }

                csvLn +=
                    "Hostname;Protocol;PuttySession;Port;ConnectToConsole;UseCredSsp;RenderingEngine;ICAEncryptionStrength;RDPAuthenticationLevel;Colors;Resolution;DisplayWallpaper;DisplayThemes;EnableFontSmoothing;EnableDesktopComposition;CacheBitmaps;RedirectDiskDrives;RedirectPorts;RedirectPrinters;RedirectSmartCards;RedirectSound;RedirectKeys;PreExtApp;PostExtApp;MacAddress;UserField;ExtApp;VNCCompression;VNCEncoding;VNCAuthMode;VNCProxyType;VNCProxyIP;VNCProxyPort;VNCProxyUsername;VNCProxyPassword;VNCColors;VNCSmartSizeMode;VNCViewOnly;RDGatewayUsageMethod;RDGatewayHostname;RDGatewayUseConnectionCredentials;RDGatewayUsername;RDGatewayPassword;RDGatewayDomain;";

                if (SaveSecurity.Inheritance)
                {
                    csvLn +=
                        "InheritCacheBitmaps;InheritColors;InheritDescription;InheritDisplayThemes;InheritDisplayWallpaper;InheritEnableFontSmoothing;InheritEnableDesktopComposition;InheritDomain;InheritIcon;InheritPanel;InheritPassword;InheritPort;InheritProtocol;InheritPuttySession;InheritRedirectDiskDrives;InheritRedirectKeys;InheritRedirectPorts;InheritRedirectPrinters;InheritRedirectSmartCards;InheritRedirectSound;InheritResolution;InheritUseConsoleSession;InheritUseCredSsp;InheritRenderingEngine;InheritUsername;InheritICAEncryptionStrength;InheritRDPAuthenticationLevel;InheritPreExtApp;InheritPostExtApp;InheritMacAddress;InheritUserField;InheritExtApp;InheritVNCCompression;InheritVNCEncoding;InheritVNCAuthMode;InheritVNCProxyType;InheritVNCProxyIP;InheritVNCProxyPort;InheritVNCProxyUsername;InheritVNCProxyPassword;InheritVNCColors;InheritVNCSmartSizeMode;InheritVNCViewOnly;InheritRDGatewayUsageMethod;InheritRDGatewayHostname;InheritRDGatewayUseConnectionCredentials;InheritRDGatewayUsername;InheritRDGatewayPassword;InheritRDGatewayDomain";
                }

                csvWr.WriteLine(csvLn);

                SaveNodemRCSV(tNC);

                csvWr.Close();
            }

            private void SaveNodemRCSV(TreeNodeCollection tNC)
            {
                foreach (TreeNode node in tNC)
                {
                    if (Tree.Node.GetNodeType(node) == Tree.Node.Type.Connection)
                    {
                        Connection.Info curConI = (Connection.Info)node.Tag;

                        WritemRCSVLine(curConI);
                    }
                    else if (Tree.Node.GetNodeType(node) == Tree.Node.Type.Container)
                    {
                        SaveNodemRCSV(node.Nodes);
                    }
                }
            }

            private void WritemRCSVLine(Connection.Info con)
            {
                string nodePath = (string)con.TreeNode.FullPath;

                int firstSlash = nodePath.IndexOf("\\");
                nodePath = nodePath.Remove(0, firstSlash + 1);
                int lastSlash = nodePath.LastIndexOf("\\");

                if (lastSlash > 0)
                {
                    nodePath = nodePath.Remove(lastSlash);
                }
                else
                {
                    nodePath = "";
                }

                string csvLn = string.Empty;

                csvLn +=
                    (string)
                    (con.Name + ";" + nodePath + ";" + con.Description + ";" + con.Icon + ";" + con.Panel + ";");

                if (SaveSecurity.Username)
                {
                    csvLn += con.Username + ";";
                }

                if (SaveSecurity.Password)
                {
                    csvLn += con.Password + ";";
                }

                if (SaveSecurity.Domain)
                {
                    csvLn += con.Domain + ";";
                }

                csvLn +=
                    (string)
                    (con.Hostname + ";" + con.Protocol.ToString() + ";" + con.PuttySession + ";" + con.Port + ";" +
                     con.UseConsoleSession + ";" + con.UseCredSsp + ";" + con.RenderingEngine.ToString() + ";" +
                     con.ICAEncryption.ToString() + ";" + con.RDPAuthenticationLevel.ToString() + ";" +
                     con.Colors.ToString() + ";" + con.Resolution.ToString() + ";" + con.DisplayWallpaper + ";" +
                     con.DisplayThemes + ";" + con.EnableFontSmoothing + ";" + con.EnableDesktopComposition + ";" +
                     con.CacheBitmaps + ";" + con.RedirectDiskDrives + ";" + con.RedirectPorts + ";" +
                     con.RedirectPrinters + ";" + con.RedirectSmartCards + ";" + con.RedirectSound.ToString() + ";" +
                     con.RedirectKeys + ";" + con.PreExtApp + ";" + con.PostExtApp + ";" + con.MacAddress + ";" +
                     con.UserField + ";" + con.ExtApp + ";" + con.VNCCompression.ToString() + ";" +
                     con.VNCEncoding.ToString() + ";" + con.VNCAuthMode.ToString() + ";" + con.VNCProxyType.ToString() +
                     ";" + con.VNCProxyIP + ";" + con.VNCProxyPort + ";" + con.VNCProxyUsername + ";" +
                     con.VNCProxyPassword + ";" + con.VNCColors.ToString() + ";" + con.VNCSmartSizeMode.ToString() + ";" +
                     con.VNCViewOnly + ";");

                if (SaveSecurity.Inheritance)
                {
                    csvLn +=
                        (string)
                        (con.Inherit.CacheBitmaps + ";" + con.Inherit.Colors + ";" + con.Inherit.Description + ";" +
                         con.Inherit.DisplayThemes + ";" + con.Inherit.DisplayWallpaper + ";" +
                         con.Inherit.EnableFontSmoothing + ";" + con.Inherit.EnableDesktopComposition + ";" +
                         con.Inherit.Domain + ";" + con.Inherit.Icon + ";" + con.Inherit.Panel + ";" +
                         con.Inherit.Password + ";" + con.Inherit.Port + ";" + con.Inherit.Protocol + ";" +
                         con.Inherit.PuttySession + ";" + con.Inherit.RedirectDiskDrives + ";" +
                         con.Inherit.RedirectKeys + ";" + con.Inherit.RedirectPorts + ";" + con.Inherit.RedirectPrinters +
                         ";" + con.Inherit.RedirectSmartCards + ";" + con.Inherit.RedirectSound + ";" +
                         con.Inherit.Resolution + ";" + con.Inherit.UseConsoleSession + ";" + con.Inherit.UseCredSsp +
                         ";" + con.Inherit.RenderingEngine + ";" + con.Inherit.Username + ";" +
                         con.Inherit.ICAEncryption + ";" + con.Inherit.RDPAuthenticationLevel + ";" +
                         con.Inherit.PreExtApp + ";" + con.Inherit.PostExtApp + ";" + con.Inherit.MacAddress + ";" +
                         con.Inherit.UserField + ";" + con.Inherit.ExtApp + ";" + con.Inherit.VNCCompression + ";" +
                         con.Inherit.VNCEncoding + ";" + con.Inherit.VNCAuthMode + ";" + con.Inherit.VNCProxyType + ";" +
                         con.Inherit.VNCProxyIP + ";" + con.Inherit.VNCProxyPort + ";" + con.Inherit.VNCProxyUsername +
                         ";" + con.Inherit.VNCProxyPassword + ";" + con.Inherit.VNCColors + ";" +
                         con.Inherit.VNCSmartSizeMode + ";" + con.Inherit.VNCViewOnly);
                }

                csvWr.WriteLine(csvLn);
            }

            #endregion CSV

            #region vRD CSV

            private void SaveTovRDCSV()
            {
                if (Runtime.IsConnectionsFileLoaded == false)
                {
                    return;
                }

                TreeNode tN;
                tN = (TreeNode)RootTreeNode.Clone();

                TreeNodeCollection tNC;
                tNC = tN.Nodes;

                csvWr = new StreamWriter(ConnectionFileName);

                SaveNodevRDCSV(tNC);

                csvWr.Close();
            }

            private void SaveNodevRDCSV(TreeNodeCollection tNC)
            {
                foreach (TreeNode node in tNC)
                {
                    if (Tree.Node.GetNodeType(node) == Tree.Node.Type.Connection)
                    {
                        Connection.Info curConI = (Connection.Info)node.Tag;

                        if (curConI.Protocol == mRemoteNC.Protocols.RDP)
                        {
                            WritevRDCSVLine(curConI);
                        }
                    }
                    else if (Tree.Node.GetNodeType(node) == Tree.Node.Type.Container)
                    {
                        SaveNodevRDCSV(node.Nodes);
                    }
                }
            }

            private void WritevRDCSVLine(Connection.Info con)
            {
                string nodePath = (string)con.TreeNode.FullPath;

                int firstSlash = nodePath.IndexOf("\\");
                nodePath = nodePath.Remove(0, firstSlash + 1);
                int lastSlash = nodePath.LastIndexOf("\\");

                if (lastSlash > 0)
                {
                    nodePath = nodePath.Remove(lastSlash);
                }
                else
                {
                    nodePath = "";
                }

                csvWr.WriteLine(con.Name + ";" + con.Hostname + ";" + con.MacAddress + ";;" + con.Port + ";" +
                                con.UseConsoleSession + ";" + nodePath);
            }

            #endregion vRD CSV

            #region vRD VRE

            private void SaveToVRE()
            {
                if (Runtime.IsConnectionsFileLoaded == false)
                {
                    return;
                }

                TreeNode tN;
                tN = (TreeNode)RootTreeNode.Clone();

                TreeNodeCollection tNC;
                tNC = tN.Nodes;

                _xmlTextWriter = new XmlTextWriter(ConnectionFileName, System.Text.Encoding.UTF8);
                _xmlTextWriter.Formatting = Formatting.Indented;
                _xmlTextWriter.Indentation = 4;

                _xmlTextWriter.WriteStartDocument();

                _xmlTextWriter.WriteStartElement("vRDConfig");
                _xmlTextWriter.WriteAttributeString("Version", "", "2.0");

                _xmlTextWriter.WriteStartElement("Connections");
                SaveNodeVRE(tNC);
                _xmlTextWriter.WriteEndElement();

                _xmlTextWriter.WriteEndElement();
                _xmlTextWriter.WriteEndDocument();
                _xmlTextWriter.Close();
            }

            private void SaveNodeVRE(TreeNodeCollection tNC)
            {
                foreach (TreeNode node in tNC)
                {
                    if (Tree.Node.GetNodeType(node) == Tree.Node.Type.Connection)
                    {
                        Connection.Info curConI = (Connection.Info)node.Tag;

                        if (curConI.Protocol == mRemoteNC.Protocols.RDP)
                        {
                            _xmlTextWriter.WriteStartElement("Connection");
                            _xmlTextWriter.WriteAttributeString("Id", "", "");

                            WriteVREitem(curConI);

                            _xmlTextWriter.WriteEndElement();
                        }
                    }
                    else
                    {
                        SaveNodeVRE(node.Nodes);
                    }
                }
            }

            private void WriteVREitem(Connection.Info con)
            {
                //Name
                _xmlTextWriter.WriteStartElement("ConnectionName");
                _xmlTextWriter.WriteValue(con.Name);
                _xmlTextWriter.WriteEndElement();

                //Hostname
                _xmlTextWriter.WriteStartElement("ServerName");
                _xmlTextWriter.WriteValue(con.Hostname);
                _xmlTextWriter.WriteEndElement();

                //Mac Adress
                _xmlTextWriter.WriteStartElement("MACAddress");
                _xmlTextWriter.WriteValue(con.MacAddress);
                _xmlTextWriter.WriteEndElement();

                //Management Board URL
                _xmlTextWriter.WriteStartElement("MgmtBoardUrl");
                _xmlTextWriter.WriteValue("");
                _xmlTextWriter.WriteEndElement();

                //Description
                _xmlTextWriter.WriteStartElement("Description");
                _xmlTextWriter.WriteValue(con.Description);
                _xmlTextWriter.WriteEndElement();

                //Port
                _xmlTextWriter.WriteStartElement("Port");
                _xmlTextWriter.WriteValue(con.Port);
                _xmlTextWriter.WriteEndElement();

                //Console Session
                _xmlTextWriter.WriteStartElement("Console");
                _xmlTextWriter.WriteValue(con.UseConsoleSession);
                _xmlTextWriter.WriteEndElement();

                //Redirect Clipboard
                _xmlTextWriter.WriteStartElement("ClipBoard");
                _xmlTextWriter.WriteValue(true);
                _xmlTextWriter.WriteEndElement();

                //Redirect Printers
                _xmlTextWriter.WriteStartElement("Printer");
                _xmlTextWriter.WriteValue(con.RedirectPrinters);
                _xmlTextWriter.WriteEndElement();

                //Redirect Ports
                _xmlTextWriter.WriteStartElement("Serial");
                _xmlTextWriter.WriteValue(con.RedirectPorts);
                _xmlTextWriter.WriteEndElement();

                //Redirect Disks
                _xmlTextWriter.WriteStartElement("LocalDrives");
                _xmlTextWriter.WriteValue(con.RedirectDiskDrives);
                _xmlTextWriter.WriteEndElement();

                //Redirect Smartcards
                _xmlTextWriter.WriteStartElement("SmartCard");
                _xmlTextWriter.WriteValue(con.RedirectSmartCards);
                _xmlTextWriter.WriteEndElement();

                //Connection Place
                _xmlTextWriter.WriteStartElement("ConnectionPlace");
                _xmlTextWriter.WriteValue("2"); //----------------------------------------------------------
                _xmlTextWriter.WriteEndElement();

                //Smart Size
                _xmlTextWriter.WriteStartElement("AutoSize");
                _xmlTextWriter.WriteValue(con.Resolution == mRemoteNC.RDP.RDPResolutions.SmartSize
                                              ? true
                                              : false);
                _xmlTextWriter.WriteEndElement();

                //SeparateResolutionX
                _xmlTextWriter.WriteStartElement("SeparateResolutionX");
                _xmlTextWriter.WriteValue("1024");
                _xmlTextWriter.WriteEndElement();

                //SeparateResolutionY
                _xmlTextWriter.WriteStartElement("SeparateResolutionY");
                _xmlTextWriter.WriteValue("768");
                _xmlTextWriter.WriteEndElement();

                //TabResolutionX
                _xmlTextWriter.WriteStartElement("TabResolutionX");
                if (con.Resolution != mRemoteNC.RDP.RDPResolutions.FitToWindow &&
                    con.Resolution != mRemoteNC.RDP.RDPResolutions.Fullscreen &&
                    con.Resolution != mRemoteNC.RDP.RDPResolutions.SmartSize)
                {
                    _xmlTextWriter.WriteValue(con.Resolution.ToString().Remove(con.Resolution.ToString().IndexOf("x")));
                }
                else
                {
                    _xmlTextWriter.WriteValue("1024");
                }
                _xmlTextWriter.WriteEndElement();

                //TabResolutionY
                _xmlTextWriter.WriteStartElement("TabResolutionY");
                if (con.Resolution != mRemoteNC.RDP.RDPResolutions.FitToWindow &&
                    con.Resolution != mRemoteNC.RDP.RDPResolutions.Fullscreen &&
                    con.Resolution != mRemoteNC.RDP.RDPResolutions.SmartSize)
                {
                    _xmlTextWriter.WriteValue(con.Resolution.ToString().Remove(0, con.Resolution.ToString().IndexOf("x")));
                }
                else
                {
                    _xmlTextWriter.WriteValue("768");
                }
                _xmlTextWriter.WriteEndElement();

                //RDPColorDepth
                _xmlTextWriter.WriteStartElement("RDPColorDepth");
                _xmlTextWriter.WriteValue(con.Colors.ToString().Replace("Colors", "").Replace("Bit", ""));
                _xmlTextWriter.WriteEndElement();

                //Bitmap Caching
                _xmlTextWriter.WriteStartElement("BitmapCaching");
                _xmlTextWriter.WriteValue(con.CacheBitmaps);
                _xmlTextWriter.WriteEndElement();

                //Themes
                _xmlTextWriter.WriteStartElement("Themes");
                _xmlTextWriter.WriteValue(con.DisplayThemes);
                _xmlTextWriter.WriteEndElement();

                //Wallpaper
                _xmlTextWriter.WriteStartElement("Wallpaper");
                _xmlTextWriter.WriteValue(con.DisplayWallpaper);
                _xmlTextWriter.WriteEndElement();
            }

            #endregion vRD VRE
        }
    }
}