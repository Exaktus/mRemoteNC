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
    namespace Credential
    {
        public class Info
        {
            #region "1 Display"

            private string _Name;

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryDisplay", 1), Browsable(true),
             LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameName"),
             LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionName")]
            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }

            private string _Description;

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryDisplay", 1), Browsable(true),
             LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameDescription"),
             LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionDescription")]
            public string Description
            {
                get { return _Description; }
                set { _Description = value; }
            }

            #endregion "1 Display"

            #region "2 Credentials"

            private string _Username;

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryCredentials", 2), Browsable(true),
             LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameUsername"),
             LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionUsername")]
            public string Username
            {
                get { return _Username; }
                set { _Username = value; }
            }

            private string _Password;

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryCredentials", 2), Browsable(true),
             LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNamePassword"),
             LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionPassword"),
             PasswordPropertyText(true)]
            public string Password
            {
                get { return _Password; }
                set { _Password = value; }
            }

            private string _Domain;

            [LocalizedAttributes.LocalizedCategoryAttribute("strCategoryCredentials", 2), Browsable(true),
             LocalizedAttributes.LocalizedDisplayNameAttribute("strPropertyNameDomain"),
             LocalizedAttributes.LocalizedDescriptionAttribute("strPropertyDescriptionDomain")]
            public string Domain
            {
                get { return _Domain; }
                set { _Domain = value; }
            }

            #endregion "2 Credentials"
        }
    }
}