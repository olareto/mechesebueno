using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_Personaje_Movimiento : MonoBehaviour {

    [SerializeField]
    private float velocidad;

    [SerializeField]
    private float posicionMeta; //1550 en Mundo1_1, 1600 en Mundo1_2

    private Scr_Personaje_Colisiones scrPersCol;
    private Scr_Personaje_SaltoTrampolin scrPersTrampolin;
    private GameObject objCamara;
    private Rigidbody2D rbMe;
    private Animator animMe;

    private float yInicialCamara;
    private bool intro;
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
    

    public bool Intro
    {
        get
        {
            return intro;
        }

        set
        {
            intro = value;
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

        scrPersCol = this.GetComponent<Scr_Personaje_Colisiones>();
        scrPersTrampolin = this.GetComponent<Scr_Personaje_SaltoTrampolin>();

        if (SceneManager.GetActiveScene().name != "Esc_Mundo1_1")
        {
            Invoke("FinIntro", 1.0f);
        }

        yInicialCamara = objCamara.transform.position.y;

        intro = false;
        meta = false;

        if (SceneManager.GetActiveScene().buildIndex <= 3)
        {
            this.animMe.SetInteger("TransMe", 0); //Corriendo 
        } else
        {
            animMe.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (intro)
        {
            MovimientoCamaraYMe();
        }

        if (rbMe.velocity.y < -0.1 && !scrPersCol.Golpeado && !scrPersCol.Berserk)
        {
            if (SceneManager.GetActiveScene().buildIndex <= 3)
            {
                this.animMe.SetInteger("TransMe", 3); //Cayendo
            }
        }
    }

    public void MovimientoCamaraYMe()
    {
        //Movimiento horizontal
        if (scrPersCol.MeMuevo)
        {
            if (SceneManager.GetActiveScene().buildIndex <= 3)
            {
                //MeCheese
                this.transform.Translate(velocidad * Time.deltaTime, 0, 0);
                if (this.transform.position.x >= posicionMeta && posicionMeta != -1)
                {
                    LlegoMeta();
                }

                //Camara
                if (!meta && scrPersCol.Vidas >= 1)
                {
                    objCamara.transform.Translate(velocidad * Time.deltaTime, 0, 0);
                }
            } else
            {
                //MeCheese
                if (!scrPersTrampolin.Trampolin)
                {
                    this.transform.Translate(velocidad * Time.deltaTime, 0, 0);
                }
                else
                {

                    if (scrPersTrampolin.SubidaTrampolin)
                    {
                        this.transform.Translate(velocidad * Time.deltaTime * 1.2f, 0, 0);
                    }
                    else
                    {
                        //Corregimos el desplazamiento de la camara por usar el trampolin
                        this.transform.position = new Vector2(objCamara.transform.position.x - scrPersCol.Separacionx, this.transform.position.y);
                        rbMe.gravityScale = 2f;

                        //this.transform.Translate(velocidad * Time.deltaTime * 1.2f, 0, 0);
                        this.transform.position = new Vector3(transform.position.x + velocidad * Time.deltaTime, transform.position.y, transform.position.z);
                    }

                }

                if (this.transform.position.x >= posicionMeta && posicionMeta != -1)
                {
                    LlegoMeta();
                }

                //Camara
                if (!meta && scrPersCol.Vidas >= 1)
                {
                    if (!scrPersTrampolin.Trampolin)
                    {
                        objCamara.transform.Translate(velocidad * Time.deltaTime, 0, 0);
                    }
                    else
                    {
                        objCamara.transform.Translate(velocidad * Time.deltaTime, 0, 0);

                    }
                }
            }
                

        }

        if (SceneManager.GetActiveScene().buildIndex <= 3)
        {
            //Movimiento vertical
            //Camara
            bool despVertCamaraSubida = false;
            bool despVertCamaraBajada = false;
            float velDesplCamara = 50f;
            float alturaCambio = yInicialCamara + 11f; //18f en el Mundo1_1;
            float delta = 0.8f;
            if (this.transform.position.y > alturaCambio)
            {
                //Subiremos hasta seguir a MeCheese
                if (objCamara.transform.position.y < this.transform.position.y - delta)
                {
                    despVertCamaraSubida = true;
                }
                else
                {
                    despVertCamaraSubida = false;
                }

                if (despVertCamaraSubida)
                {
                    objCamara.transform.Translate(0, velDesplCamara * Time.deltaTime, 0);
                }

                if (!despVertCamaraSubida)
                {
                    //Sigue a MeCheese
                    objCamara.transform.position = new Vector3(objCamara.transform.position.x, this.transform.position.y, objCamara.transform.position.z);
                }
            }
            else
            {
                //Bajaremos hasta la yInicialCamara
                if (objCamara.transform.position.y > yInicialCamara + delta)
                {
                    if (this.transform.position.y < alturaCambio / 2)
                    {
                        if (objCamara.transform.position.y > yInicialCamara + delta * 2)
                        {
                            despVertCamaraBajada = true;
                        }
                        else
                        {
                            //Llegamos a la yInicialCamara
                            despVertCamaraBajada = false;
                            objCamara.transform.position = new Vector3(objCamara.transform.position.x, yInicialCamara, objCamara.transform.position.z);
                        }

                        if (despVertCamaraBajada)
                        {
                            objCamara.transform.Translate(0, -velDesplCamara * Time.deltaTime, 0);
                        }
                    }
                    else
                    {
                        //Sigue a MeCheese
                        objCamara.transform.position = new Vector3(objCamara.transform.position.x, this.transform.position.y, objCamara.transform.position.z);
                    }
                }
            }
        
        }
    }

    public void FinIntro()
    {
        intro = true;
        this.GetComponents<AudioSource>()[0].Play(); //Sonido(0); //Tema principal
        if (SceneManager.GetActiveScene().buildIndex <= 3)
        {
            this.animMe.SetInteger("TransMe", 0); //Corriendo
        }
    }

    public void LlegoMeta()
    {
        meta = true;
        this.GetComponents<AudioSource>()[0].Stop(); //Tema principal
    }
}
