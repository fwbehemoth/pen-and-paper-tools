using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utils {
    public class TabThroughUIUtil {
        private List<GameObject> uiObjects = new List<GameObject>();
        private int currentObjectIndex = 0;

        public void addUIObject(GameObject uiObject){
            uiObjects.Add(uiObject);
        }

        public void setSelectedObject(int index){
            currentObjectIndex = index;
            EventSystem.current.SetSelectedGameObject(uiObjects[currentObjectIndex]);
        }

        public void nextUIObject(){
            currentObjectIndex++;
            if(currentObjectIndex > uiObjects.Count - 1){
                currentObjectIndex = 0;
            }

            setSelectedObject(currentObjectIndex);
        }

        public void previousUIObject(){
            currentObjectIndex--;
            if(currentObjectIndex < 0){
                currentObjectIndex = uiObjects.Count - 1;
            }

            setSelectedObject(currentObjectIndex);
        }
    }
}