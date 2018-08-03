using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Misil : MonoBehaviour {

    [SerializeField]
    private float velocidad;

    //private GameObject personaje;

    //private int tipo;
    private bool subida;
    private bool bajada;

    public bool Subida
    {
        get { return subida; }
        set { subida = value; }
    }

    public bool Bajada
    {
        get { return bajada; }
        set { bajada = value; }
    }


    // Use this for initialization
    void Start() {
        this.name = "Misil";
        //personaje = GameObject.FindGameObjectWithTag("Player");

        //tipo = 0;
        //subida = false;
        //bajada = false;
    }

    // Update is called once per frame
    void Update() {
        Movimiento();
    }

    public void Movimiento()
    {
        //Movimiento horizontal
        this.transform.Translate(velocidad * Time.deltaTime, 0, 0);

        //Movimiento vertical
        //Seguir a Mecheese
        //this.transform.position = new Vector3 (this.transform.position.x, personaje.transform.position.y+0.5f, this.transform.position.z) ;
        
        float yDesplazada = 6.84f;
        float velDespl = 9f;
        float yInicial = 2.84f;

        //Subida
        if (subida && !bajada)
        {
            if (transform.position.y < yDesplazada)
            {
                //Vamos subiendo
                //this.transform.Translate(velDespl * Time.deltaTime/3, velDespl * Time.deltaTime, 0); //Desplazamiento un poco diagonal
                this.transform.Translate(0, -velDespl * Time.deltaTime, 0);
                this.GetComponent<SpriteRenderer>().sortingOrder = -1;
            }
            else
            {
                this.transform.position = new Vector3(transform.position.x, yDesplazada, transform.position.z);
                subida = false;
                this.GetComponent<SpriteRenderer>().sortingOrder = -2;
            }
        }

        //Bajada
        if (!subida && bajada)
        {
            if (transform.position.y > yInicial)
            {
                //Vamos bajando
                //this.transform.Translate(-velDespl * Time.deltaTime/3, -velDespl * Time.deltaTime, 0); //Desplazamiento un poco diagonal
                this.transform.Translate(0, velDespl * Time.deltaTime, 0);
                this.GetComponent<SpriteRenderer>().sortingOrder = -1;
            }
            else
            {
                this.transform.position = new Vector3(transform.position.x, yInicial, transform.position.z);
                bajada = false;
                this.GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
        }
 
    }
}
