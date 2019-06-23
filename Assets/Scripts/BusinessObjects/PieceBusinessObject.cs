using System;
using Myth.BaseLib;

[Serializable]
public class PieceBusinessObject:MythBussinessObject {
    public PieceData model = new PieceData();

    public PieceBusinessObject(){

    }

	public PieceBusinessObject(int id, string name){
		model.id = id;
		model.name = name;
	}

	public void updateData(PieceData pieceData){
		model.id = pieceData.id;
		model.name = pieceData.name;
		model.prefabName = pieceData.prefabName;
		model.rotation = pieceData.rotation;
	}
}
