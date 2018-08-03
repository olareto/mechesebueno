using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Fondo : MonoBehaviour {

    [SerializeField]
    private float separacion;

    private GameObject camara;

	// Use this for initialization
	void Start () {
        camara = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(-camara.transform.position.x * separacion, this.transform.position.y, this.transform.position.z);
	}
}
