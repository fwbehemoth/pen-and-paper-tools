using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Myth.UI.Login {
	public class CampaignSelectionGUI : GUICore {
		private CampaignsBusinessObject campaignsBO = new CampaignsBusinessObject();
        public RectTransform backButton;
        public RectTransform createCampaignButton;
        public RectTransform scrollContent;
        public GameObject scrollItem;

        void Awake(){
            base.Awake();

            backButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        Globals.Instance().LoginWinType = GUIManager.WindowType.PlayerSelection;
                    }
            );

            createCampaignButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        Globals.Instance().LoginWinType = GUIManager.WindowType.NewCampaign;
                    }
            );

            tabThroughUIUtil.addUIObject(backButton.gameObject);
            tabThroughUIUtil.addUIObject(createCampaignButton.gameObject);
        }

        void OnEnable(){
            base.OnEnable();
            refresh();
        }

        public override void refresh () {
            clearContent(scrollContent);
			campaignsBO.fetch();
            doRefresh = true;
		}

		void Update () {
            base.Update();
            if(doRefresh) {
                foreach (KeyValuePair<int, CampaignBusinessObject> entry in campaignsBO.collection) {
                    doRefresh = false;
                    GameObject selectionButton = Instantiate(scrollItem);
                    selectionButton.transform.SetParent(scrollContent, false);
                    selectionButton.GetComponent<Button>().onClick.AddListener(
                            delegate  {
                                Globals.Instance().userBO.model.campaignBO = entry.Value;
                                Globals.Instance().LoginWinType = GUIManager.WindowType.MapSelection;
                            }
                    );

                    selectionButton.GetComponentInChildren<Text>().text = entry.Value.model.name;
                    tabThroughUIUtil.addUIObject(selectionButton.gameObject);
                }
            }
		}
	}
}
