using UnityEngine;
using System.Text;
using System.Collections.Generic;

    public class ConfigReader
    {
        byte[] mBuffer;
        int mOffset = 0;

        public ConfigReader(byte[] bytes)
        {
            mBuffer = bytes;
        }

        public ConfigReader(TextAsset asset)
        {
            mBuffer = asset.bytes;
        }

        public bool canRead { get { return (mBuffer != null && mOffset < mBuffer.Length); } }

        static string ReadLine(byte[] buffer, int start, int count)
        {
            return Encoding.UTF8.GetString(buffer, start, count);
        }

        public string ReadLine()
        {
            int max = mBuffer.Length;

            while (mOffset < max && mBuffer[mOffset] < 32)
                ++mOffset;

            int end = mOffset;

            if (end < max)
            {
                for (; ; )
                {
                    if (end < max)      
                    {
                        int ch = mBuffer[end++];
                        if (ch != '\n' && ch != '\r')
                            continue;
                    }
                    else
                        ++end;

                    string line = ReadLine(mBuffer, mOffset, end - mOffset - 1);
                    mOffset = end;
                    return line;
                }
            }
            mOffset = max;
            return null;
        }

        public Dictionary<int, Dictionary<int, string>> ReadDictionary()
        {
            Dictionary<int, Dictionary<int, string>> dict = new Dictionary<int, Dictionary<int, string>>();
            int mainKey = 0;
            int LastMainKey = 0;
            while (canRead)
            {
                string line = ReadLine();
                if (line == null)
                    break;
            if (line.StartsWith("["))
            {
                line.Trim();
                int Start = line.IndexOf("[");
                line = line.Remove(Start, 1);
                int End = line.IndexOf("]");
                line = line.Remove(End, 1);
                dict.Add(int.Parse(line), new Dictionary<int, string>());
                mainKey = int.Parse(line);
            }
            else if (line.StartsWith("#"))
            {
                line = line.Remove(line.IndexOf("#"), 1);
                dict[mainKey].Add(int.Parse(line), "");
                LastMainKey = int.Parse(line);
            }
            else
            {
                dict[mainKey][LastMainKey] = line;
            }
        }
        return dict;
        }
    }