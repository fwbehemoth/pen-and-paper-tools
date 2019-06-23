using UnityEngine;
using Utils;

namespace Myth.UI {
	public class GUICore : MonoBehaviour {
        public RectTransform panel;
		protected GUISkin skin;
        protected bool doRefresh = false;
        protected TabThroughUIUtil tabThroughUIUtil;

		protected void Awake() {
            tabThroughUIUtil = new TabThroughUIUtil();
		}

		// Use this for initialization
		protected void Start () {
            GameObject globals = GameObject.Find("Globals");
            if (globals == null) {
                Vector3 pos = new Vector3(0, 0, 0);
                Quaternion rot = Quaternion.Euler(0, 0, 0);
                GameObject obj = (GameObject)Resources.Load("Globals/Globals");
                globals = Instantiate(obj, pos, rot) as GameObject;
                globals.name = obj.name;
            }
            skin = Globals.Instance().skin;
            Camera.main.backgroundColor = Color.black;
		}

        protected void OnEnable() {
            tabThroughUIUtil.setSelectedObject(0);
        }

		// Update is called once per frame
		protected void Update () {
            if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
                if(Input.GetKeyDown(KeyCode.Tab)) {
                    tabThroughUIUtil.previousUIObject();
                }
            } else if(Input.GetKeyDown(KeyCode.Tab)) {
                tabThroughUIUtil.nextUIObject();
            }
		}

		//Refesh List
		public virtual void refresh() {

		}

        public virtual void setActivePanel(){
            panel.gameObject.SetActive(true);
        }

        public virtual void clearContent(RectTransform content){
            foreach (Transform child in content.transform){
                Destroy(child.gameObject);
            }
        }
	}
}
