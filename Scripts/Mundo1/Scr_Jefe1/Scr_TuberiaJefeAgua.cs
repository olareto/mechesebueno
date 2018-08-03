using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TuberiaJefeAgua : MonoBehaviour {

    [SerializeField]
    private float velocidad;

    // Use this for initialization
    void Start () {
        this.name = "TuberiaJefeAgua";
	}
	
	// Update is called once per frame
	void Update () {
        //Tambien falta Rotar
        Movimiento();
        
    }

    public void Movimiento()
    {
        this.transform.position = new Vector3(transform.position.x + velocidad * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
