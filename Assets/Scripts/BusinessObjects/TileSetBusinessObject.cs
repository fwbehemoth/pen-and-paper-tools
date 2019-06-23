using System;
using Myth.BaseLib;

[Serializable]
public class TileSetBusinessObject : MythBussinessObject {
    public TileSetData model = new TileSetData();

	public TileSetBusinessObject(int id, string name, TilesBusinessObject tilesBO, PiecesBusinessObject piecesBO){
		model.id = id;
		model.name = name;
		model.tilesBO = tilesBO;
		model.piecesBO = piecesBO;
	}
}
