using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ternary.Components.Experimental.System;

namespace Ternary.Components.Experimental.IO
{
    public class TernaryFolder : TernaryFile
    {
        private Dictionary<TernaryString, TernaryFolder> _Folders = new Dictionary<TernaryString, TernaryFolder>();
        private Dictionary<TernaryString, TernaryFile> _Files = new Dictionary<TernaryString, TernaryFile>();


        public TernaryFolder(TernaryString fullName, Tryte attr) : base(fullName, attr) { }


        public void AddFile(TernaryFile file)
        {
            if (!_Files.ContainsKey(file.Name))
                _Files.Add(file.Name, file);
        }

        public void RemoveFile(TernaryFile file)
        {
            if (_Files.ContainsKey(file.Name))
                _Files.Remove(file.Name);
        }

        public void AddFolder(TernaryFolder folder)
        {
            if (!_Folders.ContainsKey(folder.Name))
                _Folders.Add(folder.Name, folder);
        }

        public void RemoveFolder(TernaryFolder folder)
        {
            if (_Folders.ContainsKey(folder.Name))
                _Folders.Remove(folder.Name);
        }

        public override void Rename(TernaryString newFullName)
        {
            foreach (TernaryFile file in _Folders.Values.Concat(_Files.Values))
                file.Rename(new TernaryString($"{newFullName}/{file.Name}"));

            base.Rename(newFullName);
        }
    }
}
