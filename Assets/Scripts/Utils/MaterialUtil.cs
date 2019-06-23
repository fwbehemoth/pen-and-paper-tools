using UnityEngine;

namespace Myth.Utils {
	public class MaterialUtil {
		public static Material CreateLineMaterial(Color color){
			Material lineMaterial;
			var shader = Shader.Find("Hidden/Internal-Colored");
			lineMaterial = new Material(shader);
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
			lineMaterial.SetInt("_ZWrite", 0);
			lineMaterial.SetColor("_EmissionColor", color);
			return lineMaterial;
		}

		public static Material CreateGridMaterial(Color color){
			Material gridMaterial;
			var shader = Shader.Find("Hidden/Internal-Colored");
			gridMaterial = new Material(shader);
			gridMaterial.hideFlags = HideFlags.HideAndDontSave;
			gridMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			gridMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			gridMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
			gridMaterial.SetInt("_ZWrite", 0);
			gridMaterial.SetColor("_Color", color);
            gridMaterial.SetColor("_MainTex", color);
			return gridMaterial;
		}
	}
}