using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Myth.UI.Login {
	public class CharacterSelectionGUI : GUICore {
		private CharactersBusinessObject charactersBO = new CharactersBusinessObject();
        public RectTransform backButton;
        public RectTransform createCharacterButton;
        public RectTransform scrollContent;
        public GameObject scrollItem;

        void Awake(){
            base.Awake();

            backButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        Globals.Instance().LoginWinType = GUIManager.WindowType.PlayerSelection;
                    }
            );

            createCharacterButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        Globals.Instance().LoginWinType = GUIManager.WindowType.NewCharacter;
                    }
            );

            tabThroughUIUtil.addUIObject(backButton.gameObject);
            tabThroughUIUtil.addUIObject(createCharacterButton.gameObject);
        }

        void OnEnable(){
            base.OnEnable();
            refresh();
        }

		void Update () {
            base.Update();

            if(doRefresh) {
				foreach (KeyValuePair<int, CharacterBusinessObject> entry in charactersBO.collection) {
                    doRefresh = false;
					GameObject selectionButton = Instantiate(scrollItem);
                    selectionButton.transform.SetParent(scrollContent, false);
					selectionButton.GetComponent<Button>().onClick.AddListener(
							delegate  {
								Globals.Instance().userBO.model.characterBO = entry.Value;
                                CampaignBusinessObject campaignBusinessObject = new CampaignBusinessObject();
                                campaignBusinessObject.fetch();
                                Globals.Instance().userBO.model.campaignBO = campaignBusinessObject;
                                Globals.Instance().LoginWinType = GUIManager.WindowType.PlayerMapSelection;
							}
					);

                    selectionButton.GetComponentInChildren<Text>().text = entry.Value.model.name;
                    tabThroughUIUtil.addUIObject(selectionButton.gameObject);
				}
			}
		}

		public override void refresh () {
            clearContent(scrollContent);
			charactersBO.fetch();
            doRefresh = true;
		}
	}
}