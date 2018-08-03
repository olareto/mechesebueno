using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Jefe1_Intro : MonoBehaviour {

    [SerializeField]
    private GameObject espada;

    [SerializeField]
    float velocidad;

    private GameObject personaje;
    private AudioSource sonidoDanyo;
    private Animator animMecheese;

    private bool parado;
    private bool choque;
    private bool chocado;
    private bool paradaEspada;
    private bool espadaCogida;
    private bool nosHemosIdo;
    private bool finIntro;

    // Use this for initialization
    void Start () {
        personaje = GameObject.FindGameObjectWithTag("Player");
        sonidoDanyo = personaje.GetComponents<AudioSource>()[1];
        animMecheese = personaje.GetComponent<Animator>();

        parado = false;
        choque = false;
        chocado = false;
        paradaEspada = false;
        espadaCogida = false;
        nosHemosIdo = false;
        finIntro = false;
        
        
    }
	
	// Update is called once per frame
	void Update () {
        if (parado == false || espadaCogida == true)
        {
            Movimiento();
        }

        if (choque == true)
        {
            if(chocado == false)
            {
                //Choque se hace una sola vez
                Choque();
            }
            else
            {
                if (paradaEspada == false)
                {
                    MovimientoEspada();
                }
                else
                {
                    if (espadaCogida == false)
                    {
                        CogemosEspada();
                    }
                }
            }
                
        }

        if (nosHemosIdo == true)
        {
            Resurreccion();
        }

        if (finIntro)
        {
            personaje.GetComponent<Scr_Personaje_Movimiento>().FinIntro();
            GameObject.Destroy(this.gameObject);
        }
    }



    public void Movimiento()
    {
        float distPararse = 20;
        this.transform.Translate(velocidad * Time.deltaTime, 0, 0);
        if (this.transform.position.x >= personaje.transform.position.x)
        {
            choque = true;
        }

        if (choque == false)
        {
            float vel = 15f;
            GameObject objCamara = GameObject.FindGameObjectWithTag("MainCamera");

            personaje.transform.Translate(vel * Time.deltaTime, 0, 0);
            objCamara.transform.Translate(vel * Time.deltaTime, 0, 0);
        }

        if (this.transform.position.x >= personaje.transform.position.x + distPararse)
        {
            if (espadaCogida == false)
            {
                parado = true;
            }
            else
            {
                parado = false;

                //Ya nos hemos ido lejos
                if (this.transform.position.x >= personaje.transform.position.x + distPararse * 1.5)
                {
                    nosHemosIdo = true;
                }

                if (this.transform.position.x >= personaje.transform.position.x + distPararse * 2)
                {
                    finIntro = true;
                }
            }         
        }

    }

    public void MovimientoEspada()
    {
        float velocidad = 9f;
        float distPararse = 20f;

        //Horizontal
        espada.transform.position = new Vector3(espada.transform.position.x + velocidad * Time.deltaTime, espada.transform.position.y, espada.transform.position.z);
        if (espada.transform.position.x >= personaje.transform.position.x + distPararse)
        {
            paradaEspada = true;
        }

        //Vertical
        if (espada.transform.position.x <= personaje.transform.position.x + (distPararse / 2))
        {
            espada.transform.position = new Vector3(espada.transform.position.x, espada.transform.position.y + velocidad * Time.deltaTime, espada.transform.position.z);
        }
        else
        {
            espada.transform.position = new Vector3(espada.transform.position.x, espada.transform.position.y - velocidad * Time.deltaTime, espada.transform.position.z);
        }

        //Rotacion
        espada.transform.Rotate( new Vector3(0, 0, 10));
    }

    public void Choque()
    {
        espada = Instantiate(espada);
        espada.transform.position = new Vector3 (transform.position.x, transform.position.y + 2, transform.position.z);
        sonidoDanyo.Play();
        animMecheese.SetInteger("TransMe", 5); //Muerto  //personaje.GetComponent<Scr_Personaje>().CambiaAnimacion(5); //Muerto
        chocado = true;
    }

    public void CogemosEspada()
    {
        GameObject.Destroy(espada.gameObject);
        this.GetComponentsInChildren<Animator>()[1].SetBool("cogido", true);
        espadaCogida = true;
    }

    public void Resurreccion()
    {
        animMecheese.SetInteger("TransMe", 1); //Salto
    }
}
