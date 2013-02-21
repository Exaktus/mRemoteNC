using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using My;

namespace mRemoteNC.Config
{
    namespace SettingsManager
    {
        public class Save
        {
            #region Public Methods

            public void SaveSettings()
            {
                try
                {
                    var with_1 = frmMain.defaultInstance;
                    var windowPlacement = new Tools.WindowPlacement(frmMain.defaultInstance);
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

                    Settings.Default.MainFormKiosk = frmMain.defaultInstance.fullscreenManager.FullscreenActive;

                    Settings.Default.FirstStart = false;
                    Settings.Default.ResetPanels = false;
                    Settings.Default.ResetToolbars = false;
                    Settings.Default.NoReconnect = false;

                    Settings.Default.ExtAppsTBShowText = with_1.cMenToolbarShowText.Checked;

                    Settings.Default.ConDefaultPassword =
                        Security.Crypt.Encrypt(Settings.Default.ConDefaultPassword,
                                               AppInfo.General.EncryptionKey);
                    
                    

                    //Placeholder: Add new toolbar here
                    Settings.Default.msMain = ToolStripConfig.FromPanel(with_1.msMain).ToXMLString();
                    Settings.Default.tsQuickConnect = ToolStripConfig.FromPanel(with_1.tsQuickConnect).ToXMLString();
                    Settings.Default.tsQuickTexts = ToolStripConfig.FromPanel(with_1.tsQuickTexts).ToXMLString();
                    Settings.Default.tsExternalTools = ToolStripConfig.FromPanel(with_1.tsExternalTools).ToXMLString();
                    Settings.Default.ToolStrip1 = ToolStripConfig.FromPanel(with_1.ToolStrip1).ToXMLString();

                    Settings.Default.Save();
                    SavePanelsToXML();
                    SaveExternalAppsToXML();
                    SaveQuickTextsToXML();
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        ("Saving settings failed" + Constants.vbNewLine +
                                                         Constants.vbNewLine + ex.Message), false);
                }
            }



            private void SavePanelsToXML()
            {
                try
                {
                    if (Directory.Exists(AppInfo.Settings.SettingsPath) == false)
                    {
                        Directory.CreateDirectory(AppInfo.Settings.SettingsPath);
                    }

                    frmMain.Default.pnlDock.SaveAsXml(Path.Combine(AppInfo.Settings.SettingsPath,AppInfo.Settings.LayoutFileName));
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        ("SavePanelsToXML failed" + Constants.vbNewLine +
                                                         Constants.vbNewLine + ex.Message), false);
                }
            }

            private void SaveQuickTextsToXML()
            {
                try
                {
                    if (Directory.Exists(AppInfo.Settings.SettingsPath) == false)
                    {
                        Directory.CreateDirectory(AppInfo.Settings.SettingsPath);
                    }

                    var xmlTextWriter =
                        new XmlTextWriter(Path.Combine(AppInfo.Settings.SettingsPath, AppInfo.Settings.QuickTextsFilesName), System.Text.Encoding.UTF8)
                            {Formatting = Formatting.Indented, Indentation = 4};

                    xmlTextWriter.WriteStartDocument();
                    xmlTextWriter.WriteStartElement("QuickTexts");

                    foreach (var extA in Runtime.QuickTexts)
                    {
                        xmlTextWriter.WriteStartElement("QuickTexts");
                        xmlTextWriter.WriteAttributeString("DisplayName", "", extA.DisplayName);
                        xmlTextWriter.WriteAttributeString("Text", "", extA.Text);
                        xmlTextWriter.WriteEndElement();
                    }

                    xmlTextWriter.WriteEndElement();
                    xmlTextWriter.WriteEndDocument();

                    xmlTextWriter.Close();
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        ("SaveQuickTextsToXML failed" + Constants.vbNewLine +
                                                         Constants.vbNewLine + ex.Message), false);
                }
            }

            private void SaveExternalAppsToXML()
            {
                try
                {
                    if (Directory.Exists(AppInfo.Settings.SettingsPath) == false)
                    {
                        Directory.CreateDirectory(AppInfo.Settings.SettingsPath);
                    }

                    var xmlTextWriter =
                        new XmlTextWriter(
                            AppInfo.Settings.SettingsPath + "\\" +
                            AppInfo.Settings.ExtAppsFilesName, System.Text.Encoding.UTF8)
                            {Formatting = Formatting.Indented, Indentation = 4};

                    xmlTextWriter.WriteStartDocument();
                    xmlTextWriter.WriteStartElement("Apps");

                    foreach (Tools.ExternalTool extA in Runtime.ExternalTools)
                    {
                        xmlTextWriter.WriteStartElement("App");
                        xmlTextWriter.WriteAttributeString("DisplayName", "", extA.DisplayName);
                        xmlTextWriter.WriteAttributeString("FileName", "", extA.FileName);
                        xmlTextWriter.WriteAttributeString("Arguments", "", extA.Arguments);
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
                        ("SaveExternalAppsToXML failed" + Constants.vbNewLine +
                                                         Constants.vbNewLine + ex.Message), false);
                }
            }

            #endregion Public Methods
        }
    }
}