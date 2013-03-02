using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AxWFICALib;
using Microsoft.VisualBasic;
using mRemoteNC.App;

//using mRemoteNC.Runtime;

namespace mRemoteNC
{
    namespace Connection
    {
        public class Icon : StringConverter
        {
            public static string[] Icons = new string[] { };

            public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(
                System.ComponentModel.ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(Icons);
            }

            public override bool GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext context)
            {
                return true;
            }

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public static System.Drawing.Icon FromString(string IconName)
            {
                try
                {
                    string IconPath =
                        (new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.DirectoryPath +
                        "\\Icons\\" + IconName + ".ico";

                    if (System.IO.File.Exists(IconPath))
                    {
                        System.Drawing.Icon nI = new System.Drawing.Icon(IconPath);

                        return nI;
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("Couldn\'t get Icon from String" + Constants.vbNewLine +
                                                         ex.Message));
                }

                return null;
            }
        }
    }
}