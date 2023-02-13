using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YandexCloud.DataStreams
{
    public class RecordsSizeException : Exception
    {
        internal const long MAX_MESSAGES_SIZE = 5_242_880;

        public long Size { get; }
        public RecordsSizeException(string message, long size)
          : base(message) 
        {
            Size = size;
        }

        public static void Ensure(int size)
        {
            if (size > MAX_MESSAGES_SIZE)
            {
                throw new RecordsSizeException($"The collection of records is large. The size of collection is equal {size}", size);
            }
        }
        
    }

    public class RecordSizeException : Exception
    {
        internal const long MAX_MESSAGE_SIZE = 1_048_576;
   
        public long Size { get; }
        public int Index { get; }
        public RecordSizeException(string message, long size, int index)
          : base(message)
        {
            Size = size;
            Index = index;
        }

        public static void Ensure(int size, int index)
        {
            if (size > MAX_MESSAGE_SIZE)
            {
                throw new RecordSizeException($"The record is large. The size of record is equal {size}", size, index);
            }
        }

    }
}
