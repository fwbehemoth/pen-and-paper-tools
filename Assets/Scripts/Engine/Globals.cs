using System;
using System.Collections.Generic;
using Myth.BaseLib;
using Myth.UI;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Globals : MonoBehaviour {
    static Globals instance = null;
    private GameMaster_Controller GM_Controller;
    public MythAPI api;
    public Networking networking;
    public MythFileManager fileManager;
    public UserBusinessObject userBO;
    private Rect scrollRect = new Rect(5, 20, 400, 300);
    private Rect debugRect = new Rect(250, 500, 400, 300);
    private Vector2 debugScrollPos = Vector2.zero;
    private string debugStr = "";
    private bool newDebug = false;
    private GUIStyle style = new GUIStyle();
    public GUISkin skin;
    public GUIManager.WindowType LoginWinType;

    public static Globals Instance(){
        return instance;
    }

    // Use this for initialization
    void Awake(){
        if(!instance){
            instance = this;
            userBO = new UserBusinessObject();
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    void OnGUI (){
//		debugRect = GUI.Window (2, debugRect, makeDebugWindow, "Debug", "Window");
    }

    public void addToDebug(string mess){
        newDebug = true;
        debugStr += mess;
    }

    void makeDebugWindow(int id){
        GUIContent debugContent = new GUIContent(debugStr);
        float debughght = style.CalcHeight(debugContent, debugRect.width);
        debugScrollPos = GUI.BeginScrollView(scrollRect, debugScrollPos, new Rect(scrollRect.x, scrollRect.y, scrollRect.width - 20, debughght));
        if(newDebug){
            newDebug = false;
            debugScrollPos.y = debughght;
        }
        GUI.Label(new Rect(scrollRect.x, scrollRect.y, scrollRect.width, debughght), debugStr);
        GUI.EndScrollView();

        GUI.DragWindow();
    }

    public Vector3 Vector3FromString(string vec){
        vec = vec.Replace("(", "");
        vec = vec.Replace(")", "");
        string[] vectorArr = vec.Split(","[0]);
        float x = float.Parse(vectorArr[0]);
        float y = float.Parse(vectorArr[1]);
        float z = float.Parse(vectorArr[2]);

        return new Vector3(x, y , z);
    }

    public Quaternion QuaternionFromString(string quat){
        quat = quat.Replace("(", "");
        quat = quat.Replace(")", "");
        string[] quatArr = quat.Split(","[0]);
        float w = float.Parse(quatArr[0]);
        float x = float.Parse(quatArr[1]);
        float y = float.Parse(quatArr[2]);
        float z = float.Parse(quatArr[3]);

        return new Quaternion(w, x, y ,z);
    }

    public string DictionaryToString(Dictionary<string, string> dict){
        string info = "";
        foreach(KeyValuePair<string,string> entry in dict){
            info += "&" + entry.Key + "=" + entry.Value;
        }
        return info;
    }

    public void DebugLog(string origin, string message){
        Debug.Log (origin + " - " + message);
        addToDebug(message + "\n");
    }

    public void removeAllPlanes(){
        GameObject[] planes = GameObject.FindGameObjectsWithTag("Plane");
        foreach(GameObject plane in planes){
//            if(plane.transform.position.x > thresholdX || plane.transform.position.y > thresholdY){
                Destroy(plane);
//            }
        }
    }
}
