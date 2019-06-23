using Constants;
using Myth.Constants;
using Myth.Utils;
using UnityEngine;

namespace Myth.Engine {
    public class EditGrid : MonoBehaviour{

        public int numRows = MapEditorConstants.EDIT_GRID_DEFAULT_ROW;
        public int numCols = MapEditorConstants.EDIT_GRID_DEFAULT_COLUMN;
        private int originalNumRows = 0;
        private int originalNumCols = 0;
        private static Material lineMaterial;
	    private static Material gridMaterial;
        private float tileSize = 0.6f;
	    public bool isAdd = true;

        void OnPostRender() {
            if (!lineMaterial) {
                lineMaterial = MaterialUtil.CreateLineMaterial(Colors.lineColor);
            }
            lineMaterial.SetPass(0);

	        if (!gridMaterial) {
		        gridMaterial = MaterialUtil.CreateGridMaterial(Colors.gridColor);
	        }

            tileSize = GameObjectUtils.getTileBoundsSize();

            GL.PushMatrix();
            GL.Begin(GL.LINES);
            if (numRows > 0 && numCols > 0) {
                for (int i = 0; i <= numCols; i++) {
                    GL.Color(Colors.lineColor);
                    GL.Vertex3(0, tileSize * i, 0);
                    GL.Vertex3(tileSize * numRows, tileSize * i, 0);
                }

                for (int j = 0; j <= numRows; j++) {
                    GL.Color(Colors.lineColor);
                    GL.Vertex3(tileSize * j, 0, 0);
                    GL.Vertex3(tileSize * j, tileSize * numCols, 0);
                }

                if (originalNumCols != numCols || originalNumRows != numRows) {
                    originalNumRows = numRows;
                    originalNumCols = numCols;
                }

                if (originalNumCols > numCols || originalNumRows > numRows) {
                    isAdd = false;
                } else {
                    isAdd = true;
                }
            }
            GL.End();
            GL.PopMatrix();
        }

        public Vector3 WorldToGridCoordinates(Vector3 point){
            Vector3 gridPoint = new Vector3((int)((point.x - transform.position.x) / tileSize) , (int)((point.y - transform.position.y) / tileSize), 0.0f);
            return gridPoint;
        }

        public Vector3 GridToWorldCoordinates(int col, int row){
            Vector3 worldPoint = new Vector3(transform.position.x + (col * tileSize + tileSize / 2.0f), transform.position.y + (row * tileSize + tileSize / 2.0f), 0.0f);
            return worldPoint;
        }

        public bool IsInsideGridBounds(Vector3 point){
            float minX = transform.position.x;
            float maxX = minX + numCols * tileSize;
            float minY = transform.position.y;
            float maxY = minY + numRows * tileSize;
            return (point.x >= minX && point.x <= maxX && point.y >= minY && point.y <= maxY);
        }

        public bool IsInsideGridBounds(int col, int row){
            return (col >= 0 && col < numCols && row >= 0 && row < numRows);
        }

//        void OnDrawGizmos(){
//            if(numRows > 0 && numCols > 0) {
//                for (int i = 0; i <= numRows; i++) {
//                    Gizmos.DrawLine(new Vector3(0, tileSize * i, 0), new Vector3(tileSize * numCols, tileSize * i, 0));
//                }
//
//                for (int j = 0; j <= numCols; j++) {
//                    Gizmos.DrawLine(new Vector3(tileSize * j, 0, 0), new Vector3(tileSize * j, tileSize * numRows, 0));
//                }
//            }
//        }
    }
}