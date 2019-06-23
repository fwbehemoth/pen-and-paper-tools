using UnityEngine;
using UnityEngine.UI;

namespace Myth.UI.Login {
	public class NewAccountGUI : GUICore {
		private string username = "";
		private string password = "";
        public RectTransform backButton;
        public RectTransform createButton;
        public RectTransform usernameInput;
        public RectTransform passwordInput;
        public RectTransform errorText;

        void Awake(){
            base.Awake();

            tabThroughUIUtil.addUIObject(usernameInput.gameObject);
            tabThroughUIUtil.addUIObject(passwordInput.gameObject);
            tabThroughUIUtil.addUIObject(backButton.gameObject);
            tabThroughUIUtil.addUIObject(createButton.gameObject);

            backButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        backToLogin();
                    }
            );

            createButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        checkNames();
                    }
            );
        }

		private void checkNames() {
            username = usernameInput.gameObject.GetComponentInChildren<InputField>().text;
            password = passwordInput.gameObject.GetComponentInChildren<InputField>().text;

			Globals.Instance().userBO.setUsernameAndPassword(username, password);
			Globals.Instance().userBO.checkName();
		}

		private void nameDoesExist() {
			errorText.GetComponent<Text>().text = "Username already exists. Please choose another.";
		}

		private void nameDoesNotExist() {
            errorText.GetComponent<Text>().text = "Account Created. Please Log In.";
			Globals.Instance().userBO.save();
		}

        private void backToLogin(){
            Globals.Instance().LoginWinType = GUIManager.WindowType.Login;
        }
	}
}
