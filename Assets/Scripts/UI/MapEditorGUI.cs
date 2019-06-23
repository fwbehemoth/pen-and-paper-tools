using System;
using System.Collections.Generic;
using Myth.Engine;
using Myth.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Myth.UI {
	public class MapEditorGUI : GUICore {
		private int entrySet = 0;
		private int entryTile = 0;
		private int numRows;
		private int numCols;
        private int totTiles;
		public GameObject selection = null;
		public ModeType mode = ModeType.PAINT;
		public ToolType tool = ToolType.ADD;
		private EditGrid grid;
		private int[] tileIdList;
		private Dictionary<int, int> tileIdDict;
		private int[] setIdList;
        public RectTransform modesGroup;
        public RectTransform toolsGroup;
        public RectTransform rowInput;
        public RectTransform columnInput;
        public RectTransform saveButton;
        public RectTransform quitButton;
        public RectTransform setsDropdown;
        public RectTransform tilesDropdown;
        public RectTransform updateButton;

		public enum ModeType{
			PAINT,
			SELECT
		}

		public enum ToolType{
			ADD,
			DELETE,
			MOVE,
			ROTATE
		}

        void Awake() {
            base.Awake();

            tabThroughUIUtil.addUIObject(modesGroup.Find("Paint").gameObject);
            tabThroughUIUtil.addUIObject(modesGroup.Find("Select").gameObject);
            tabThroughUIUtil.addUIObject(toolsGroup.Find("Add").gameObject);
            tabThroughUIUtil.addUIObject(toolsGroup.Find("Delete").gameObject);
            tabThroughUIUtil.addUIObject(toolsGroup.Find("Move").gameObject);
            tabThroughUIUtil.addUIObject(toolsGroup.Find("Rotate").gameObject);
            tabThroughUIUtil.addUIObject(rowInput.gameObject);
            tabThroughUIUtil.addUIObject(columnInput.gameObject);
            tabThroughUIUtil.addUIObject(updateButton.gameObject);
            tabThroughUIUtil.addUIObject(saveButton.gameObject);
            tabThroughUIUtil.addUIObject(quitButton.gameObject);
            tabThroughUIUtil.addUIObject(setsDropdown.gameObject);
            tabThroughUIUtil.addUIObject(tilesDropdown.gameObject);

            setsDropdown.GetComponent<Dropdown>().onValueChanged.AddListener(
                    delegate(int arg0) {
                        setsDropdown.GetComponent<Dropdown>().Hide();
                        entrySet = setsDropdown.GetComponent<Dropdown>().value;
                        refreshTilesDropdown();
                        setSelection();
                    }
            );

            tilesDropdown.GetComponent<Dropdown>().onValueChanged.AddListener(
                    delegate(int arg0) {
                        entryTile = tilesDropdown.GetComponent<Dropdown>().value;
                        tilesDropdown.GetComponent<Dropdown>().Hide();
                        setSelection();
                    }
            );

            foreach(Toggle toggle in modesGroup.transform.GetComponentsInChildren<Toggle>()){
                toggle.onValueChanged.AddListener(
                        delegate(bool isSelected) {
                            toggleMode(toggle.name);
                        }
                );
            }

            foreach(Toggle toggle in toolsGroup.transform.GetComponentsInChildren<Toggle>()){
                toggle.onValueChanged.AddListener(
                        delegate(bool isSelected) {
                            toggleTool(toggle.name);
                        }
                );
            }

            saveButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        Globals.Instance().DebugLog(this.GetType().Name, "SAVE");
                        Globals.Instance().userBO.model.mapBO.save(grid.numRows, grid.numCols);
                    }
            );

            quitButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        Globals.Instance().DebugLog(this.GetType().Name, "QUIT");
                        SceneManager.LoadScene("Login");
                    }
            );

            updateButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        refreshMapSize(Int32.Parse(rowInput.GetComponent<InputField>().text), Int32.Parse(columnInput.GetComponent<InputField>().text));
                    }
            );

            setupGrid();
        }

        private void setupGrid(){
            grid = GameObject.Find("Main Camera").GetComponent<EditGrid>();

            numRows = grid.numRows;
            numCols = grid.numCols;

//            if(Globals.Instance().userBO.model.mapBO.mapTilesBO.collection.Count == 0) {
//
//            } else {
//                grid.numRows = Globals.Instance().userBO.model.mapBO.mapTilesBO.collection.Count;
//                grid.numCols = Globals.Instance().userBO.model.mapBO.mapTilesBO.collection[0].Count;
//            }

            rowInput.GetComponent<InputField>().text = StringUtils.Parse(numRows);
            columnInput.GetComponent<InputField>().text = StringUtils.Parse(numCols);
        }

		void Start () {
            refresh();
		}

        void OnEnabel(){
            base.OnEnable();
        }

		void Update () {
            base.Update();
            if(doRefresh){
				refreshSetsDropdown();
                refreshTilesDropdown();
            }
            entrySet = 0;
            entryTile = 0;
			setsDropdown.GetComponent<Dropdown>().RefreshShownValue();
            tilesDropdown.GetComponent<Dropdown>().RefreshShownValue();
		}

        private void refreshSetsDropdown(){
            setsDropdown.GetComponent<Dropdown>().options.Clear();
            totTiles = 0;
            setIdList = new int[Globals.Instance().userBO.model.tileSetsBO.collection.Count + 1];
            if(Globals.Instance().userBO.model.tileSetsBO.collection.Count != 0){
                doRefresh = false;
                int i = 0;
                setsDropdown.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData("All"));
                setIdList[i] = -1;
                i++;
                foreach(KeyValuePair<int, TileSetBusinessObject> entry in Globals.Instance().userBO.model.tileSetsBO.collection){
                    setsDropdown.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData(entry.Value.model.name));
                    setIdList[i] = entry.Value.model.id;
                    totTiles += entry.Value.model.tilesBO.collection.Count;
                    totTiles += entry.Value.model.piecesBO.collection.Count;
                    i++;
                }
            }
        }

        private void refreshTilesDropdown(){
            tilesDropdown.GetComponent<Dropdown>().options.Clear();
            int j = 0;
            if(setsDropdown.GetComponent<Dropdown>().options[setsDropdown.GetComponent<Dropdown>().value].text == "All"){
                tileIdList = new int[totTiles];
                tileIdDict = new Dictionary<int, int>();

                foreach(KeyValuePair<int, TileSetBusinessObject> entry in Globals.Instance().userBO.model.tileSetsBO.collection){
                    if(entry.Value.model.tilesBO.collection.Count != 0){
                        foreach(KeyValuePair<int, TileBusinessObject> entry2 in entry.Value.model.tilesBO.collection){
                            tilesDropdown.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData(entry.Value.model.tilesBO.collection[entry2.Key].model.name));
                            tileIdList[j] = entry2.Key;
                            tileIdDict.Add(entry2.Key, entry.Key);
                            j++;
                        }
                    }

                    if(entry.Value.model.piecesBO.collection.Count != 0){
                        foreach(KeyValuePair<int, PieceBusinessObject> entry2 in entry.Value.model.piecesBO.collection){
                            tilesDropdown.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData(entry.Value.model.piecesBO.collection[entry2.Key].model.name));
                            tileIdList[j] = entry2.Key;
                            tileIdDict.Add(entry2.Key, entry.Key);
                            j++;
                        }
                    }
                }
            } else {
                tileIdDict = new Dictionary<int, int>();
                if(Globals.Instance().userBO.model.tileSetsBO.collection[setIdList[entrySet]].model.tilesBO.collection.Count + Globals.Instance().userBO.model.tileSetsBO.collection[setIdList[entrySet]].model.piecesBO.collection.Count < entryTile){
                    entryTile = 0;
                }
                if(Globals.Instance().userBO.model.tileSetsBO.collection[setIdList[entrySet]].model.tilesBO.collection.Count != 0){
                    foreach(KeyValuePair<int, TileBusinessObject> entry in Globals.Instance().userBO.model.tileSetsBO.collection[setIdList[entrySet]].model.tilesBO.collection){
                        tilesDropdown.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData(entry.Value.model.name));
                        tileIdList[j] = entry.Value.model.id;
                        tileIdDict.Add(entry.Key, setIdList[entrySet]);
                        j++;
                    }
                }

                if(Globals.Instance().userBO.model.tileSetsBO.collection[setIdList[entrySet]].model.piecesBO.collection.Count != 0){
                    foreach(KeyValuePair<int, PieceBusinessObject> entry in Globals.Instance().userBO.model.tileSetsBO.collection[setIdList[entrySet]].model.piecesBO.collection){
                        tilesDropdown.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData(entry.Value.model.name));
                        tileIdList[j] = entry.Value.model.id;
                        tileIdDict.Add(entry.Key, setIdList[entrySet]);
                        j++;
                    }
                }
            }

            setSelection();
        }

        public override void refresh () {
            doRefresh = true;
        }

        public void refreshMapSize(Dictionary<string, int> counts){
            int row = counts["row"];
            int column = counts["column"];
            refreshMapSize(row, column);
        }

        private void refreshMapSize(int rows, int cols){
//            if(Globals.Instance().userBO.model.mapBO.mapTilesBO.collection.Count > 0) {
//                if (Globals.Instance().userBO.model.mapBO.mapTilesBO.collection.getCurrentRowCount() < grid.numRows || Globals.Instance().userBO.model.mapBO.mapTilesBO.collection.getCurrentColumnCount() < grid.numCols) {
//                    Globals.Instance().userBO.model.mapBO.mapTilesBO.collection.setCurrentCounts(grid.numRows, grid.numCols);
//                }
//            }

            grid.numRows = rows;
            grid.numCols = cols;
        }

        private void toggleMode(string selected){
            switch(selected){
                case "Paint":
                    mode = ModeType.PAINT;
                    break;

                case "Select":
                    mode = ModeType.SELECT;
                    break;
            }
        }

        private void toggleTool(string selected){
            switch(selected){
                case "Add":
                    tool = ToolType.ADD;
                    break;

                case "Delete":
                    tool = ToolType.DELETE;
                    break;

                case "Move":
                    tool = ToolType.MOVE;
                    break;

                case "Rotate":
                    tool = ToolType.ROTATE;
                    break;
            }
        }

		public void setSelection(){
			string select = "";
			if (Globals.Instance().userBO.model.tileSetsBO.collection[tileIdDict[tileIdList[entryTile]]].model.tilesBO.collection.ContainsKey(tileIdList[entryTile])) {
				select = "Tiles/" + Globals.Instance().userBO.model.tileSetsBO.collection[tileIdDict[tileIdList[entryTile]]].model.name + "/" + Globals.Instance().userBO.model.tileSetsBO.collection[tileIdDict[tileIdList[entryTile]]].model.name + "_" + Globals.Instance().userBO.model.tileSetsBO.collection[tileIdDict[tileIdList[entryTile]]].model.tilesBO.collection[tileIdList[entryTile]].model.name.Replace(" ", "_");
			} else if (Globals.Instance().userBO.model.tileSetsBO.collection[tileIdDict[tileIdList[entryTile]]].model.piecesBO.collection.ContainsKey(tileIdList[entryTile])) {
				select = "Pieces/" + Globals.Instance().userBO.model.tileSetsBO.collection[tileIdDict[tileIdList[entryTile]]].model.name + "/" + Globals.Instance().userBO.model.tileSetsBO.collection[tileIdDict[tileIdList[entryTile]]].model.name + "_" + Globals.Instance().userBO.model.tileSetsBO.collection[tileIdDict[tileIdList[entryTile]]].model.piecesBO.collection[tileIdList[entryTile]].model.name.Replace(" ", "_");
			}
			Globals.Instance().DebugLog(this.GetType().Name, "Selection: " + select);
			selection = (GameObject)Resources.Load(select);
		}

		public void removeExtraTiles(){
			for(int x = 0; x < Globals.Instance().userBO.model.mapBO.mapTilesBO.collection.Count; x++){
				for(int y = 0; y < Globals.Instance().userBO.model.mapBO.mapTilesBO.collection.Count; y++) {
					// finish removing tiles after updating the grid.

				}
			}
		}
	}
}
