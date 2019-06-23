using System.Collections;
using UnityEngine;

public class MoveTarget : MonoBehaviour {
	public float speed = 5.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update()
	{
		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d")){
			//transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
			transform.Translate(Vector3.right * speed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a")){
			//transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
			transform.Translate(Vector3.right * -(speed * Time.deltaTime));
		}
		if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey("w")){
			//transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
			transform.Translate(Vector3.up * -(speed * Time.deltaTime));
		}
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey("s")){
			//transform.position += new Vector3(0, 0, speed * Time.deltaTime);	
			transform.Translate(Vector3.up * speed * Time.deltaTime);
		}
	}
}
