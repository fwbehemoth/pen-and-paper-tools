using Myth.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Login {
	public class LoginGUI : GUICore {
		private string username = "";
		private string password = "";
		public RectTransform loginButton;
        public RectTransform createButton;
		public RectTransform usernameInput;
		public RectTransform passwordInput;
		public RectTransform errorText;

        void Awake(){
            base.Awake();

            tabThroughUIUtil.addUIObject(usernameInput.gameObject);
            tabThroughUIUtil.addUIObject(passwordInput.gameObject);
            tabThroughUIUtil.addUIObject(loginButton.gameObject);
            tabThroughUIUtil.addUIObject(createButton.gameObject);

			loginButton.GetComponent<Button>().onClick.AddListener(
				delegate  {
					login();
				}
            );

            createButton.GetComponent<Button>().onClick.AddListener(
				delegate  {
					createAccount();
				}
            );
        }

		private void login() {
            errorText.GetComponent<Text>().text = "";
			username = usernameInput.gameObject.GetComponent<InputField>().text;
            password = passwordInput.gameObject.GetComponent<InputField>().text;
			Globals.Instance().userBO.setUsernameAndPassword(username, password);
			Globals.Instance().userBO.fetch();
		}

        private void createAccount(){
            Globals.Instance().LoginWinType = GUIManager.WindowType.NewAccount;
        }

		void invalidLoginTrue() {
            errorText.GetComponent<Text>().text = "Invalid Username and/or Password";
		}
	}
}
