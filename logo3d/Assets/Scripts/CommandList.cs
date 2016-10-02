using UnityEngine;
using System.Collections;

public class CommandList : MonoBehaviour {
	/// <summary>
	/// Class to store the commands that can be accessed by both buttons and the input parser
	/// Currently receives the player gameObject through public declaration
	/// </summary>
	GameObject obj;

	Vector3 startPos;
	Quaternion startRot;

	Material lineMat;

	void Start(){
	}

	public void MoveForward(float val){
		if (val != 0) {
			obj = GameObject.FindGameObjectWithTag ("Player");
			var dest = obj.transform.forward * (val / 100);
			DrawLine (obj.transform.position, obj.transform.position + dest); 
			obj.transform.position += dest;

		}
	}

	public void MoveBackwards(float val){
		if (val != 0) {
			obj = GameObject.FindGameObjectWithTag ("Player");
			var dest = -obj.transform.forward * (val / 100);
			DrawLine (obj.transform.position, obj.transform.position + dest);
			obj.transform.position += dest;
		}
	}

	public void RotateLeft(float val){
		if (val != 0) {
			obj = GameObject.FindGameObjectWithTag ("Player");
			obj.transform.Rotate (new Vector3 (0, -val, 0));
		}
	}

	public void RotateRight(float val){
		if (val != 0) {
			obj = GameObject.FindGameObjectWithTag ("Player");
			obj.transform.Rotate (new Vector3 (0, val, 0));
		}
	}

	//===Returns to the starting position and rotation received in Start()===//
	public void ToHome(){
		obj = GameObject.FindGameObjectWithTag ("Player");
		obj.transform.position = new Vector3(0,0.1f,0);
		obj.transform.Rotate (Vector3.zero);
	}	

	//not working as I would like, will need rework//
	void DrawLine(Vector3 start, Vector3 end){
		Debug.Log (start + "is start and the end: " + end);
		GameObject newLine = new GameObject ();
		newLine.transform.position = new Vector3 (0, 0.1f, 0);
		newLine.AddComponent<LineRenderer> ();
		LineRenderer lr = newLine.GetComponent<LineRenderer> ();
		lr.useWorldSpace = false;
		lr.material = Resources.Load ("lineMat") as Material;
		lr.material.SetColor ("_Color", Color.blue);
		lr.SetWidth (0.05f, 0.05f);
		lr.SetPosition (0, start);
		lr.SetPosition (1, end);
	}

}
	