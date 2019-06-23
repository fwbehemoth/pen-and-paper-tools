using System;
using Myth.BaseLib;

[Serializable]
public class PiecesBusinessObject : MythBussinessObject {
    public PieceCollection collection = new PieceCollection();

	public void Add(PieceBusinessObject pieceBO) {
		collection.Add(pieceBO.model.id, pieceBO);
	}
	
	public void addCollecttion() {
		
	}
	
	public void addCollection(TileCollection collection) {
		
	}
}
