using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AxWFICALib;
using Microsoft.VisualBasic;
using mRemoteNC;
using mRemoteNC.App;
using mRemoteNC.Connection;

//using mRemoteNC.Runtime;

namespace mRemoteNC
{
    namespace Protocol
    {
        public partial class InterfaceControl
        {
            #region Properties

            public Base Protocol { get; set; }

            public Info Info { get; set; }

            #endregion Properties

            #region Methods

            public InterfaceControl(Control Parent, Base Protocol, Info Info)
            {
                try
                {
                    this.Protocol = Protocol;
                    this.Info = Info;
                    this.Parent = Parent;
                    this.Location = new Point(0, 0);
                    this.Size = this.Parent.Size;
                    this.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                    InitializeComponent();
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        (string)
                                                        ("Couldn\'t create new InterfaceControl" + Constants.vbNewLine +
                                                         ex.Message));
                }
            }

            #endregion Methods
        }
    }
}