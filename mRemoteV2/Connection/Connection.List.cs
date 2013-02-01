using System;
using System.Collections;

namespace mRemoteNC.Connection
{
    public class List : CollectionBase
    {
        #region "Public Properties"

        public object this[object Index]
        {
            get
            {
                if (Index is Connection.Info)
                {
                    return Index;
                }
                else
                {
                    //FIxME
                    return (Connection.Info)List[(int)Index];
                }
            }
        }

        public new int Count
        {
            get { return List.Count; }
        }

        #endregion "Public Properties"

        #region "Public Methods"

        public Info Add(Info cInfo)
        {
            this.List.Add(cInfo);
            return cInfo;
        }

        public void Remove()
        {
        }

        public void AddRange(Info[] cInfo)
        {
            foreach (Info cI in cInfo)
            {
                List.Add(cI);
            }
        }

        public Info FindByConstantID(string id)
        {
            foreach (Info conI in List)
            {
                if (conI.ConstantID == id)
                {
                    return conI;
                }
            }

            return null;
        }

        //Public Function Find(ByVal cInfo As Connection.Info)
        //    For Each cI As Connection.Info In List

        //    Next
        //End Function

        public Connection.List Copy()
        {
            try
            {
                return (Connection.List)this.MemberwiseClone();
            }
            catch (Exception)
            {
            }

            return null;
        }

        public new void Clear()
        {
            this.List.Clear();
        }

        #endregion "Public Methods"
    }
}