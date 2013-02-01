using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using AxMSTSCLib;
using AxWFICALib;
using Microsoft.VisualBasic;

namespace mRemoteNC
{
    namespace Config
    {
        namespace SettingsManager
        {
            namespace Providers
            {
                public class ChooseProvider : 
#if !PORTABLE 
                    LocalFileSettingsProvider 
#else
 PortableSettingsProvider 
#endif
                {

                }

                public class PortableSettingsProvider : SettingsProvider
                {
                    private const string SETTINGSROOT = "Settings"; //XML Root Node

                    public override void Initialize(string name, NameValueCollection col)
                    {
                        base.Initialize(this.ApplicationName, col);
                    }

                    public override string ApplicationName
                    {
                        get
                        {
                            if (Application.ProductName.Trim().Length > 0)
                            {
                                return Application.ProductName;
                            }
                            else
                            {
                                System.IO.FileInfo fi = new System.IO.FileInfo(Application.ExecutablePath);
                                return fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length);
                            }
                        }
                        set
                        {
                            //Do nothing
                        }
                    }

                    public virtual string GetAppSettingsPath()
                    {
                        //Used to determine where to store the settings
                        System.IO.FileInfo fi = new System.IO.FileInfo(Application.ExecutablePath);
                        return fi.DirectoryName;
                    }

                    public virtual string GetAppSettingsFilename()
                    {
                        //Used to determine the filename to store the settings
                        return "portable.config"; //ApplicationName & ".settings"
                    }

                    public override void SetPropertyValues(SettingsContext context,
                                                           SettingsPropertyValueCollection propvals)
                    {
                        //Iterate through the settings to be stored
                        //Only dirty settings are included in propvals, and only ones relevant to this provider
                        foreach (SettingsPropertyValue propval in propvals)
                        {
                            SetValue(propval);
                        }

                        try
                        {
                            SettingsXML.Save(System.IO.Path.Combine(GetAppSettingsPath(), GetAppSettingsFilename()));
                        }
                        catch (Exception)
                        {
                            //Ignore if cant save, device been ejected
                        }
                    }

                    public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context,
                                                                                      SettingsPropertyCollection props)
                    {
                        //Create new collection of values
                        SettingsPropertyValueCollection values = new SettingsPropertyValueCollection();

                        //Iterate through the settings to be retrieved
                        foreach (SettingsProperty setting in props)
                        {
                            SettingsPropertyValue value = new SettingsPropertyValue(setting);
                            value.IsDirty = false;
                            value.SerializedValue = GetValue(setting);
                            values.Add(value);
                        }
                        return values;
                    }

                    private System.Xml.XmlDocument m_SettingsXML = null;

                    private System.Xml.XmlDocument SettingsXML
                    {
                        get
                        {
                            //If we dont hold an xml document, try opening one.
                            //If it doesnt exist then create a new one ready.
                            if (m_SettingsXML == null)
                            {
                                m_SettingsXML = new System.Xml.XmlDocument();

                                try
                                {
                                    m_SettingsXML.Load(System.IO.Path.Combine(GetAppSettingsPath(),
                                                                              GetAppSettingsFilename()));
                                }
                                catch (Exception)
                                {
                                    //Create new document
                                    XmlDeclaration dec = m_SettingsXML.CreateXmlDeclaration("1.0", "utf-8", string.Empty);
                                    m_SettingsXML.AppendChild(dec);

                                    XmlNode nodeRoot;

                                    nodeRoot = m_SettingsXML.CreateNode(XmlNodeType.Element, SETTINGSROOT.ToString(), "");
                                    m_SettingsXML.AppendChild(nodeRoot);
                                }
                            }

                            return m_SettingsXML;
                        }
                    }

                    private string GetValue(SettingsProperty setting)
                    {
                        string ret = "";

                        try
                        {
                            if (IsRoaming(setting))
                            {
                                var selectSingleNode = SettingsXML.SelectSingleNode(System.Convert.ToString(SETTINGSROOT + "/" + setting.Name));
                                if (selectSingleNode != null)
                                    ret =
                                        selectSingleNode.InnerText;
                                else
                                {
                                    ret = setting.DefaultValue != null ? setting.DefaultValue.ToString() : "";
                                }
                            }
                            else
                            {
                                ret =
                                    SettingsXML.SelectSingleNode(
                                        System.Convert.ToString(SETTINGSROOT + "/" +
                                                                (new Microsoft.VisualBasic.Devices.Computer()).Name +
                                                                "/" + setting.Name)).InnerText;
                            }
                        }
                        catch (Exception)
                        {
                            if (setting.DefaultValue != null)
                            {
                                ret = setting.DefaultValue.ToString();
                            }
                            else
                            {
                                ret = "";
                            }
                        }

                        return ret;
                    }

                    private void SetValue(SettingsPropertyValue propVal)
                    {
                        System.Xml.XmlElement MachineNode;
                        System.Xml.XmlElement SettingNode;

                        //Determine if the setting is roaming.
                        //If roaming then the value is stored as an element under the root
                        //Otherwise it is stored under a machine name node
                        try
                        {
                            if (IsRoaming(propVal.Property))
                            {
                                SettingNode =
                                    (XmlElement)
                                    (SettingsXML.SelectSingleNode(
                                        System.Convert.ToString(SETTINGSROOT + "/" + propVal.Name)));
                            }
                            else
                            {
                                SettingNode =
                                    (XmlElement)
                                    (SettingsXML.SelectSingleNode(
                                        System.Convert.ToString(SETTINGSROOT + "/" +
                                                                (new Microsoft.VisualBasic.Devices.Computer()).Name +
                                                                "/" + propVal.Name)));
                            }
                        }
                        catch (Exception)
                        {
                            SettingNode = null;
                        }

                        //Check to see if the node exists, if so then set its new value
                        if (SettingNode != null)
                        {
                            if (propVal.SerializedValue != null)
                            {
                                SettingNode.InnerText = propVal.SerializedValue.ToString();
                            }
                        }
                        else
                        {
                            if (IsRoaming(propVal.Property))
                            {
                                //Store the value as an element of the Settings Root Node
                                SettingNode = SettingsXML.CreateElement(propVal.Name);
                                if (propVal.SerializedValue != null)
                                {
                                    SettingNode.InnerText = propVal.SerializedValue.ToString();
                                }
                                SettingsXML.SelectSingleNode(SETTINGSROOT.ToString()).AppendChild(SettingNode);
                            }
                            else
                            {
                                //Its machine specific, store as an element of the machine name node,
                                //creating a new machine name node if one doesnt exist.
                                try
                                {
                                    MachineNode =
                                        (XmlElement)
                                        (SettingsXML.SelectSingleNode(
                                            System.Convert.ToString(SETTINGSROOT + "/" +
                                                                    (new Microsoft.VisualBasic.Devices.Computer()).Name)));
                                }
                                catch (Exception)
                                {
                                    MachineNode =
                                        SettingsXML.CreateElement((new Microsoft.VisualBasic.Devices.Computer()).Name);
                                    SettingsXML.SelectSingleNode(SETTINGSROOT.ToString()).AppendChild(MachineNode);
                                }

                                if (MachineNode == null)
                                {
                                    MachineNode =
                                        SettingsXML.CreateElement((new Microsoft.VisualBasic.Devices.Computer()).Name);
                                    SettingsXML.SelectSingleNode(SETTINGSROOT.ToString()).AppendChild(MachineNode);
                                }

                                SettingNode = SettingsXML.CreateElement(propVal.Name);
                                if (propVal.SerializedValue != null)
                                {
                                    SettingNode.InnerText = propVal.SerializedValue.ToString();
                                }
                                MachineNode.AppendChild(SettingNode);
                            }
                        }
                    }

                    private bool IsRoaming(SettingsProperty prop)
                    {
                        //Determine if the setting is marked as Roaming
                        //For Each d As DictionaryEntry In prop.Attributes
                        //    Dim a As Attribute = DirectCast(d.Value, Attribute)
                        //    If TypeOf a Is System.Configuration.SettingsManageabilityAttribute Then
                        //        Return True
                        //    End If
                        //Next
                        //Return False

                        return true;
                    }
                }
            }
        }
    }
}