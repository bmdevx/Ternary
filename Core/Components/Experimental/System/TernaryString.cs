using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ternary.Components.Experimental.System
{
    public class TernaryString : IComparable, ICloneable, IEnumerable,
        IComparable<TernaryString>, IEnumerable<Tryte>, IEquatable<TernaryString>,
        IComparable<String>, IEnumerable<char>, IEquatable<String>
    {
        private List<Tryte> _Chars;


        public int Length => _Chars.Count;


        public TernaryString()
        {
            _Chars = new List<Tryte>();
        }

        public TernaryString(TernaryString tstring)
        {
            _Chars = new List<Tryte>(tstring);
        }

        public TernaryString(string @string) : this(@string.ToCharArray()) {  }

        public TernaryString(Tryte[] chars)
        {
            _Chars = new List<Tryte>(chars);
        }

        public TernaryString(char[] chars) : this(chars.Select(c => new Tryte(c.ToTryte()))) { }

        public TernaryString(IEnumerable<Tryte> chars)
        {
            _Chars = new List<Tryte>(chars);
        }

        public TernaryString(IEnumerable<char> chars) : this(chars.Select(c => new Tryte(c.ToTryte()))) { }





        public int IndexOf(Tryte @char)
        {
            return _Chars.IndexOf(@char);
        }

        public int IndexOf(char @char)
        {
            return IndexOf(@char.ToTryte());
        }


        public int LastIndexOf(Tryte @char)
        {
            return _Chars.LastIndexOf(@char);
        }

        public int LastIndexOf(char @char)
        {
            return LastIndexOf(@char.ToTryte());
        }

        public bool Contains(Tryte @char)
        {
            return _Chars.Any(c => c == @char);
        }

        public bool Contains(char @char)
        {
            return Contains(@char.ToTryte());
        }

        public TernaryString Substring(int index, int length = 0)
        {
            return new TernaryString(_Chars.Skip(index).Take(length < 1 ? _Chars.Count : length));
        }
        
        public TernaryString Substring(Tryte index)
        {
            return new TernaryString(_Chars.Skip(index.ToInt()));
        }

        public TernaryString Substring(Tryte index, Tryte length)
        {
            int len = length.ToInt();
            return new TernaryString(_Chars.Skip(index.ToInt()).Take(len));
        }


        public TernaryString Trim(Tryte @char)
        {
            int startIndex = 0;
            for (; startIndex < _Chars.Count; startIndex++)
            {
                if (_Chars[startIndex] != @char)
                    break;
            }

            int endIndex = _Chars.Count;
            for (; endIndex > 0; endIndex--)
            {
                if (_Chars[endIndex] != @char)
                    break;
            }

            return Substring(startIndex, endIndex - startIndex);
        }

        public TernaryString Trim(char @char = ' ')
        {
            return Trim(@char.ToTryte());
        }



        public object Clone()
        {
            return new TernaryString(this);
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(TernaryString other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(string other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(TernaryString other)
        {
            return _Chars.SequenceEqual(other._Chars);
        }

        public bool Equals(string other)
        {
            return _Chars.SequenceEqual(other.Select(c => new Tryte(c - Tryte.MAX_INT_VALUE)));
        }

        public IEnumerator<Tryte> GetEnumerator()
        {
            foreach (Tryte tryte in _Chars)
                yield return tryte;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<char> IEnumerable<char>.GetEnumerator()
        {
            foreach (Tryte tryte in _Chars)
                yield return tryte.ToChar();
        }

        public override string ToString()
        {
            return new string(_Chars.Select(c => c.ToChar()).ToArray());
        }
    }
}
