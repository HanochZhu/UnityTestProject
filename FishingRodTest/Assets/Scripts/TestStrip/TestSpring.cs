using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpring : MonoBehaviour {

    Vector3 tarpos = new Vector3(10,10,10);
    float x = 0;
	// Use this for initialization
	void Start () {
        //tarpos *= 10f;
        StartCoroutine(myUpdate());
	}
	
	// Update is called once per frame
	IEnumerator myUpdate () {
        while (x < 1.1f)
        {
            this.transform.position = mylerp(Vector3.zero, tarpos, StripUtils.getInterpolation(x));
            Debug.Log(x);
            Debug.Log(StripUtils.getInterpolation(x));
            x += 0.01f;
            yield return new WaitForSeconds(1);
        }

	}

    private Vector3 mylerp(Vector3 or,Vector3 tar,float input){
        return tar * input - or * (1 - input);
    }

}
