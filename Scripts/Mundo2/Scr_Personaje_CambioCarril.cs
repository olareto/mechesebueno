using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Personaje_CambioCarril : MonoBehaviour {

    private Scr_Personaje_Movimiento scrPersMov;
    private Scr_Personaje_Colisiones scrPersCol;
    private Scr_Personaje_SaltoTrampolin scrPersTrampolin;

    private int nCambios;
    private float yInicial;

    private bool heTocado;
    private bool subida;
    private bool cambioDeCarril;
    
    

    // Use this for initialization
    void Start () {
        scrPersMov = GetComponent<Scr_Personaje_Movimiento>();
        scrPersCol = GetComponent<Scr_Personaje_Colisiones>();
        scrPersTrampolin = GetComponent<Scr_Personaje_SaltoTrampolin>();

        yInicial = this.transform.position.y;
        nCambios = 0;

        heTocado = false;
        subida = false;
        cambioDeCarril = false;     
	}
	
	// Update is called once per frame
	void Update () {
        if (scrPersMov.Intro)
        {
            //Para ordenador
            if (
                    Input.GetKeyDown("space")
                        && !GetComponent<Scr_Personaje_Colisiones>().Golpeado
                        && !GetComponent<Scr_Personaje_Movimiento>().Meta
                        && !GameObject.FindGameObjectWithTag("TagCochePayasos").GetComponent<Scr_CochePayasos>().EstaDiana
            )  // && !trampolin)
            {
                nCambios++;
                if (nCambios <= 3)
                {
                    subida = !subida;
                }
                cambioDeCarril = true;

            }

            //Para movil
            if (
                    Input.touchCount > 0
                        && !heTocado
                        && !GetComponent<Scr_Personaje_Colisiones>().Golpeado
                        && !GetComponent<Scr_Personaje_Movimiento>().Meta
                        && !GameObject.FindGameObjectWithTag("TagCochePayasos").GetComponent<Scr_CochePayasos>().EstaDiana
                ) // && !trampolin)
            {
                heTocado = true;
                nCambios++;
                if (subida)
                {
                    subida = false;
                }
                else
                {
                    subida = true;
                }
                cambioDeCarril = true;
            }

            if (Input.touchCount == 0)
            {
                heTocado = false;
            }

            if (cambioDeCarril && !scrPersMov.Meta && !scrPersCol.Golpeado && !scrPersTrampolin.Trampolin)
            {
                CambiarDeCarril();
            }
        }
        
    }

    public void CambiarDeCarril()
    {
        float velDespl = 12f;
        float yDesplazada = 7.5f;

        if (subida)
        {
            if (transform.position.y < yDesplazada)
            {
                //Vamos subiendo
                //this.transform.Translate(velDespl * Time.deltaTime/3, velDespl * Time.deltaTime, 0); //Desplazamiento un poco diagonal
                this.transform.Translate(0, velDespl * Time.deltaTime, 0);
                this.GetComponent<SpriteRenderer>().sortingOrder = -1;
            }
            else
            {
                this.transform.position = new Vector3(transform.position.x, yDesplazada, transform.position.z);
                cambioDeCarril = false;
                subida = true;
                nCambios = 0;
                this.GetComponent<SpriteRenderer>().sortingOrder = -2;
            }
        }
        else
        {
            if (transform.position.y > yInicial)
            {
                //Vamos bajando
                //this.transform.Translate(-velDespl * Time.deltaTime/3, -velDespl * Time.deltaTime, 0); //Desplazamiento un poco diagonal
                this.transform.Translate(0, -velDespl * Time.deltaTime, 0);
                this.GetComponent<SpriteRenderer>().sortingOrder = -1;
            }
            else
            {
                this.transform.position = new Vector3(transform.position.x, yInicial, transform.position.z);
                cambioDeCarril = false;
                subida = false;
                nCambios = 0;
                this.GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
        }


    }
}
