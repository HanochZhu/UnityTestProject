using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBall : MonoBehaviour {

    public GameObject ga;
    private Vector3 offset;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            Vector3 pos = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 screenPos = Camera.main.WorldToScreenPoint(ga.transform.position);
                offset = screenPos - pos;
            }
            else if (Input.GetMouseButton(0))
            {
                ga.transform.position = Camera.main.ScreenToViewportPoint(pos + offset);
            }
        }
	}
}
