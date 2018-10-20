using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Ternary.Components.Experimental.System;

namespace Ternary.Components.Experimental.IO
{
    public class TernaryFile : IEnumerable<Tryte>
    {
        public TernaryString FullName { get; protected set; }
        private TernaryString _Name;
        public TernaryString Name => _Name ?? (_Name = FullName.Substring(FullName.LastIndexOf('/')));

        public Tryte Attributes { get; protected set; }

        private Tryte[] _Data;

        public Tryte FileSize => _Data != null ? _Data.Length : 0;


        public TernaryFile(TernaryString fullName, Tryte attr, Tryte[] data = null)
        {
            FullName = fullName;
            Attributes = attr;
            _Data = data;
        }

        public virtual void Rename(TernaryString newFullName)
        {
            FullName = newFullName;
            _Name = null;
        }


        public IEnumerator<Tryte> GetEnumerator()
        {
            foreach (Tryte tryte in _Data)
                yield return tryte;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
