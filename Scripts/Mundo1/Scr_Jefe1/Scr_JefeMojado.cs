using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_JefeMojado : MonoBehaviour {

    private float velocidad;

	// Use this for initialization
	void Start () {
        velocidad = 50f;
	}
	
	// Update is called once per frame
	void Update () {
        Movimiento();
	}

    public void Movimiento()
    {
        this.transform.Translate(velocidad * Time.deltaTime, 0, 0);
    }
}
