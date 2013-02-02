using System;
using WeifenLuo.WinFormsUI.Docking;

namespace mRemoteNC
{
    namespace UI
    {
        namespace Window
        {
            public class Base : DockContent
            {
                public Base()
                {
                    //InitializeComponent();
                }

                #region Public Properties

                private Type _WindowType;

                public Type WindowType
                {
                    get { return this._WindowType; }
                    set { this._WindowType = value; }
                }

                private DockContent _DockPnl;

                public DockContent DockPnl
                {
                    get { return this._DockPnl; }
                    set { this._DockPnl = value; }
                }

                #endregion Public Properties

                #region Public Methods

                public void SetFormText(string Text)
                {
                    this.Text = Text;
                    this.TabText = Text;
                }

                #endregion Public Methods

                private void InitializeComponent()
                {
                    this.SuspendLayout();
                    //
                    // Base
                    //
                    this.ClientSize = new System.Drawing.Size(284, 262);
                    this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular,
                                                        System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    this.Name = "Base";
                    this.Load += new System.EventHandler(this.Base_Load);
                    this.ResumeLayout(false);
                }

                private void Base_Load(object sender, EventArgs e)
                {
                }
            }
        }
    }
}