using UnityEngine;
using UnityEngine.UI;

namespace Myth.UI.Login {
	public class PlayerSelectionGUI : GUICore {
        public RectTransform characterButton;
        public RectTransform masterButton;
        public RectTransform logoutButton;

        void Awake(){
            base.Awake();

            tabThroughUIUtil.addUIObject(characterButton.gameObject);
            tabThroughUIUtil.addUIObject(masterButton.gameObject);
            tabThroughUIUtil.addUIObject(logoutButton.gameObject);

            characterButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        Globals.Instance().LoginWinType = GUIManager.WindowType.CharacterSelection;
                    }
            );

            masterButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        Globals.Instance().LoginWinType = GUIManager.WindowType.CampaignSelection;
                    }
            );

            logoutButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        Globals.Instance().LoginWinType = GUIManager.WindowType.Login;
                    }
            );
        }
	}
}
