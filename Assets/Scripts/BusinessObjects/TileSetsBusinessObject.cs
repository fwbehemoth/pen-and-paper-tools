using System;
using Enums;
using LitJson;
using Myth.BaseLib;
using UnityEngine;

[Serializable]
public class TileSetsBusinessObject:MythBussinessObject {
    public TileSetCollection collection = new TileSetCollection();

    public override void fetch() {
		Globals.Instance().api.apiCall("FetchUserTileSets.php?userid=" + Globals.Instance().userBO.model.id, RequestMethodTypeEnum.Get, success, failure);
    }

    protected override void success(JsonData json) {
        collection.Clear();
        int count = int.Parse(json["count"].ToString());
        for(int i = 0; i < count; i++) {
            int tilecnt = int.Parse(json[i]["TilesCount"].ToString());
			TilesBusinessObject tilesBO = new TilesBusinessObject();
			PiecesBusinessObject piecesBO = new PiecesBusinessObject();
            for(int j = 0; j < tilecnt; j++) {
                if(json[i]["Tiles"][j]["Type"].ToString() == "Tile") {
					TileBusinessObject tileBO = new TileBusinessObject(
                            int.Parse(json[i]["Tiles"][j]["Id"].ToString()),
                            json[i]["Tiles"][j]["Name"].ToString());
                    /*for(int x = 0; x < json[i]["Tiles"][j]["XDim"].Count; x++){
						tile.xDimension[x] = json[i]["Tiles"][j]["XDim"][x].ToString();
					}
					for(int y = 0; y < json[i]["Tiles"][j]["YDim"].Count; y++){
						tile.yDimension[y] = json[i]["Tiles"][j]["YDim"][y].ToString();
					}*/
                    tilesBO.Add(tileBO);
                } else {
					PieceBusinessObject pieceBO = new PieceBusinessObject(
                            int.Parse(json[i]["Tiles"][j]["Id"].ToString()),
                            json[i]["Tiles"][j]["Name"].ToString());
                    /*for(int x = 0; x < json[i]["Tiles"][j]["XDim"].Count; x++){
						piece.xDimension[x] = json[i]["Tiles"][j]["XDim"][x].ToString();
					}
					for(int y = 0; y < json[i]["Tiles"][j]["YDim"].Count; y++){
						piece.yDimension[y] = json[i]["Tiles"][j]["YDim"][y].ToString();
					}*/
                    //Debug.Log(piece.name);
					piecesBO.Add(pieceBO);
                }
            }
			TileSetBusinessObject tileSetBO = new TileSetBusinessObject(
                    int.Parse(json[i]["Id"].ToString()),
                    json[i]["Name"].ToString(), tilesBO, piecesBO);
			Add (tileSetBO);
        }

        GameObject guiObj = GameObject.Find("Engine");
        guiObj.SendMessage("enableMapEditorGUI");
    }

	public void Add(TileSetBusinessObject tileSetBO){
		collection.Add(tileSetBO.model.id, tileSetBO);
	}
}
