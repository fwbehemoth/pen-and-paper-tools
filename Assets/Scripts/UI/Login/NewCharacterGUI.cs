using System;
using UnityEngine;
using UnityEngine.UI;

namespace Myth.UI.Login {
	public class NewCharacterGUI : GUICore {
		private string charName = "";
		private string charType = "";
		private string charRace = "";
		private string charGender = "";
		private string charAge = "";
		private string charHght = "";
		private string charWght = "";
        public RectTransform nameInput;
        public RectTransform typeInput;
        public RectTransform raceInput;
        public RectTransform ageInput;
        public RectTransform genderInput;
        public RectTransform heightInput;
        public RectTransform weightInput;
        public RectTransform cancelButton;
        public RectTransform characterMiniaturesDropdown;
        public RectTransform createCharacterButton;
        private int entryCharacterMiniature = 0;

        void Awake(){
            base.Awake();

            tabThroughUIUtil.addUIObject(nameInput.gameObject);
            tabThroughUIUtil.addUIObject(typeInput.gameObject);
            tabThroughUIUtil.addUIObject(raceInput.gameObject);
            tabThroughUIUtil.addUIObject(ageInput.gameObject);
            tabThroughUIUtil.addUIObject(genderInput.gameObject);
            tabThroughUIUtil.addUIObject(heightInput.gameObject);
            tabThroughUIUtil.addUIObject(weightInput.gameObject);
            tabThroughUIUtil.addUIObject(cancelButton.gameObject);
            tabThroughUIUtil.addUIObject(characterMiniaturesDropdown.gameObject);
            tabThroughUIUtil.addUIObject(createCharacterButton.gameObject);

            cancelButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
                        clearFields();
                        Globals.Instance().LoginWinType = GUIManager.WindowType.CharacterSelection;
                    }
            );

            createCharacterButton.GetComponent<Button>().onClick.AddListener(
                    delegate  {
						createCharacter();
                        Globals.Instance().LoginWinType = GUIManager.WindowType.CharacterSelection;
                    }
            );

            characterMiniaturesDropdown.GetComponent<Dropdown>().onValueChanged.AddListener(
                    delegate(int arg0) {
                        characterMiniaturesDropdown.GetComponent<Dropdown>().Hide();
                        entryCharacterMiniature = characterMiniaturesDropdown.GetComponent<Dropdown>().value;
                    }
            );
        }

		void clearFields() {
            nameInput.gameObject.GetComponentInChildren<InputField>().text = "";
            typeInput.gameObject.GetComponentInChildren<InputField>().text = "";
            raceInput.gameObject.GetComponentInChildren<InputField>().text = "";
            ageInput.gameObject.GetComponentInChildren<InputField>().text = "";
            genderInput.gameObject.GetComponentInChildren<InputField>().text = "";
            heightInput.gameObject.GetComponentInChildren<InputField>().text = "";
            weightInput.gameObject.GetComponentInChildren<InputField>().text = "";
		}

        void createCharacter(){
            charName = nameInput.gameObject.GetComponentInChildren<InputField>().text;
            charType = typeInput.gameObject.GetComponentInChildren<InputField>().text;
            charRace = raceInput.gameObject.GetComponentInChildren<InputField>().text;
            charAge = ageInput.gameObject.GetComponentInChildren<InputField>().text;
            charGender = genderInput.gameObject.GetComponentInChildren<InputField>().text;
            charHght = heightInput.gameObject.GetComponentInChildren<InputField>().text;
            charWght = weightInput.gameObject.GetComponentInChildren<InputField>().text;

            CharacterBusinessObject charBO = new CharacterBusinessObject(charName, charType, charRace, Int32.Parse(charAge), charGender, charHght, charWght, 1);
            clearFields();
            charBO.save();
        }
	}
}
