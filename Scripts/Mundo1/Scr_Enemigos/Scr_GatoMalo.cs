using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GatoMalo : Scr_Gen_Enemigo {

	[SerializeField]
    private float velocidad;

    private Animator animGato;

    private bool hasHechoSonido;
    

	// Use this for initialization
	void Start () {
        this.name = "GatoMalo";

        //Fijos
        Camara = GameObject.FindGameObjectWithTag ("MainCamera");
		MeMuevo = false;

		//Propios
		animGato = this.GetComponent<Animator> ();
        hasHechoSonido = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (MeMuevo == false)
        {
            Visto();
        }
        else
        {
            if (hasHechoSonido == false)
            {
                HazSonido();
            }
            
            Movimiento();
        }
    }

	public override void Movimiento(){
		this.transform.Translate(-velocidad * Time.deltaTime,0,0);
		animGato.SetBool ("visto", true);
	}

    public void HazSonido()
    {
        this.GetComponent<AudioSource>().Play();
        hasHechoSonido = true;
    }
}
