using System.Collections;
using UnityEngine;

public class Enemy_Nameplate : MonoBehaviour {
	public string PlayerName;
	public string newName;
	public Font f;
	public GUIStyle s = new GUIStyle();
	public string affiliation = "friendly";
	public bool localToCamera = false;
	public float verticalOffset = 0f;
	public GameObject playerObject;
	private float stringLength = 0f;
	private float startOffsetVertical = 0f;
	private bool canSee = false;
	public LayerMask blockLayers = -1;
	private GameObject mainCam;
	
	/*			
	 * 
	 * 			How to use;
	 * 			
	 * 			Simply attach this to the object you want displaying the nameplate, then set the PlayerName, and newName to the name you want displaying.
	 * 			If you want to change or set the name via scripting, then simply use the GetComponent<nameplate1>().newName = "TheNewName";
	 * 			Use the Vertical Offset (negative int) to raise the nameplate from the base position of the game object.
	 * 			If you want to lower the nameplate, then simple put a positive int into the Vertical Offset within the Scene editor.
	 * 			
	 * 			You will need to select a font, if you are otherwise unable to select a font, the default font will be used.
	 * 			
	 * 			This is a very easy script which also compensates for occluded objects, and will only display nameplates of objects within
	 * 			the view of the camera.
	 * 
	 * 			Hope you enjoy this script. Provided free by Studio Sakamoto, Written by Koharu Sakamoto.
	 * 
	 * 			Free for unlimited unrestricted use.
	 * 			
	 * 			
	 */
	
	void Start(){
		mainCam = GameObject.Find ("Main Camera");
		newName = PlayerName;
		
		switch(affiliation){
		case "friendly":
			s.normal.textColor = Color.green;
			break;
		case "aggressive":
			s.normal.textColor = Color.red;
			break;
		case "defensive":
			s.normal.textColor = Color.yellow;
			break;
		default:
			s.normal.textColor = Color.blue;
			break;
		}
			
		startOffsetVertical = verticalOffset;	
		if(f != null){
			s.font = f;
		}
		
		stringLength = PlayerName.Length * 8.00f;
		//This allows you to set a custom colour depending on a part of the PlayerName. In this example, GM's display with a Gold Name.
		if(PlayerName.IndexOf("[GM]") != -1){
			//Player is GM.
			Color gold = new Color();
			gold.r = 255;
			gold.g = 210;
			gold.b = 0;
			gold.a = 1;
			s.normal.textColor = gold;
		}
	}
	
	void Update(){
		if(playerObject == null){
			playerObject = gameObject; //Set to this.	
		}
		
		if(newName != PlayerName){
			//We need to change the name!
			PlayerName = newName;
			Start(); //Reinitiate the start function.
		}
		
		if(startOffsetVertical != 0 && localToCamera == true){
			//float camPosUp = mainCam.transform.position.y; //Camera Height.
		
			if(verticalOffset < startOffsetVertical){
				verticalOffset = (startOffsetVertical/(Vector3.Distance(mainCam.transform.position,transform.position)*0.01f))/8;
			}else{
				verticalOffset = (startOffsetVertical/Mathf.Tan(startOffsetVertical)*(Vector3.Distance(mainCam.transform.position,transform.position)) / Mathf.Tan(startOffsetVertical));		
			}
		
		}else if(startOffsetVertical != 0){
			if(verticalOffset >= startOffsetVertical){
				float voffset = 0f;
				voffset = (startOffsetVertical/(Vector3.Distance(mainCam.transform.position,transform.position)*0.01f))/2;
				if(voffset < (startOffsetVertical*-1)){
					voffset = ((startOffsetVertical*(Vector3.Distance(mainCam.transform.position,transform.position)*-0.01f))/2)+startOffsetVertical;
				}
			verticalOffset = voffset;
			}
		}
		
		Vector3 cam = mainCam.GetComponent<Camera>().WorldToViewportPoint(transform.position);
		RaycastHit hit;
		if(!Physics.Linecast(transform.position + 2.1f * Vector3.up,playerObject.transform.position + 2.1f * Vector3.up,out hit,blockLayers)){
			if(cam.x >= 0 && cam.y >= 0 && cam.z >= 0){
				canSee = true;
			}else{
				canSee = false;	
			}
		}else{
			if(hit.collider != null){
				if(hit.collider.name == "player"){
					canSee = true;	
				}else{
					canSee = false;	
					Debug.DrawLine (playerObject.transform.position + 2.1f * Vector3.up, hit.point);
					//Debug.Log(hit.collider.name);
				}
			}
		}
	}
	
	void OnBecameVisible(){
		//Debug.Log(gameObject.name + " visible");
	}
	
	void OnBecameInvisible(){
		//Debug.Log(gameObject.name + " invisible");
	}
	
	void OnDrawGizmos (){
		// Use gizmos to gain information about the state of your setup	
		Gizmos.color = canSee ? Color.blue : Color.red;
		Gizmos.DrawLine (playerObject.transform.position + 2.1f * Vector3.up,transform.position + 1.8f * Vector3.up);
	}
	
	void OnGUI(){
		if(Vector3.Distance(playerObject.transform.position,transform.position) < 100 && canSee == true)
			GUI.Label(new Rect(mainCam.GetComponent<Camera>().WorldToScreenPoint(transform.position).x-(stringLength/2),Screen.height-mainCam.GetComponent<Camera>().WorldToScreenPoint(transform.position).y+verticalOffset,100,100),PlayerName,s);
	}
}

