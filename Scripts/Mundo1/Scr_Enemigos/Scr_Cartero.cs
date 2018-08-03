using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Cartero : Scr_Gen_Enemigo {

    [SerializeField]
    private GameObject pfCarta;

    private Animator animCartero;

    private bool ataque;

	// Use this for initialization
	void Start () {
        this.name = "Cartero";

        //Fijos
        Camara = GameObject.FindGameObjectWithTag ("MainCamera");
		MeMuevo = false;

		//Propios
		animCartero = this.GetComponent<Animator>();

        ataque = false;
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
        if(ataque == false)
        {
            ataque = true;
            animCartero.SetBool("atacando", true);
            StartCoroutine("EnumLanzaCartas");
        }
		
	}

    IEnumerator EnumLanzaCartas()
    {
        //Contador: cartas lanzadas
        //Contador2: veces que lanzará las 3 cartas
        int contador = 0;
        int contador2 = 0;
        while (contador2 < 4)
        {
            contador2++;
            while (contador < 3)
            {
                contador++;
                yield return new WaitForSeconds(0.05f);
                GameObject objCarta = Instantiate(pfCarta);
                float espacio = 1;
                objCarta.transform.position = new Vector3(this.transform.position.x-espacio, this.transform.position.y, this.transform.position.z);
                objCarta.GetComponent<Scr_Carta>().Tipo = contador;

            }
            yield return new WaitForSeconds(0.4f);
            contador = 0;
        }
    }
}
