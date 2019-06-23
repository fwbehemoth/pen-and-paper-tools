using UnityEngine;

namespace Myth.Utils {
	public class TransformUtils {
		public static Vector3 backwards(Transform transform){
			return -transform.forward;
		}
	}
}