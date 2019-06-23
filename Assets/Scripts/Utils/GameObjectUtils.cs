using UnityEngine;

namespace Myth.Utils {
	public class GameObjectUtils {
		public static float getTileBoundsSize() {
			GameObject tile = (GameObject)Resources.Load("Tiles/Blank_Tile");
			MeshFilter filter = (MeshFilter)tile.transform.GetChild(0).GetComponent(typeof(MeshFilter));
			Bounds bounds = filter.sharedMesh.bounds;
			return bounds.size.x;
		}
	}
}
