using UnityEngine;
using UnityEngine.EventSystems;

namespace Myth.UI.Lib {
	public class MythDragPanel : MonoBehaviour, IPointerDownHandler, IDragHandler {

		private Vector2 pointerOffset;
		private RectTransform canvasRectTransform;
		private RectTransform panelRectTransform;

		void Awake () {
			Canvas canvas = GetComponentInParent <Canvas>();
			if (canvas != null) {
				canvasRectTransform = canvas.transform as RectTransform;
				panelRectTransform = transform.parent as RectTransform;
			}
		}

		public void OnPointerDown (PointerEventData data) {
			panelRectTransform.SetAsLastSibling ();
			RectTransformUtility.ScreenPointToLocalPointInRectangle (panelRectTransform, data.position, data.pressEventCamera, out pointerOffset);
		}

		public void OnDrag (PointerEventData data) {
			if (panelRectTransform == null)
				return;

			Vector2 pointerPostion = ClampToWindow (data);

			Vector2 localPointerPosition;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle (canvasRectTransform, pointerPostion, data.pressEventCamera, out localPointerPosition)) {
				panelRectTransform.localPosition = localPointerPosition - pointerOffset;
			}
		}

		Vector2 ClampToWindow (PointerEventData data) {
			Vector2 rawPointerPosition = data.position;

			Vector3[] canvasCorners = new Vector3[4];
			canvasRectTransform.GetWorldCorners (canvasCorners);
			Vector3 pos = panelRectTransform.localPosition;

			Vector3 minPosition = canvasRectTransform.rect.min - panelRectTransform.rect.min;
			Vector3 maxPosition = canvasRectTransform.rect.max - panelRectTransform.rect.max;

//			Debug.Log("World Corners: " + canvasCorners[0] + ", " + canvasCorners[1] + ", " + canvasCorners[2] + ", " + canvasCorners[3]);

			float clampedX = Mathf.Clamp (rawPointerPosition.x, canvasCorners[0].x, canvasCorners[2].x);
			float clampedY = Mathf.Clamp (rawPointerPosition.y, canvasCorners[0].y, canvasCorners[2].y);

//			float clampedX = Mathf.Clamp (rawPointerPosition.x, minPosition.x, maxPosition.x);
//			float clampedY = Mathf.Clamp (rawPointerPosition.y, maxPosition.y, maxPosition.y);

			Vector2 newPointerPosition = new Vector2 (clampedX, clampedY);
//			return newPointerPosition;
			return data.position;
		}
	}
}