using System;
using Myth.BaseLib;
using UnityEngine;

[Serializable]
public class TileBusinessObject : MythBussinessObject {
    public TileData model = new TileData();

    public TileBusinessObject(){
        
    }

	public TileBusinessObject(int id, string name){
		model.id = id;
		model.name = name;
	}

    public void setTileIndexes(int rowIndex, int columnIndex){
        model.rowIndex = rowIndex;
        model.columnIndex = columnIndex;
    }

    public void updateData(TileData tiledata){
        model.id = tiledata.id;
        model.name = tiledata.name;
        model.rowIndex = tiledata.rowIndex;
        model.columnIndex = tiledata.columnIndex;
        model.centerX = tiledata.centerX;
        model.centerY = tiledata.centerY;
        model.prefabName = tiledata.prefabName;
    }

    public void updateNamesAndID(TileData tiledata){
        model.id = tiledata.id;
        model.name = tiledata.name;
        model.prefabName = tiledata.prefabName;
    }
}
