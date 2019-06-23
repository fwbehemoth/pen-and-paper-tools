using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Myth.UI.Login {
    public class PlayerMapSelectionGUI : GUICore {
        private MapsBusinessObject mapsBO = new MapsBusinessObject();
        public RectTransform backButton;
        public RectTransform scrollContent;
        public GameObject scrollItem;

        void Awake(){
            base.Awake();

            backButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        Globals.Instance().LoginWinType = GUIManager.WindowType.CharacterSelection;
                    }
            );

            tabThroughUIUtil.addUIObject(backButton.gameObject);
        }

        void OnEnable(){
            base.OnEnable();
            refresh();
        }

        void Update () {
            base.Update();

            if(doRefresh) {
                if (mapsBO.collection.Count > 0) {
                    foreach (KeyValuePair<int, MapBusinessObject> entry in mapsBO.collection) {
                        doRefresh = false;
                        GameObject selectionButton = Instantiate(scrollItem);
                        selectionButton.transform.SetParent(scrollContent, false);
                        selectionButton.GetComponent<Button>().onClick.AddListener(
                                delegate  {
                                    Globals.Instance().userBO.model.mapBO = entry.Value;
                                    SceneManager.LoadScene("LoadMap");
                                }
                        );

                        selectionButton.GetComponentInChildren<Text>().text = entry.Value.model.name;
                        tabThroughUIUtil.addUIObject(selectionButton.gameObject);
                    }
                } else {
                    refresh();
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
