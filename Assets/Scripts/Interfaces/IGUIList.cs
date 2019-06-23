using System.Collections.Generic;
using UnityEngine;

namespace Myth.Interfaces {
    public interface IGUIList {
        int getLength();
        Dictionary<int, IGUIData> getCollection();
    }
}