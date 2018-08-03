using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Diana : MonoBehaviour {

    [SerializeField]
    private float velocidad;

    private Transform rectanguloPadre;

    private bool sentido;
    private bool dianaTocada;
    private bool heTocado;

	// Use this for initialization
	void Start () {
        rectanguloPadre = this.transform.parent;
        dianaTocada = false;
        heTocado = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!dianaTocada)
        {
            Movimiento();

            //Para Ordenador
            if (Input.GetKeyDown("space"))
            {
                dianaTocada = true;
                Invoke("HemosTocadoDiana", 2f);
            }

            //Para movil
            if (Input.touchCount > 0 && !heTocado)
            {
                heTocado = true;
                dianaTocada = true;
                Invoke("HemosTocadoDiana", 2f);
            }
        } 
	}

    public void Movimiento()
    {
        if (sentido)
        {
            this.transform.Translate(velocidad * Time.deltaTime, 0, 0);
        } else
        {
            this.transform.Translate(-velocidad * Time.deltaTime, 0, 0);
        }

        if (this.transform.position.x >= rectanguloPadre.position.x + 6 && sentido)
        {
            sentido = false;
        }

        if (this.transform.position.x <= rectanguloPadre.position.x - 6 && !sentido)
        {
            sentido = true;
        }
    }

    public void HemosTocadoDiana()
    {
        Scr_CochePayasos scrCochePayasos = GameObject.FindGameObjectWithTag("TagCochePayasos").GetComponent<Scr_CochePayasos>();
        scrCochePayasos.EstaDiana = false;

        scrCochePayasos.Disparaste = true;

        //Si aciertas, le quitarás una vida
        scrCochePayasos.Vidas--;

        //Destruimos el cañón
        DestroyImmediate(GameObject.FindGameObjectWithTag("TagCanyon"));
        DestroyImmediate(this.transform.parent.gameObject);
    }
}
