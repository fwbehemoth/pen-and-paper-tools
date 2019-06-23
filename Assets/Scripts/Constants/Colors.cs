using UnityEngine;

namespace Myth.Constants {
	public class Colors {
		public static Color lineColor = Color.cyan;
		public static Color gridColor = Color.gray;

		public static Color moveSelectionColor(Color originalColor){
			return new Color(originalColor.r, originalColor.g, originalColor.b * 3);
		}
	}
}