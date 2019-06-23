using System.Collections.Generic;
using UnityEngine;

namespace Myth.UI {
	public class GUIManager : MonoBehaviour {
		public enum WindowType {
			Login = 0,
			NewCharacter = 1,
			NewCampaign = 2,
			NewMap = 3,
			PlayerSelection = 4,
			MapSelection = 5,
			CharacterSelection = 6,
			CampaignSelection = 7,
			NewAccount = 8,
			PlayerMapSelection = 9
		}

		public List<GameObject> guiObjects = new List<GameObject>();
		private WindowType currentWindow = WindowType.Login;

        void Start() {
			setActivePanel();
        }

		void Update () {
			setActivePanel();
		}

        private void setActivePanel(){
            if (currentWindow != Globals.Instance().LoginWinType) {
                currentWindow = Globals.Instance().LoginWinType;
                foreach (GameObject comp in guiObjects) {
                    comp.SetActive(false);
                }

                guiObjects[(int)currentWindow].SetActive(true);
                guiObjects[(int)currentWindow].GetComponent<GUICore>().refresh();
            }
        }
	}
}
