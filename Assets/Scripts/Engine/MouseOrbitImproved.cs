using System.Collections;
using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour {
 
    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
 
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
 
    public float distanceMin = .5f;
    public float distanceMax = 15f;
 
    float x = 0.0f;
    float y = 0.0f;
 	
	Quaternion rotation;
	Vector3 negDistance;
	Vector3 position;
		
	// Use this for initialization
	void Start () {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
 
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
		
		setRotation();
	}
 
    void LateUpdate () {
		if(GUIUtility.hotControl==0){
			if (Input.GetMouseButton(1)){
			    if (target) {
			        setRotation();
			    }
			} else {
				setDistance();
			}
		}
	}
	
	void setDistance(){
        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*10, distanceMin, distanceMax);
 
        RaycastHit hit;
        if (Physics.Linecast (target.position, transform.position, out hit)) {
                distance -=  hit.distance;
        }
        negDistance = new Vector3(0.0f, 0.0f, -distance);
        position = rotation * negDistance + target.position;
 
        transform.position = position;
	}
	
	void setRotation(){
        RaycastHit hit;
        if (Physics.Linecast (target.position, transform.position, out hit)) {
                distance -=  hit.distance;
        }
		
		x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
		
        y = ClampAngle(y, yMinLimit, yMaxLimit);
 
        rotation = Quaternion.Euler(y, x, 0);
		
        negDistance = new Vector3(0.0f, 0.0f, -distance);
        position = rotation * negDistance + target.position;

        transform.rotation = rotation;
        transform.position = position;
	}
 
    public static float ClampAngle(float angle, float min, float max){
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}