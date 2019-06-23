using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Myth.UI.Login {
	public class MapSelectionGUI : GUICore {
		private MapsBusinessObject mapsBO = new MapsBusinessObject();
        public RectTransform backButton;
        public RectTransform createMapButton;
        public RectTransform scrollContent;
        public GameObject scrollItem;

        void Awake(){
            base.Awake();

            backButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        Globals.Instance().LoginWinType = GUIManager.WindowType.CampaignSelection;
                    }
            );

            createMapButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        Globals.Instance().LoginWinType = GUIManager.WindowType.NewMap;
                    }
            );

            tabThroughUIUtil.addUIObject(backButton.gameObject);
            tabThroughUIUtil.addUIObject(createMapButton.gameObject);
        }

        void OnEnable(){
            base.OnEnable();
            refresh();
        }

		void Update () {
            base.Update();

            if(doRefresh) {
                foreach (KeyValuePair<int, MapBusinessObject> entry in mapsBO.collection) {
                    doRefresh = false;
                    GameObject selectionButton = Instantiate(scrollItem);
                    selectionButton.transform.SetParent(scrollContent, false);
                    selectionButton.GetComponent<Button>().onClick.AddListener(
                            delegate  {
                                Globals.Instance().userBO.model.mapBO = entry.Value;
                                SceneManager.LoadScene("EditMap");
                            }
                    );

                    selectionButton.GetComponentInChildren<Text>().text = entry.Value.model.name;
                    tabThroughUIUtil.addUIObject(selectionButton.gameObject);
                }
            }
		}

		public override void refresh() {
            clearContent(scrollContent);
			mapsBO.fetch();
            doRefresh = true;
		}
	}
}
