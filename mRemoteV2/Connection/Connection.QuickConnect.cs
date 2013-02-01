using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using My;

namespace mRemoteNC
{
    public class QuickConnect
    {
        public QuickConnect()
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            qBox = frmMain.Default.cmbQuickConnect;
        }

        private static ToolStripComboBox qBox = frmMain.Default.cmbQuickConnect;

        public class History
        {
            public static bool Exists(string Text)
            {
                try
                {
                    for (int i = 0; i <= qBox.Items.Count - 1; i++)
                    {
                        if ((string) qBox.Items[i] == Text)
                        {
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strQuickConnectHistoryExistsFailed +
                                                        Constants.vbNewLine + ex.Message, true);
                }

                return false;
            }

            public static void Add(string Text)
            {
                try
                {
                    qBox.Items.Insert(0, Text);
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        Language.strQuickConnectAddFailed + Constants.vbNewLine +
                                                        ex.Message, true);
                }
            }
        }
    }
}

//using mRemoteNC.Runtime;

namespace mRemoteNC.Connection
{
}