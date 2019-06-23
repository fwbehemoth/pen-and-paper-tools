using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myth.UI.Lib {
    public class MythDropdown {
        static int DropdownHash = "DropdownList".GetHashCode();
        static Vector2 scrollPos = Vector2.zero;
        // Delegate
        public delegate void ListCallBack(int select);

        public static bool List (Rect position, ref bool showList, ref int listEntry, GUIContent buttonContent, GUIContent[] list,
                GUIStyle listStyle/*, ListCallBack callBack*/) {



            return List(position, ref showList, ref listEntry, buttonContent, list, "button", "box", listStyle/*, callBack*/);
        }

        public static bool List (Rect position, ref bool showList, ref int listEntry, GUIContent buttonContent, GUIContent[] list,
                GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle/*, ListCallBack callBack*/) {

            int controlID = GUIUtility.GetControlID(DropdownHash, FocusType.Passive);
            bool done = false;
            //int lastSelection = 0;
            switch (Event.current.GetTypeForControl(controlID)) {
                case EventType.MouseDown:
                    if (position.Contains(Event.current.mousePosition)) {
                        GUIUtility.hotControl = controlID;
                        showList = true;
                        //lastSelection = listEntry;
                    }
                    break;
                case EventType.MouseUp:
                    if (showList) {
                        done = true;
                        // Call our delegate method
                        //callBack();
                    }
                    break;
            }

            GUI.Label(position, buttonContent, buttonStyle);

            if (showList) {
                // Get our list of strings
                string[] text = new string[list.Length];
                // convert to string
                for (int i = 0; i < list.Length; i++) {
                    //            int count = 0;
                    //            foreach(KeyValuePair<int, GUIContent> entry in dict){
                    //                text[count] = entry.Value.text;
                    //                count++;
                    text[i] = list[i].text;
                }

                int hght = 180;
                if (list.Length * 20 < hght) {
                    hght = list.Length * 20;
                }

                Rect scrollRect = new Rect(position.x, position.y, position.width + 20, hght);

                Rect listRect = new Rect(position.x, position.y, position.width, list.Length * 20);
                scrollPos = GUI.BeginScrollView(scrollRect, scrollPos, listRect);
                GUI.Box(listRect, "", boxStyle);
                listEntry = GUI.SelectionGrid(listRect, listEntry, text, 1, listStyle);
                GUI.EndScrollView();
            }
            if (done) {
                showList = false;
            }
            return done;
        }
    }
}
