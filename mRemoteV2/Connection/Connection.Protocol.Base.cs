#region

using System;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using mRemoteNC.Connection;
using mRemoteNC.Protocol;

namespace mRemoteNC
{
    public class Base
    {
        #region Properties

        #region Control

        private string _Name;

        public string Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }

        private InterfaceControl _InterfaceControl;

        public InterfaceControl InterfaceControl
        {
            get { return this._InterfaceControl; }
            set { this._InterfaceControl = value; }
        }

        public Control Control { get; set; }

        #endregion Control

        private Info.Force _Force;

        public Info.Force Force
        {
            get { return this._Force; }
            set { this._Force = value; }
        }

        public System.Timers.Timer tmrReconnect = new System.Timers.Timer(2000);
        public ReconnectGroup ReconnectGroup;

        #endregion Properties

        #region Methods

        public virtual void Focus()
        {
            try
            {
                Control.Focus();
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    "Couldn\'t focus Control (Connection.Protocol.Base)" +
                                                    Constants.vbNewLine + ex.Message, true);
            }
        }

        public virtual bool SetProps()
        {
            try
            {
                this._InterfaceControl.Parent.Tag = this._InterfaceControl;
                this._InterfaceControl.Show();

                if (this.Control != null)
                {
                    this.Control.Name = this._Name;
                    this.Control.Parent = this._InterfaceControl;
                    this.Control.Location = this._InterfaceControl.Location;
                    this.Control.Size = this.InterfaceControl.Size;
                    this.Control.Anchor = this._InterfaceControl.Anchor;
                }

                return true;
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    (string)
                                                    ("Couldn\'t SetProps (Connection.Protocol.Base)" +
                                                     Constants.vbNewLine + ex.Message), true);
                return false;
            }
        }

        public virtual bool Connect()
        {
            if (InterfaceControl.Info.Protocol != Protocols.RDP)
            {
                if (ConnectedEvent != null)
                    ConnectedEvent(this);
            }
            return true; //FIXME*
        }

        public virtual void Disconnect()
        {
            this.Close();
        }

        public virtual void Resize()
        {
        }

        public virtual void Close()
        {
            Thread t = new Thread(new System.Threading.ThreadStart(CloseBG));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.IsBackground = true;
            t.Start();
        }

        private void CloseBG()
        {
            if (ClosedEvent != null)
                ClosedEvent(this);

            try
            {
                tmrReconnect.Enabled = false;

                if (this.Control != null)
                {
                    try
                    {
                        this.DisposeControl();
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                            (string)
                                                            ("Could not dispose control, probably form is already closed (Connection.Protocol.Base)" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                if (this._InterfaceControl != null)
                {
                    try
                    {
                        if (this._InterfaceControl.Parent != null)
                        {
                            if (this._InterfaceControl.Parent.Tag != null)
                            {
                                this.SetTagToNothing();
                            }

                            this.DisposeInterface();
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                            (string)
                                                            ("Could not set InterfaceControl.Parent.Tag or Dispose Interface, probably form is already closed (Connection.Protocol.Base)" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                    (string)
                                                    ("Couldn\'t Close InterfaceControl BG (Connection.Protocol.Base)" +
                                                     Constants.vbNewLine + ex.Message), true);
            }
        }

        private delegate void DisposeInterfaceCB();

        private void DisposeInterface()
        {
            if (this._InterfaceControl.InvokeRequired)
            {
                DisposeInterfaceCB s = new DisposeInterfaceCB(DisposeInterface);
                this._InterfaceControl.Invoke(s);
            }
            else
            {
                this._InterfaceControl.Dispose();
            }
        }

        private delegate void SetTagToNothingCB();

        private void SetTagToNothing()
        {
            if (this._InterfaceControl.Parent.InvokeRequired)
            {
                SetTagToNothingCB s = new SetTagToNothingCB(SetTagToNothing);
                this._InterfaceControl.Parent.Invoke(s);
            }
            else
            {
                this._InterfaceControl.Parent.Tag = null;
            }
        }

        private delegate void DisposeControlCB();

        private void DisposeControl()
        {
            if (this.Control.InvokeRequired)
            {
                DisposeControlCB s = new DisposeControlCB(DisposeControl);
                this.Control.Invoke(s);
            }
            else
            {
                this.Control.Dispose();
            }
        }

        #endregion Methods

        #region Events

        public delegate void ConnectingEventHandler(object sender);

        private ConnectingEventHandler ConnectingEvent;

        public event ConnectingEventHandler Connecting
        {
            add { ConnectingEvent = (ConnectingEventHandler)System.Delegate.Combine(ConnectingEvent, value); }
            remove { ConnectingEvent = (ConnectingEventHandler)System.Delegate.Remove(ConnectingEvent, value); }
        }

        public delegate void ConnectedEventHandler(object sender);

        private ConnectedEventHandler ConnectedEvent;

        public event ConnectedEventHandler Connected
        {
            add { ConnectedEvent = (ConnectedEventHandler)System.Delegate.Combine(ConnectedEvent, value); }
            remove { ConnectedEvent = (ConnectedEventHandler)System.Delegate.Remove(ConnectedEvent, value); }
        }

        public delegate void DisconnectedEventHandler(object sender, string DisconnectedMessage);

        private DisconnectedEventHandler DisconnectedEvent;

        public event DisconnectedEventHandler Disconnected
        {
            add { DisconnectedEvent = (DisconnectedEventHandler)System.Delegate.Combine(DisconnectedEvent, value); }
            remove { DisconnectedEvent = (DisconnectedEventHandler)System.Delegate.Remove(DisconnectedEvent, value); }
        }

        public delegate void ErrorOccuredEventHandler(object sender, string ErrorMessage);

        private ErrorOccuredEventHandler ErrorOccuredEvent;

        public event ErrorOccuredEventHandler ErrorOccured
        {
            add { ErrorOccuredEvent = (ErrorOccuredEventHandler)System.Delegate.Combine(ErrorOccuredEvent, value); }
            remove { ErrorOccuredEvent = (ErrorOccuredEventHandler)System.Delegate.Remove(ErrorOccuredEvent, value); }
        }

        public delegate void ClosingEventHandler(object sender);

        private ClosingEventHandler ClosingEvent;

        public event ClosingEventHandler Closing
        {
            add { ClosingEvent = (ClosingEventHandler)System.Delegate.Combine(ClosingEvent, value); }
            remove { ClosingEvent = (ClosingEventHandler)System.Delegate.Remove(ClosingEvent, value); }
        }

        public delegate void ClosedEventHandler(object sender);

        private ClosedEventHandler ClosedEvent;

        public event ClosedEventHandler Closed
        {
            add { ClosedEvent = (ClosedEventHandler)System.Delegate.Combine(ClosedEvent, value); }
            remove { ClosedEvent = (ClosedEventHandler)System.Delegate.Remove(ClosedEvent, value); }
        }

        public void Event_Closing(object sender)
        {
            if (ClosingEvent != null)
                ClosingEvent(sender);
        }

        public void Event_Closed(object sender)
        {
            if (ClosedEvent != null)
                ClosedEvent(sender);
        }

        public void Event_Connecting(object sender)
        {
            if (ConnectingEvent != null)
                ConnectingEvent(sender);
        }

        public void Event_Connected(object sender)
        {
            if (ConnectedEvent != null)
                ConnectedEvent(sender);
        }

        public void Event_Disconnected(object sender, string DisconnectedMessage)
        {
            if (DisconnectedEvent != null)
                DisconnectedEvent(sender, DisconnectedMessage);
        }

        public void Event_ErrorOccured(object sender, string ErrorMsg)
        {
            if (ErrorOccuredEvent != null)
                ErrorOccuredEvent(sender, ErrorMsg);
        }

        public void Event_ReconnectGroupCloseClicked()
        {
            Close();
        }

        #endregion Events
    }
}

#endregion

//using mRemoteNC.Runtime;

namespace mRemoteNC.Connection
{
}