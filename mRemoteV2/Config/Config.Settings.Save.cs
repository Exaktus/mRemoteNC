using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using My;

//using mRemoteNC.Runtime;

namespace mRemoteNC.Config
{
    namespace SettingsManager
    {
        public class Save
        {
            #region Public Methods

            public void Save_Renamed()
            {
                try
                {
                    var with_1 = frmMain.defaultInstance;
                    Tools.WindowPlacement windowPlacement = new Tools.WindowPlacement(frmMain.defaultInstance);
                    if (with_1.WindowState == FormWindowState.Minimized && windowPlacement.RestoreToMaximized)
                    {
                        with_1.Opacity = 0;
                        with_1.WindowState = FormWindowState.Maximized;
                    }

                    Settings.Default.MainFormLocation = with_1.Location;
                    Settings.Default.MainFormSize = with_1.Size;

                    if (with_1.WindowState != FormWindowState.Normal)
                    {
                        Settings.Default.MainFormRestoreLocation = with_1.RestoreBounds.Location;
                        Settings.Default.MainFormRestoreSize = with_1.RestoreBounds.Size;
                    }

                    Settings.Default.MainFormState = with_1.WindowState;

                    Settings.Default.MainFormKiosk = Tools.Misc.Fullscreen.FullscreenActive;

                    Settings.Default.FirstStart = false;
                    Settings.Default.ResetPanels = false;
                    Settings.Default.ResetToolbars = false;
                    Settings.Default.NoReconnect = false;

                    Settings.Default.ExtAppsTBLocation = with_1.tsExternalTools.Location;
                    if (with_1.tsExternalTools.Parent != null)
                    {
                        Settings.Default.ExtAppsTBParentDock = with_1.tsExternalTools.Parent.Dock.ToString();
                    }
                    Settings.Default.ExtAppsTBVisible = with_1.tsExternalTools.Visible;
                    Settings.Default.ExtAppsTBShowText = with_1.cMenToolbarShowText.Checked;

                    Settings.Default.QuickyTBLocation = with_1.tsQuickConnect.Location;
                    if (with_1.tsQuickConnect.Parent != null)
                    {
                        Settings.Default.QuickyTBParentDock = with_1.tsQuickConnect.Parent.Dock.ToString();
                    }
                    Settings.Default.QuickyTBVisible = with_1.tsQuickConnect.Visible;

                    Settings.Default.ConDefaultPassword =
                        Security.Crypt.Encrypt((string)Settings.Default.ConDefaultPassword,
                                               (string)mRemoteNC.App.Info.General.EncryptionKey);

                    Settings.Default.Save();
                    this.SavePanelsToXML();
                    this.SaveExternalAppsToXML();
                    SaveQuickTextsToXML();
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("Saving settings failed" + Constants.vbNewLine +
                                                         Constants.vbNewLine + ex.Message), false);
                }
            }

            public void SavePanelsToXML()
            {
                try
                {
                    if (Directory.Exists((string)mRemoteNC.App.Info.Settings.SettingsPath) == false)
                    {
                        Directory.CreateDirectory((string)mRemoteNC.App.Info.Settings.SettingsPath);
                    }

                    frmMain.Default.pnlDock.SaveAsXml(
                        (string)
                        (mRemoteNC.App.Info.Settings.SettingsPath + "\\" + mRemoteNC.App.Info.Settings.LayoutFileName));
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("SavePanelsToXML failed" + Constants.vbNewLine +
                                                         Constants.vbNewLine + ex.Message), false);
                }
            }

            public void SaveQuickTextsToXML()
            {
                try
                {
                    if (Directory.Exists((string)mRemoteNC.App.Info.Settings.SettingsPath) == false)
                    {
                        Directory.CreateDirectory((string)mRemoteNC.App.Info.Settings.SettingsPath);
                    }

                    XmlTextWriter xmlTextWriter =
                        new XmlTextWriter(
                            mRemoteNC.App.Info.Settings.SettingsPath + "\\" +
                            mRemoteNC.App.Info.Settings.QuickTextsFilesName, System.Text.Encoding.UTF8);
                    xmlTextWriter.Formatting = Formatting.Indented;
                    xmlTextWriter.Indentation = 4;

                    xmlTextWriter.WriteStartDocument();
                    xmlTextWriter.WriteStartElement("QuickTexts");

                    foreach (Tools.QuickText extA in Runtime.QuickTexts)
                    {
                        xmlTextWriter.WriteStartElement("QuickTexts");
                        xmlTextWriter.WriteAttributeString("DisplayName", "", (string)extA.DisplayName);
                        xmlTextWriter.WriteAttributeString("Text", "", (string)extA.Text);
                        xmlTextWriter.WriteEndElement();
                    }

                    xmlTextWriter.WriteEndElement();
                    xmlTextWriter.WriteEndDocument();

                    xmlTextWriter.Close();
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("SaveQuickTextsToXML failed" + Constants.vbNewLine +
                                                         Constants.vbNewLine + ex.Message), false);
                }
            }

            public void SaveExternalAppsToXML()
            {
                try
                {
                    if (Directory.Exists((string)mRemoteNC.App.Info.Settings.SettingsPath) == false)
                    {
                        Directory.CreateDirectory((string)mRemoteNC.App.Info.Settings.SettingsPath);
                    }

                    XmlTextWriter xmlTextWriter =
                        new XmlTextWriter(
                            mRemoteNC.App.Info.Settings.SettingsPath + "\\" +
                            mRemoteNC.App.Info.Settings.ExtAppsFilesName, System.Text.Encoding.UTF8);
                    xmlTextWriter.Formatting = Formatting.Indented;
                    xmlTextWriter.Indentation = 4;

                    xmlTextWriter.WriteStartDocument();
                    xmlTextWriter.WriteStartElement("Apps");

                    foreach (Tools.ExternalTool extA in Runtime.ExternalTools)
                    {
                        xmlTextWriter.WriteStartElement("App");
                        xmlTextWriter.WriteAttributeString("DisplayName", "", (string)extA.DisplayName);
                        xmlTextWriter.WriteAttributeString("FileName", "", (string)extA.FileName);
                        xmlTextWriter.WriteAttributeString("Arguments", "", (string)extA.Arguments);
                        xmlTextWriter.WriteAttributeString("WaitForExit", "", extA.WaitForExit.ToString());
                        xmlTextWriter.WriteAttributeString("TryToIntegrate", "", extA.TryIntegrate.ToString());
                        xmlTextWriter.WriteEndElement();
                    }

                    xmlTextWriter.WriteEndElement();
                    xmlTextWriter.WriteEndDocument();

                    xmlTextWriter.Close();
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("SaveExternalAppsToXML failed" + Constants.vbNewLine +
                                                         Constants.vbNewLine + ex.Message), false);
                }
            }

            #endregion Public Methods
        }
    }
}