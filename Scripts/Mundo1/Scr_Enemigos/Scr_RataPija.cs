using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_RataPija : Scr_Gen_Enemigo {

	[SerializeField]
    private float velocidad;

	// Use this for initialization
	void Start () {
        //Fijos
		Camara = GameObject.FindGameObjectWithTag ("MainCamera");
        MeMuevo = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (MeMuevo == false)
        {
            Visto();
        }
        else
        {
            Movimiento();
        }
    }

	public override void Movimiento(){
        this.transform.Translate(-velocidad * Time.deltaTime, 0, 0);
	}
}
