using System;
using System.Collections;
using System.Collections.Generic;

namespace mRemoteNC
{
    public class List : CollectionBase
    {
        #region "Public Properties"

        public Base this[object Index]
        {
            get
            {
                var vl = Index as Base;
                if (vl != null)
                {
                    return vl;
                }
                else
                {
                    return (Base)List[(int)Index];
                }
            }
        }

        public new int Count
        {
            get { return List.Count; }
        }

        #endregion "Public Properties"

        #region "Public Methods"

        public Base Add(Base cProt)
        {
            this.List.Add(cProt);
            return cProt;
        }

        public void AddRange(IEnumerable<Base> cProt)
        {
            foreach (Base cP in cProt)
            {
                List.Add(cP);
            }
        }

        public void Remove(Base cProt)
        {
            try
            {
                this.List.Remove(cProt);
            }
            catch (Exception)
            {
            }
        }

        public new void Clear()
        {
            this.List.Clear();
        }

        #endregion "Public Methods"
    }
}
