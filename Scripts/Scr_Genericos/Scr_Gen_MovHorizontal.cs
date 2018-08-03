using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Gen_MovHorizontal : MonoBehaviour {

    [SerializeField]
    private bool haciaDerecha;

    [SerializeField]
    private float velocidad;

    [SerializeField]
    private float distanciaParaActuar;

    private GameObject camara;

	// Use this for initialization
	void Start () {
        camara = GameObject.FindGameObjectWithTag("MainCamera");
    }
	
	// Update is called once per frame
	void Update () {
        CompruebaDistancia();
    }

    public void Movimiento()
    {
        if (haciaDerecha)
        {
            this.transform.Translate(velocidad * Time.deltaTime, 0, 0);
        } else
        {
            this.transform.Translate(-velocidad * Time.deltaTime, 0, 0);
        }
    }

    public void CompruebaDistancia()
    {
        float distanciaAcamara = this.transform.position.x - camara.transform.position.x;
        if (distanciaAcamara < distanciaParaActuar)
        {
            Movimiento();
        }
    }
}
