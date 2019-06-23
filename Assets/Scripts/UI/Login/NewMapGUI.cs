using UnityEngine;
using UnityEngine.UI;

namespace Myth.UI.Login {
	public class NewMapGUI : GUICore {
        public RectTransform backButton;
        public RectTransform createMapButton;
        public RectTransform mapNameInput;

        void Awake() {
            base.Awake();

            tabThroughUIUtil.addUIObject(mapNameInput.gameObject);
            tabThroughUIUtil.addUIObject(backButton.gameObject);
            tabThroughUIUtil.addUIObject(createMapButton.gameObject);

            backButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        Globals.Instance().LoginWinType = GUIManager.WindowType.MapSelection;
                    }
            );

            createMapButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        createMap(mapNameInput.gameObject.GetComponentInChildren<InputField>().text);
                        Globals.Instance().LoginWinType = GUIManager.WindowType.MapSelection;
                    }
            );
        }

		void clearFields() {
			mapNameInput.gameObject.GetComponentInChildren<InputField>().text = "";
		}

		void createMap(string mapName) {
            MapBusinessObject mapBO = new MapBusinessObject(mapName);
            mapBO.save();
		}
	}
}
