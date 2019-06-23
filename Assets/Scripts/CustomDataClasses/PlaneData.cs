using System;
using Myth.BaseLib;
using Myth.Controllers;

namespace Myth.CustomDataClasses {
    [Serializable]
    public class PlaneData:MythDataObject {
        public float centerX;
        public float centerY;
        public PlaneController topPlane;
        public PlaneController bottomPlane;
        public PlaneController leftPlane;
        public PlaneController rightPlane;
    }
}