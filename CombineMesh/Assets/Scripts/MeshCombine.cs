using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombine : MonoBehaviour {

    public MeshFilter meshFilter1;
    public MeshFilter meshFilter2;

	// Use this for initialization
	void Start () {
        CombineMesh(meshFilter1, meshFilter2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CombineMesh(MeshFilter tmesh1,MeshFilter tmesh2)
    {
        GameObject go = new GameObject("combine");
        go.AddComponent<MeshFilter>();
        go.AddComponent<MeshRenderer>();

        CombineInstance[] combines = new CombineInstance[2];
        Material[] materials = new Material[2];

        combines[0].mesh = tmesh1.mesh;
        combines[0].transform = tmesh1.transform.localToWorldMatrix;
        materials[0] = tmesh1.GetComponent<MeshRenderer>().material;

        combines[1].mesh = tmesh2.mesh;
        combines[1].transform = tmesh2.transform.localToWorldMatrix;
        materials[1] = tmesh2.GetComponent<MeshRenderer>().material;

        tmesh2.gameObject.SetActive(false);
        tmesh1.gameObject.SetActive(false);

        go.GetComponent<MeshFilter>().mesh = new Mesh();
        go.GetComponent<MeshFilter>().mesh.CombineMeshes(combines,false);
        go.GetComponent<MeshRenderer>().sharedMaterials = materials;
    }
}
