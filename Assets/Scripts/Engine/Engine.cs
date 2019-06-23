using System.Collections;
using Myth.UI.Lib;
using UnityEngine;

namespace Myth.Engine {
	public class Engine : MonoBehaviour {
		public GameObject player;
		public GameObject startSquare;
		public bool inGUI = false;

		private Rect diceRect = new Rect(0, 100, 200, 220);
		private Rect logRect = new Rect(250, 20, 600, 200);
		private Rect scrollRect = new Rect(5, 20, 590, 180);

		private Vector2 logScrollPos = Vector2.zero;
		private ArrayList selectedSq = new ArrayList();
		private int maxSquares = 6;
		private int selectedSqPos = 0;
		private bool move = false;
		private bool newLog = false;

		private Hashtable ht;
		private Transform currentPos;
		private Dice dice;
		private string amtText = "1";
		private string modText = "0";
		private string resText = "";
		private string log = "";
		private float logMaxHght;
		private bool showDice = false;
		private int diceEntry = 0;
		private GUIContent[] listofDice;

		private bool showType = false;
		private int typeEntry = 0;
		//private int lasttypeEntry = 0;
		//private Vector2 typeVector = Vector2.zero;
		private GUIContent[] listofType;
		//private string[] listofType;

		public GUISkin listSkin;
		private GUIStyle logStyle = new GUIStyle();

		// Use this for initialization
		void Start () {
			player.transform.position = startSquare.transform.position;
			startSquare.GetComponent<TileController>().Clicked = true;
			currentPos = startSquare.transform;
			selectedSq.Add(startSquare.transform);
			dice = this.gameObject.GetComponent<Dice>();

			// Make a GUIStyle that has a solid white hover/onHover background to indicate highlighted items
			/*listStyle =new GUIStyle();
			listStyle.normal.textColor = Color.white;
			var tex = new Texture2D(2, 2);
			listStyle.hover.background = tex;
			listStyle.onHover.background = tex;
			listStyle.padding.left = listStyle.padding.right = listStyle.padding.top = listStyle.padding.bottom = 4;*/

			listofDice = new GUIContent[System.Enum.GetValues(typeof(Dice.DiceType)).Length];
			int i = 0;
			foreach (Dice.DiceType type in System.Enum.GetValues(typeof(Dice.DiceType))) {
				listofDice[i] = new GUIContent(type.ToString());
				i++;
			}

			//		listofType = new Dictionary<int, GUIContent>();
			//		listofType.Add(0, new GUIContent("Initiative"));
			//		listofType.Add(1, new GUIContent("Attack"));
			//		listofType.Add(2, new GUIContent("Damage"));
			//		listofType.Add(3, new GUIContent("Skill"));

			listofType = new GUIContent[4];
			listofType[0] = new GUIContent("Initiative");
			listofType[1] = new GUIContent("Attack");
			listofType[2] = new GUIContent("Damage");
			listofType[3] = new GUIContent("Skill");
		}

		// Update is called once per frame
		void Update () {
			if (!inGUI) {
				if (Input.GetMouseButtonDown(0)) {
					RaycastHit hit = new RaycastHit();
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					if (Physics.Raycast(ray, out hit)) {
						if (hit.transform.tag == "Square") {
							if (hit.transform != startSquare.transform && hit.transform.GetComponent<TileController>().type == "movement") {
								if (hit.transform.GetComponent<TileController>().Clicked) {
									if ( hit.transform == (Transform)selectedSq[selectedSqPos]) {
										hit.transform.GetComponent<TileController>().Clicked = false;
										selectedSq.Remove(hit.transform);
										selectedSqPos--;
										currentPos = (Transform)selectedSq[selectedSqPos];
									}
								} else {
									if (isAdjacent(hit.transform) && (selectedSqPos < maxSquares)) {
										selectedSqPos++;
										currentPos = hit.transform;
										selectedSq.Add(hit.transform);
										hit.transform.GetComponent<TileController>().Clicked = true;
									}
								}
							}
						}
					}
				}

				if (move) {
					for (int i = 1; i < selectedSq.Count; i++) {
						Transform pos = (Transform)selectedSq[i];
						player.transform.position = pos.position;
					}
					move = false;
					startSquare = currentPos.gameObject;
					selectedSq = new ArrayList();
					selectedSq.Add(startSquare.transform);
					resetSquares();
					startSquare.transform.GetComponent<TileController>().Clicked = true;
					selectedSqPos = 0;
				}
			}
		}

		void OnGUI () {
			if (selectedSqPos == 0) {
				GUI.enabled = false;
			} else {
				GUI.enabled = true;
			}

			if (GUI.Button(new Rect(5, 5, 100, 20), "Move")) {
				move = true;
			}

			GUI.enabled = true;
			GUI.Label(new Rect(110, 5, 30, 30), selectedSqPos.ToString());

			diceRect = GUI.Window(0, diceRect, makeDiceWindow, "Dice", "Window");
			logRect = GUI.Window(1, logRect, makeLogWindow, "Log", "Window");

			if (diceRect.Contains(Event.current.mousePosition) || logRect.Contains(Event.current.mousePosition)) {
				inGUI = true;
			} else {
				inGUI = false;
			}
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
				log += "Player x has rolled a " + roll + " for their " + listofType[typeEntry].text;
				int total = int.Parse(modText) + roll;
				if (int.Parse(modText) < 0) {
					log += " with a " + modText + " for a total of " + total;
				} else if (int.Parse(modText) != 0) {
					log += " with a +" + modText + " for a total of " + total;
				}

				log += " for their " + listofType[typeEntry].text + " roll. \n";
				resText = "Type: " + listofType[typeEntry].text + "\nAmount: " + amtText + "\nRoll: " + roll + "\nModifier: " + modText;
			}

			GUI.Label(new Rect(25, 125, 50, 20), "Result: ");
			GUI.TextField(new Rect(75, 125, 100, 80), resText);

			if (MythDropdown.List(new Rect(15, 20, 80, 20), ref showType, ref typeEntry, new GUIContent(listofType[typeEntry].text), listofType, listSkin.button)) {

			}

			if (MythDropdown.List(new Rect(100, 20, 80, 20), ref showDice, ref diceEntry, new GUIContent(listofDice[diceEntry].text), listofDice, listSkin.button)) {

			}

			GUI.DragWindow();
		}

		void resetSquares() {
			GameObject[] squares = GameObject.FindGameObjectsWithTag("Square");
			foreach (GameObject square in squares) {
				square.transform.GetComponent<TileController>().Clicked = false;
			}
		}

		bool isAdjacent(Transform pos) {
			bool isNext = false;
			float playerXLT = currentPos.transform.position.x - (startSquare.transform.GetComponent<Collider>().bounds.size.x);
			float playerXRT = currentPos.transform.position.x + (startSquare.transform.GetComponent<Collider>().bounds.size.x);
			float playerZUP = currentPos.transform.position.z + (startSquare.transform.GetComponent<Collider>().bounds.size.z);
			float playerZDN = currentPos.transform.position.z - (startSquare.transform.GetComponent<Collider>().bounds.size.z);

			if (Mathf.Approximately(pos.position.x, playerXLT) && Mathf.Approximately(pos.position.z, playerZUP)) {
				if (!(isWall(playerXLT, currentPos.transform.position.z) || isWall(currentPos.transform.position.x, playerZUP))) {
					isNext = true;
				}
			}

			if (Mathf.Approximately(pos.position.x, playerXLT) && Mathf.Approximately(pos.position.z, playerZDN)) {
				if (!(isWall(playerXLT, currentPos.transform.position.z) || isWall(currentPos.transform.position.x, playerZDN))) {
					isNext = true;
				}
			}

			if (Mathf.Approximately(pos.position.x, playerXRT) && Mathf.Approximately(pos.position.z, playerZUP)) {
				if (!(isWall(playerXRT, currentPos.transform.position.z) || isWall(currentPos.transform.position.x, playerZUP))) {
					isNext = true;
				}
			}

			if (Mathf.Approximately(pos.position.x, playerXRT) && Mathf.Approximately(pos.position.z, playerZDN)) {
				if (!(isWall(playerXRT, currentPos.transform.position.z) || isWall(currentPos.transform.position.x, playerZDN))) {
					isNext = true;
				}
			}

			if (Mathf.Approximately(pos.position.x, playerXLT) && Mathf.Approximately(pos.position.z, currentPos.transform.position.z)) {
				isNext = true;

			}

			if (Mathf.Approximately(pos.position.x, playerXRT) && Mathf.Approximately(pos.position.z, currentPos.transform.position.z)) {

				isNext = true;
			}

			if (Mathf.Approximately(pos.position.z, playerZUP) && Mathf.Approximately(pos.position.x, currentPos.transform.position.x)) {
				isNext = true;
			}

			if (Mathf.Approximately(pos.position.z, playerZDN) && Mathf.Approximately(pos.position.x, currentPos.transform.position.x)) {
				isNext = true;
			}

			return isNext;
		}

		bool isWall(float diagSqX, float diagSqZ) {
			GameObject[] squares = GameObject.FindGameObjectsWithTag("Square");
			foreach (GameObject square in squares) {
				if (square.transform.GetComponent<TileController>().type == "wall" && Mathf.Approximately(square.transform.position.x, diagSqX) && Mathf.Approximately(square.transform.position.z, diagSqZ)) {
					return true;
				}
			}
			return false;
		}
	}
}