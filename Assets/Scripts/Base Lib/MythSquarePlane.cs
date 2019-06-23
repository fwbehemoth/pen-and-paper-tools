using UnityEngine;

namespace Myth.BaseLib {
    public static class MythSquarePlane {
        public static GameObject CreatePlane(float tilesize, int xIndex, int yIndex, Material material = null, bool collider = true) {
            GameObject plane = new GameObject("Plane");
            MeshFilter planeFilter = plane.AddComponent(typeof(MeshFilter)) as MeshFilter;
            MeshRenderer planeRenderer = plane.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
            Mesh planeMesh = new Mesh();
            float originX = xIndex * tilesize;
            float originY = yIndex * tilesize;
            float finalX = (xIndex + 1) * tilesize;
            float finalY = (yIndex + 1) * tilesize;

            planeMesh.vertices = new Vector3[]{
                new Vector3(originX, originY, 0),
                new Vector3(finalX, originY, 0),
                new Vector3(finalX, finalY, 0),
                new Vector3(originX, finalY, 0)
            };

            planeMesh.uv = new Vector2[]{
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(1, 0)
            };

            planeMesh.triangles = new int[]{0, 3, 2, 2, 1, 0};
            planeMesh.normals = planeMesh.vertices;
            planeFilter.mesh = planeMesh;

            if (collider) {
                (plane.AddComponent(typeof(MeshCollider)) as MeshCollider).sharedMesh = planeMesh ;
            }

            if (material) {
                planeRenderer.material = material;
            }

            planeMesh.RecalculateBounds();
            planeMesh.RecalculateNormals();

            TileController tileController = plane.AddComponent(typeof(TileController)) as TileController;
            Vector3 center = centerPoint(originX, originY, finalX, finalY);
            TileBusinessObject tileBusinessObject = new TileBusinessObject();
            tileBusinessObject.model.centerY = center.y;
            tileBusinessObject.model.centerX = center.x;
            tileBusinessObject.model.rowIndex = xIndex;
            tileBusinessObject.model.columnIndex = yIndex;
            tileController.tileBO = tileBusinessObject;
 //           Globals.Instance().userBO.model.mapBO.mapTilesBO.Add(tileController);
            plane.tag = "Plane";
	        plane.gameObject.layer = 9;
            return plane;
        }

        private static Vector3 centerPoint(float originX, float originY, float finalX, float finalY){
            float diffX = (finalX - originX) / 2;
            float diffY = (finalY - originY) / 2;
            float centerX = originX + diffX;
            float centerY = originY + diffY;

            return new Vector3(centerX, centerY, 0);
        }
    }
}