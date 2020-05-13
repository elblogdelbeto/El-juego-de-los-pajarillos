using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botonesGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {

        if (GUI.RepeatButton(new Rect(10, 70, 50, 30), "Click"))
            Debug.Log("Clicked the button with text");
    }
}
