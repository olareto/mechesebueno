using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_Personaje_Movimiento1 : MonoBehaviour {

    [SerializeField]
    private float velocidad;

    [SerializeField]
    private float posicionMeta; //1550 en Mundo1_1, 1600 en Mundo1_2

    private Scr_Personaje_Colisiones1 scrPersCol;
    private GameObject objCamara;
    private Rigidbody2D rbMe;
    private Animator animMe;

    private float yInicialCamara;
    private bool meta;


    public bool Meta
    {
        get
        {
            return meta;
        }

        set
        {
            meta = value;
        }
    }
    


    public float Velocidad
    {
        get
        {
            return velocidad;
        }

        set
        {
            velocidad = value;
        }
    }

    // Use this for initialization
    void Start () {
        objCamara = GameObject.FindGameObjectWithTag("MainCamera");
        

        rbMe = this.GetComponent<Rigidbody2D>();
        animMe = this.GetComponent<Animator>();

        scrPersCol = this.GetComponent<Scr_Personaje_Colisiones1>();

        if (SceneManager.GetActiveScene().name != "Esc_Mundo1_1")
        {
            Invoke("FinIntro", 1.0f);
        }

        yInicialCamara = objCamara.transform.position.y;

        meta = false;

        this.animMe.SetInteger("TransMe", 0); //Corriendo 
	}
	
	// Update is called once per frame
	void Update () {
        MovimientoCamaraYMe();

        if (rbMe.velocity.y < -0.1 && !scrPersCol.Golpeado && !scrPersCol.Berserk)
        {
        	this.animMe.SetInteger("TransMe", 3); //Cayendo
        }
    }

    public void MovimientoCamaraYMe()
    {
        //Movimiento horizontal
        if (scrPersCol.MeMuevo)
        {
           
            //MeCheese
            //this.transform.Translate(0, -velocidad * Time.deltaTime, 0);
            //if (this.transform.position.x >= posicionMeta && posicionMeta != -1)
            //{
              //  LlegoMeta();
            //}

            //Camara
            //if (!meta && scrPersCol.Vidas >= 1)
            //{
              //  objCamara.transform.Translate(0, -velocidad * Time.deltaTime, 0);
            //}
             
                

        }

        
    }

    public void FinIntro()
    {
        this.GetComponents<AudioSource>()[0].Play(); //Sonido(0); //Tema principal
        this.animMe.SetInteger("TransMe", 0); //Corriendo
    }

    public void LlegoMeta()
    {
        meta = true;
        this.GetComponents<AudioSource>()[0].Stop(); //Tema principal
    }
}
