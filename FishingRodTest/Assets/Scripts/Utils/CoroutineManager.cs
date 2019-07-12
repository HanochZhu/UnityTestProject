using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour {

    private static CoroutineManager instance;
    public static CoroutineManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("CoroutineManager").AddComponent<CoroutineManager>() ;
            }
            return instance;
        }
    }

    public void AddCoroutine(IEnumerator cur)
    {
        this.StartCoroutine(cur);
    }
}
