using System;
using Myth.BaseLib;

[Serializable]
public class TilesBusinessObject : MythBussinessObject {
    public TileCollection collection = new TileCollection();

    public void Add(TileBusinessObject tileBO) {
        collection.Add(tileBO.model.id, tileBO);
    }

    public void addCollecttion() {

    }

    public void addCollection(TileCollection collection) {
    	
    }
}
