using System;
using System.Collections.Generic;

namespace Utils {
    [Serializable]
    public class JSONArrayWrapper<T> {
        public int rows = 0;
        public int columns = 0;
        public List<T> list = new List<T>();
    }
}