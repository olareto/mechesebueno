using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Periodico : MonoBehaviour {

    [SerializeField]
    private float velocidad;

    private GameObject personaje;

    private bool heEstadoDetras;
    private bool heEsperado;

    private int tipo;

    public int Tipo
    {
        get { return tipo; }
        set { tipo = value; }
    }



    // Use this for initialization
    void Start () {
        this.name = "Periodico";
        personaje = GameObject.FindGameObjectWithTag("Player");

        heEstadoDetras = false;
        heEsperado = false;
    }
	
	// Update is called once per frame
	void Update () {
        Movimiento();
    }

    public void Movimiento()
    {
        float distRetroceso0 = 5f;
        float distRetroceso1 = 0f;
        float distRetroceso2 = -5f;

        if (!heEstadoDetras && !heEsperado)
        {
            if ( 
                    (tipo == 0 && this.transform.position.x <= personaje.transform.position.x - distRetroceso0) ||
                    (tipo == 1 && this.transform.position.x <= personaje.transform.position.x - distRetroceso1) ||
                    (tipo == 2 && this.transform.position.x <= personaje.transform.position.x - distRetroceso2)
               )
            {
                heEstadoDetras = true;
                StartCoroutine("EnumEsperar");
            } else
            {
                this.transform.position = new Vector3(transform.position.x - velocidad * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }

        if (heEstadoDetras)
        {
            if (heEsperado)
            {
                this.transform.position = new Vector3(transform.position.x + velocidad * 2 * Time.deltaTime, transform.position.y, transform.position.z);
            } else
            {
                this.transform.position = new Vector3(transform.position.x - velocidad * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }
       
    
        //Rotacion
        this.transform.Rotate(new Vector3(0, 0, 20));
    }

    public IEnumerator EnumEsperar()
    {
        yield return new WaitForSeconds(0.2f);
        heEsperado = true;
    }
}
