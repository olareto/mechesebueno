using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Coche : MonoBehaviour {

    [SerializeField]
    private float velocidad;

    [SerializeField]
    private float distanciaParaActuar;

    [SerializeField]
    private int patron;

    private bool estaArriba;

    private GameObject camara;
    private GameObject objMecheese;

    
    
    // Use this for initialization
    void Start () {
        camara = GameObject.FindGameObjectWithTag("MainCamera");
        objMecheese = GameObject.FindGameObjectWithTag("Player");

        if (this.transform.position.y > 6)
        {
            estaArriba = true;
            GetComponent<SpriteRenderer>().sortingOrder = -2;
        } else
        {
            estaArriba = false;
            GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
        CompruebaDistancia();
    }

    public void Movimiento()
    {
        //Sigue recto 
        if (patron == 1)
        {
            this.transform.Translate(-velocidad * Time.deltaTime, 0, 0);
        }

        //Se cambia de carril
        if(patron == 2)
        {
            //Movimiento hacia delante
            this.transform.Translate(-velocidad * Time.deltaTime, 0, 0);

            float distanciaAcamara = this.transform.position.x - camara.transform.position.x;
            float velocidadCambio = 10f;

            //Cambio de carril
            if (!estaArriba)
            {
                if (distanciaAcamara < 10 && this.transform.position.y <= 8.2f)
                {
                    //Subo
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + velocidadCambio * Time.deltaTime, this.transform.position.z);
                    GetComponent<SpriteRenderer>().sortingOrder = -1;
                    if (this.transform.position.y > objMecheese.transform.position.y)
                    {
                        GetComponent<SpriteRenderer>().sortingOrder = -2;
                    }
                }
            } else
            {
                if (distanciaAcamara < 10 && this.transform.position.y >= 4.2)
                {
                    //Bajo
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - velocidadCambio * Time.deltaTime, this.transform.position.z);
                    GetComponent<SpriteRenderer>().sortingOrder = -1;
                    if (this.transform.position.y < objMecheese.transform.position.y)
                    {
                        GetComponent<SpriteRenderer>().sortingOrder = 0;
                    }
                }
            }     
        }

        //Espejo
        if (patron == 3)
        {
            this.transform.Translate(-velocidad * Time.deltaTime, 0, 0);
            float distanciaAcamara = this.transform.position.x - camara.transform.position.x;

            if (distanciaAcamara > 0)
            {
                //Misma altura que Mecheese
                this.transform.position = new Vector3(this.transform.position.x, objMecheese.transform.position.y+0.7f, this.transform.position.z);
                //Misma profundidad que Mecheese
                GetComponent<SpriteRenderer>().sortingOrder = objMecheese.GetComponent<SpriteRenderer>().sortingOrder;
            } else
            {
                //Profundidad correcta
                if (this.transform.position.y > objMecheese.transform.position.y)
                {
                    GetComponent<SpriteRenderer>().sortingOrder = objMecheese.GetComponent<SpriteRenderer>().sortingOrder-1;
                } else
                {
                    GetComponent<SpriteRenderer>().sortingOrder = objMecheese.GetComponent<SpriteRenderer>().sortingOrder + 1;
                }
                
            }
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
