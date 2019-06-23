using UnityEngine;
using UnityEngine.UI;

namespace Myth.UI.Login {
	public class NewCampaignGUI : GUICore {
		private string campName = "";
		private string campGame = "";
		private string campSet = "";
        public RectTransform backButton;
        public RectTransform createCampaignButton;
        public RectTransform campaignNameInput;
        public RectTransform campaignGameInput;
        public RectTransform campaignSettingInput;

        void Awake(){
            base.Awake();

            tabThroughUIUtil.addUIObject(campaignNameInput.gameObject);
            tabThroughUIUtil.addUIObject(campaignGameInput.gameObject);
            tabThroughUIUtil.addUIObject(campaignSettingInput.gameObject);
            tabThroughUIUtil.addUIObject(backButton.gameObject);
            tabThroughUIUtil.addUIObject(createCampaignButton.gameObject);

            backButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        Globals.Instance().LoginWinType = GUIManager.WindowType.CampaignSelection;
                    }
            );

            createCampaignButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        createCampaign();
                        Globals.Instance().LoginWinType = GUIManager.WindowType.CampaignSelection;
                    }
            );
        }

		void clearFields() {
			campaignNameInput.gameObject.GetComponentInChildren<InputField>().text = "";
            campaignGameInput.gameObject.GetComponentInChildren<InputField>().text = "";
            campaignSettingInput.gameObject.GetComponentInChildren<InputField>().text = "";
		}

		//Create
		void createCampaign() {
            campName = campaignNameInput.gameObject.GetComponentInChildren<InputField>().text;
            campGame = campaignGameInput.gameObject.GetComponentInChildren<InputField>().text;
            campSet = campaignSettingInput.gameObject.GetComponentInChildren<InputField>().text;

			CampaignBusinessObject campaignBusinessObject = new CampaignBusinessObject(campName, campGame, campSet);
			campaignBusinessObject.save();
		}
	}
}
