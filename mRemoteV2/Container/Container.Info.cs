using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AxMSTSCLib;
using AxWFICALib;
using Microsoft.VisualBasic;

//using mRemoteNC.Tools.LocalizedAttributes;

namespace mRemoteNC
{
    namespace Container
    {
        [DefaultProperty("Name")]
        public class Info
        {
            #region Properties

            private string _Name = "New Container";

            //[mRemoteNC.Container.Info.Name("strCategoryDisplay", 1), Browsable(true), ReadOnly(false), Bindable(false), DefaultValue(""), DesignOnly(false), mRemoteNC.Container.Info.Name("strPropertyNameName"), mRemoteNC.Container.Info.Name("strPropertyDescriptionName"), Attributes.Container()]
            public string Name
            {
                get { return this._ConnectionInfo.Name; }
                set { this._ConnectionInfo.Name = value; }
            }

            private TreeNode _TreeNode;

            [Category(""), Browsable(false), ReadOnly(false), Bindable(false), DefaultValue(""), DesignOnly(false)]
            public TreeNode TreeNode
            {
                get { return this._TreeNode; }
                set { this._TreeNode = value; }
            }

            private object _Parent;

            [Category(""), Browsable(false)]
            public object Parent
            {
                get { return this._Parent; }
                set { this._Parent = value; }
            }

            //Private _GlobalID As Integer = 0
            //<Category(""), _
            //    Browsable(False)> _
            //Public Property GlobalID() As Integer
            //    Get
            //        Return _GlobalID
            //    End Get
            //    Set(ByVal value As Integer)
            //        _GlobalID = value
            //    End Set
            //End Property

            private bool _IsExpanded;

            [Category(""), Browsable(false), ReadOnly(false), Bindable(false), DefaultValue(""), DesignOnly(false)]
            public bool IsExpanded
            {
                get { return this._IsExpanded; }
                set { this._IsExpanded = value; }
            }

            private Connection.Info _ConnectionInfo = new Connection.Info();

            public Connection.Info ConnectionInfo
            {
                get { return this._ConnectionInfo; }
                set { this._ConnectionInfo = value; }
            }

            #endregion Properties

            #region Methods

            public Container.Info Copy()
            {
                return (Container.Info)this.MemberwiseClone();
            }

            public Info()
            {
                this.SetDefaults();
            }

            public void SetDefaults()
            {
                if (IsExpanded == null)
                {
                    this.IsExpanded = true;
                }
            }

            #endregion Methods

            public class Attributes
            {
                public class Container : Attribute
                {
                }
            }
        }
    }
}