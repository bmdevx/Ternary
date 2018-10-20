using System;
using System.Collections.Generic;
using System.Text;
using Ternary.Components.Experimental.System;

namespace Ternary.Components.Experimental.IO
{

    /*
        Input-
        Write Structure:
        1 Tryte Name Length
        X Tryte Name
        1 Tryte File Properties
        2 Tryte Size of File
        X Tryte File
        
        Read Structure:
        1 Tryte Name Length
        X Tryte Name

        Output-
        1 Tryte Number of Folders
        1 Tryte Name Length
        X Tryte Name
        1 Tryte Folder Properties
        1 Tryte Number of Files
        1 Tryte Name Length
        X Tryte Name
        1 Tryte File Properties
        2 Tryte Size of File
        X Tryte File
    */




    public class TStorage : IBusComponent<Tryte>
    {
        public string ComponentName { get; internal set; }

        public event ComponentBusTriggeredEvent<Tryte> BusOutput;


        public bool InOperation { get; private set; }
        public bool DataTransfering { get; private set; }
        public Trit RWState { get; private set; }
        public Int32 CurrentStep { get; private set; }
        public Int32 DataStep { get; private set; }
        
        private Tryte _NameLength;
        private Tryte[] _NameBuffer;
        private Trort _DataLength;
        private Tryte[] _DataBuffer;
        private Tryte _DataAttributes;


        private Dictionary<TernaryString, TernaryFolder> _Folders = new Dictionary<TernaryString, TernaryFolder>();


        public void ReadWriteEnabled(object sender, Trit rwe)
        {

        }


        public void BusInput(object sender, Tryte data)
        {
            if (RWState == Trit.Neg)
                Read(sender, data);
            else if (RWState == Trit.Pos)
                Write(sender, data);
        }


        private void Read(object sender, Tryte data)
        {

        }

        private void Write(object sender, Tryte data)
        {
            if (DataTransfering)
            {
                if (_DataLength == 0)
                {
                    CreateFolder(new TernaryString(_NameBuffer), _DataAttributes);

                    DataTransfering = false;
                    CurrentStep = 0;
                }
                else
                {
                    _DataBuffer[DataStep] = data;

                    DataStep++;

                    if (DataStep == _DataLength)
                    {
                        CreateFile(new TernaryString(_NameBuffer), _DataAttributes, _DataBuffer);

                        DataTransfering = false;
                        CurrentStep = 0;
                    }
                }
            }
            else
            {
                switch (CurrentStep)
                {
                    case 0: _NameLength = data; break;
                    case int cs when (cs < _NameLength + 1): _NameBuffer[cs - 1] = data; break;
                    case int cs when (cs == _NameLength + 1): _DataAttributes = data; break;
                    case int cs when (cs == _NameLength + 2): _DataLength = new Trort(data); break;
                    case int cs when (cs == _NameLength + 3): {
                            for (int i = 0; i < Tryte.NUMBER_OF_TRITS; i++)
                                _DataLength[i + Tryte.NUMBER_OF_TRITS] = data[i];
                            break;
                        }
                    case int cs when (cs > _NameLength + 3):
                        _DataBuffer = new Tryte[_DataLength.ToInt()];
                        DataTransfering = true;
                        DataStep = 0;
                        break;
                }
            }

            CurrentStep++;
        }


        private void CreateFolder(TernaryString fullName, Tryte attr)
        {
            TernaryFolder folder = new TernaryFolder(fullName.Trim('/'), attr);
            
            int index = folder.Name.IndexOf('/');
            if (index > 0 && index != folder.Name.Length - 1)
            {
                TernaryString baseFolderName = folder.Name.Substring('/');
                if (_Folders.ContainsKey(baseFolderName))
                    _Folders[baseFolderName].AddFolder(folder);
                else
                    throw new Exception($"Folder root folder '{baseFolderName}' Not Found");
            }
            else
            {
                _Folders.Add(folder.Name, folder);
            }
        }

        private void CreateFile(TernaryString fullName, Tryte attr, Tryte[] data)
        {

        }

        private void ModifyFile(TernaryString fullName, Tryte attr, Tryte[] data)
        {

        }
    }
}
