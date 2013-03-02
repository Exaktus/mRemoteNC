using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AxWFICALib;
using Microsoft.VisualBasic;

namespace mRemoteNC
{
    namespace Credential
    {
        public class List : CollectionBase
        {
            #region Public Properties

            public Credential.Info this[object Index]
            {
                get
                {
                    if (Index is Credential.Info)
                    {
                        return (Credential.Info)Index;
                    }
                    else
                    {
                        return ((Credential.Info)(List[System.Convert.ToInt32(Index)]));
                    }
                }
            }

            public new int Count
            {
                get { return List.Count; }
            }

            #endregion Public Properties

            #region Public Methods

            public Credential.Info Add(Credential.Info cInfo)
            {
                List.Add(cInfo);
                return cInfo;
            }

            public void AddRange(Credential.Info[] cInfo)
            {
                foreach (Credential.Info cI in cInfo)
                {
                    List.Add(cI);
                }
            }

            public Credential.List Copy()
            {
                try
                {
                    return (Credential.List)this.MemberwiseClone();
                }
                catch (Exception)
                {
                }

                return null;
            }

            public new void Clear()
            {
                List.Clear();
            }

            #endregion Public Methods
        }
    }
}