using System.Collections.Generic;
using Myth.UI.Lib;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Myth.UI {
	public class MapGUI : GUICore {
		public bool inGUI = false;
		private Rect diceRect = new Rect(0, 100, 200, 220);
		private Rect logRect = new Rect(250, 20, 600, 200);
		private Vector2 logScrollPos = Vector2.zero;
		private Rect debugRect = new Rect(250, 500, 400, 300);
		private Vector2 debugScrollPos = Vector2.zero;
		private Rect scrollRect = new Rect(5, 20, 590, 180);

		private bool newLog = false;
		public bool newDebug = false;
//		private Dice dice;
		private string amtText = "1";
		private string modText = "0";
		private string resText = "";
		private string log = "";
		public string debugStr = "";
		private float logMaxHght;
		private bool showDice = false;
		private int diceEntry = 0;
		private GUIContent[] listofDice;
		public GUISkin listSkin;
		private GUIStyle logStyle = new GUIStyle();
		private GUIContent[] listofType;

		private bool showType = false;
		private int rollEntry = 0;

		public Character_Controller cc;

		public Dropdown rollTypeDropdown;
        public Dropdown diceTypeDropdown;
        public RectTransform amountInput;
        public RectTransform modifierInput;
        public RectTransform rollButton;
        public RectTransform resultScrollContent;
        public RectTransform logScrollContent;
        public RectTransform quitButton;
        public GameObject resultScrollItem;
        public GameObject logScrollItem;

        void Awake(){
            base.Awake();
            quitButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        SceneManager.LoadScene("Login");
                    }
            );

            rollButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        diceRollResult();
                    }
            );

            foreach (string rollType in Dice.RollType()){
                rollTypeDropdown.options.Add(new Dropdown.OptionData(rollType));
            }

            foreach (Dice.DiceType diceType in System.Enum.GetValues(typeof(Dice.DiceType))) {
                diceTypeDropdown.options.Add(new Dropdown.OptionData(diceType.ToString()));
            }

            rollTypeDropdown.onValueChanged.AddListener(
                    delegate(int arg0) {
                        rollTypeDropdown.Hide();
                        rollEntry = rollTypeDropdown.value;
                    }
            );
            rollTypeDropdown.RefreshShownValue();

            diceTypeDropdown.onValueChanged.AddListener(
                    delegate(int arg0) {
                        diceTypeDropdown.Hide();
                        diceEntry = diceTypeDropdown.value;
                    }
            );
            diceTypeDropdown.RefreshShownValue();
        }

		// Use this for initialization
		void Start () {
//			dice = this.gameObject.GetComponent<Dice>();
            base.Start();
            resultScrollContent.parent.GetComponent<RectTransform>().anchorMin = new Vector2(0,0);
            resultScrollContent.parent.GetComponent<RectTransform>().anchorMax = new Vector2(1,1);
            logScrollContent.parent.GetComponent<RectTransform>().anchorMin = new Vector2(0,0);
            logScrollContent.parent.GetComponent<RectTransform>().anchorMax = new Vector2(1,1);
            logScrollContent.parent.gameObject.SetActive(true);
			listofDice = new GUIContent[System.Enum.GetValues(typeof(Dice.DiceType)).Length];
			int i = 0;
			foreach (Dice.DiceType type in System.Enum.GetValues(typeof(Dice.DiceType))) {
				listofDice[i] = new GUIContent(type.ToString());
				i++;
			}

			listofType = new GUIContent[4];
			listofType[0] = new GUIContent("Initiative");
			listofType[1] = new GUIContent("Attack");
			listofType[2] = new GUIContent("Damage");
			listofType[3] = new GUIContent("Skill");
		}

		// Update is called once per frame
		void OnGUI () {
			/*if(cc.selectedTilePos == 0){
                GUI.enabled = false;
            } else {
                GUI.enabled = true;
            }*/

//			if (GUI.Button(new Rect(5, 5, 100, 20), "Move")) {
//				cc.move = true;
//			}
//
//			if (GUI.Button(new Rect(5, 30, 100, 20), "Quit")) {
//				Globals.Instance().DebugLog("QUIT");
//				Application.LoadLevel("Login");
//			}

//			GUI.enabled = true;
//			GUI.Label(new Rect(110, 5, 30, 30), cc.selectedTilePos.ToString());

//			diceRect = GUI.Window(0, diceRect, makeDiceWindow, "Dice", "Window");
//			logRect = GUI.Window(1, logRect, makeLogWindow, "Log", "Window");
			//debugRect = GUI.Window (2, debugRect, makeDebugWindow, "Debug", "Window");

//			if (diceRect.Contains(Event.current.mousePosition) || logRect.Contains(Event.current.mousePosition)) {
//				inGUI = true;
//			} else {
//				inGUI = false;
//			}
		}

		void makeDebugWindow(int id) {
			GUIContent debugContent = new GUIContent(debugStr);
			float debughght = logStyle.CalcHeight(debugContent, debugRect.width);
			debugScrollPos = GUI.BeginScrollView(scrollRect, debugScrollPos, new Rect(scrollRect.x, scrollRect.y, scrollRect.width - 20, debughght));
			if (newDebug) {
				newDebug = false;
				debugScrollPos.y = debughght;
			}
			GUI.Label(new Rect(scrollRect.x, scrollRect.y, scrollRect.width, debughght), debugStr);
			GUI.EndScrollView();

			GUI.DragWindow();
		}

		void makeLogWindow(int id) {
			GUIContent logContent = new GUIContent(log);
			float loghght = logStyle.CalcHeight(logContent, logRect.width);
			logScrollPos = GUI.BeginScrollView(scrollRect, logScrollPos, new Rect(scrollRect.x, scrollRect.y, scrollRect.width - 20, loghght));
			if (newLog) {
				newLog = false;
				logScrollPos.y = loghght;
			}
			GUI.Label(new Rect(scrollRect.x, scrollRect.y, scrollRect.width, loghght), log);
			GUI.EndScrollView();

			GUI.DragWindow();
		}

		void makeDiceWindow(int id) {
			GUI.Label(new Rect(25, 50, 50, 20), "Amount: ");
			amtText = GUI.TextField(new Rect(75, 50, 100, 20), amtText, 25);

			GUI.Label(new Rect(25, 75, 50, 20), "Modifier: ");
			modText = GUI.TextField(new Rect(75, 75, 100, 20), modText, 25);

			if (GUI.Button(new Rect(100, 100, 50, 20), "Roll")) {
				newLog = true;
				string str = listofDice[diceEntry].text;
				Dice.DiceType type = (Dice.DiceType)System.Enum.Parse(typeof(Dice.DiceType), str);
				int roll = Dice.roll(int.Parse(amtText), type);
				//int res = roll + int.Parse(modText);
				log += "Player x has rolled a " + roll + " for their " + listofType[rollEntry].text;
				int total = int.Parse(modText) + roll;
				if (int.Parse(modText) < 0) {
					log += " with a " + modText + " for a total of " + total;
				} else if (int.Parse(modText) != 0) {
					log += " with a +" + modText + " for a total of " + total;
				}

				log += " for their " + listofType[rollEntry].text + " roll. \n";
				resText = "Type: " + listofType[rollEntry].text + "\nAmount: " + amtText + "\nRoll: " + roll + "\nModifier: " + modText;
			}

			GUI.Label(new Rect(25, 125, 50, 20), "Result: ");
			GUI.TextField(new Rect(75, 125, 100, 80), resText);

			if (MythDropdown.List(new Rect(15, 20, 80, 20), ref showType, ref rollEntry, new GUIContent(listofType[rollEntry].text), listofType, listSkin.button)) {

			}

			if (MythDropdown.List(new Rect(100, 20, 80, 20), ref showDice, ref diceEntry, new GUIContent(listofDice[diceEntry].text), listofDice, listSkin.button)) {

			}

			GUI.DragWindow();
		}

		public void addToLog(string mess) {
			newLog = true;
			log += mess;
		}

		public void addToDebug(string mess) {
			newDebug = true;
			debugStr += mess;
		}

        private void diceRollResult(){
            List<Dropdown.OptionData> diceOptions = diceTypeDropdown.GetComponent<Dropdown>().options;
            Dice.DiceType diceType = (Dice.DiceType)System.Enum.Parse(typeof(Dice.DiceType), diceOptions[diceEntry].text);

            int roll = Dice.roll(int.Parse(amountInput.GetComponent<InputField>().text), diceType);
            int total = roll + int.Parse(modifierInput.GetComponent<InputField>().text);

            List<Dropdown.OptionData> rollOptions = rollTypeDropdown.GetComponent<Dropdown>().options;
            string rollType = rollOptions[rollEntry].text;

            string message = "";
            if (int.Parse(modifierInput.GetComponent<InputField>().text) < 0) {
                message += " with a -" + modifierInput.GetComponent<InputField>().text + " for a total of " + total;
            } else if (int.Parse(modifierInput.GetComponent<InputField>().text) != 0) {
                message += " with a +" + modifierInput.GetComponent<InputField>().text + " for a total of " + total;
            }

            message += "\n";

            logScrollContent.GetComponentInChildren<Text>().text += "Player x has rolled a " + roll + " for their " + rollType + message;
            resultScrollContent.GetComponentInChildren<Text>().text += "You have rolled a " + roll + " for your " + rollType + message;
        }
	}
}
