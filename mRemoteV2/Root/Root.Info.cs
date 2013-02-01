using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using My;

namespace Root
{
    [DefaultProperty("Name")]
    public class Info
    {
        public Info(RootType typ)
        {
            _Type = typ;
        }

        #region "Properties"

        private string _Name = Language.strConnections;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryDisplay", 1), Browsable(true), ReadOnly(false),
         Bindable(false), DefaultValue(""), DesignOnly(false),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameName"),
         LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionName"), Attributes.Root()]
        public string Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }

        private bool _Password;

        [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryDisplay", 1), Browsable(true), ReadOnly(false),
         Bindable(false), DefaultValue(""), DesignOnly(false),
         LocalizedAttributes.LocalizedDisplayNameAttribute("strPasswordProtect"), Attributes.Root(),
         TypeConverter(typeof(mRemoteNC.Tools.Misc.YesNoTypeConverter))]
        public bool Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private string _PasswordString;

        [Category(""), Browsable(false), ReadOnly(false), Bindable(false), DefaultValue(""), DesignOnly(false)]
        public string PasswordString
        {
            get { return _PasswordString; }
            set { _PasswordString = value; }
        }

        private Root.Info.RootType _Type = RootType.Connection;

        [Category(""), Browsable(false), ReadOnly(false), Bindable(false), DefaultValue(""), DesignOnly(false)]
        public Root.Info.RootType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private TreeNode _TreeNode;

        [Category(""), Browsable(false), ReadOnly(false), Bindable(false), DefaultValue(""), DesignOnly(false)]
        public TreeNode TreeNode
        {
            get { return this._TreeNode; }
            set { this._TreeNode = value; }
        }

        #endregion "Properties"

        public enum RootType
        {
            Connection,
            Credential
        }

        public class Attributes
        {
            public class Root : Attribute
            {
            }
        }
    }
}