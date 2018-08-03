using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Scr_Personaje_Colisiones1 : MonoBehaviour {

    [SerializeField]
    private GameObject pfPolvo;

    [SerializeField]
    private float tiempoInvulnerable;

    [SerializeField]
    private GameObject objSonidosMonedas;

    private Animator animMe;
    private Transform trCamara;

    private int vidas;
    private int monedas;
    private float separaciony; 

    private bool berserk;
    private bool golpeado;  
    private bool meMuevo;
    private bool invulnerable;
    private bool ataque;

    private Scr_Personaje_Movimiento1 scrPersMov;

    public bool Berserk
    {
        get
        {
            return berserk;
        }

        set
        {
            berserk = value;
        }
    }


    public bool Golpeado
    {
        get
        {
            return golpeado;
        }

        set
        {
            golpeado = value;
        }
    }

    public bool MeMuevo
    {
        get
        {
            return meMuevo;
        }

        set
        {
            meMuevo = value;
        }
    }
     public bool Ataque
    {
        get
        {
            return ataque;
        }

        set
        {
            ataque = value;
        }
    }

    public int Vidas
    {
        get
        {
            return vidas;
        }

        set
        {
            vidas = value;
        }
    }

    public int Monedas
    {
        get
        {
            return monedas;
        }

        set
        {
            monedas = value;
        }
    }

    public float Separaciony
    {
        get
        {
            return separaciony;
        }

        set
        {
            separaciony = value;
        }
    }


    // Use this for initialization
    void Start () {
        ataque=false;
        animMe = GetComponent<Animator>();
        trCamara = GameObject.FindGameObjectWithTag("MainCamera").transform;

        vidas = 3;
        if (SceneManager.GetActiveScene().name == "Esc_Mundo1_3")
        {
            vidas = 1;
        }
        monedas = 0;
        separaciony = trCamara.position.y - this.transform.position.y ;

        berserk = false;
        golpeado = false;    
        meMuevo = true;
        invulnerable = false;

        scrPersMov = GetComponent<Scr_Personaje_Movimiento1>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.y < trCamara.position.y - separaciony)
        {
            if (SceneManager.GetActiveScene().buildIndex <= 3)
            {
                Desplazado();
            } 
        }
    }

    public void OnTriggerEnter2D(Collider2D colTr)
    {

        if (!scrPersMov.Meta)
        {
            string nombreColTr = colTr.gameObject.name;





//////////

             if (nombreColTr == "Enemigo"){
                if(ataque){
                    Destroy(colTr.gameObject);
                }
                else{
                    Golpe();
                }

            }
            /////


            

            if (nombreColTr == "Moneda")
            {
                Destroy(colTr.gameObject);
                monedas++;
                SonidoMonedas();
            }

            if (nombreColTr == "Botiquin")
            {
                ReproduceSonido(3); //Sardina
                Destroy(colTr.gameObject);
                vidas++;
            }





            

            if (nombreColTr == "PowerUP")
            {
                ReproduceSonido(2); //Berserk
                Destroy(colTr.gameObject);
                berserk = true;
                this.GetComponent<Animator>().SetInteger("TransMe", 6); //Berserk

                scrPersMov.Velocidad *= 2;
            }

            if (nombreColTr == "Vacio")
            {
                ReproduceSonido(4); //CaidaAlVacio
                golpeado = true;
                vidas = 0;
                ParaSonido(0); //Tema principal
                meMuevo = false;
            }

            if (nombreColTr == "BaseCamion")
            {
                //Ya no puedo saltar encima
                colTr.GetComponentsInParent<Collider2D>()[1].enabled = false;
                colTr.GetComponentsInParent<Collider2D>()[2].enabled = false;
            }

            if (nombreColTr == "TuberiaJefeBase")
            {
                //Ya no puedo saltar encima
                colTr.GetComponentsInParent<Collider2D>()[1].enabled = false;
            }

            if (!berserk)
            {
                if (nombreColTr == "TuberiaJefe")
                {
                    colTr.gameObject.GetComponent<Scr_Tuberia>().LanzarAgua();
                }

                if (!golpeado &&
                    (
                        nombreColTr == "Periodico" ||
                        nombreColTr == "Carta" ||
                        nombreColTr == "RataPija" ||
                        nombreColTr == "Pajaro" ||
                        nombreColTr == "Perro" ||
                        nombreColTr == "GatoMalo" ||
                        nombreColTr == "Cartero" ||
                        nombreColTr == "BaseCaja" ||
                        nombreColTr == "ArbustoMalo" ||
                        nombreColTr == "Jefe_1"||

                        nombreColTr == "Coche" ||
                        nombreColTr == "Camion" ||
                        nombreColTr == "Ratamotera" ||
                        nombreColTr == "Fuego"
                    )
                )
                {
                    if (!invulnerable)
                    {
                        Golpe();
                    }

                    //Ya no puedo saltar encima
                    if (nombreColTr == "BaseCaja")
                    {
                        colTr.GetComponentsInParent<Collider2D>()[1].enabled = false;
                    }
                }
            }
            else
            {
                //Modo BERSERK
                if (!golpeado &&
                    (
                        nombreColTr == "Carta" ||
                        nombreColTr == "RataPija" ||
                        nombreColTr == "Pajaro" ||
                        nombreColTr == "Perro" ||
                        nombreColTr == "GatoMalo" ||
                        nombreColTr == "Cartero"
                    )
                )
                {
                    Destroy(colTr.gameObject);
                    GameObject objPolvo = Instantiate(pfPolvo);
                    objPolvo.transform.position = new Vector3(colTr.transform.position.x, colTr.transform.position.y, colTr.transform.position.z);
                }
            }
        }
    }

    //Colisiones
    public void OnCollisionEnter2D(Collision2D colisionador)
    {
        string nombreColisionador = colisionador.gameObject.name;

        if (nombreColisionador == "Suelo")
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            if (!golpeado && !berserk)
            {
                CambiaAnimacion(0); //Corriendo
            }
        }

        if (
                nombreColisionador == "Alfeizar" ||
                nombreColisionador == "Tejado" ||
                nombreColisionador == "Caja" ||
                nombreColisionador == "Caja2" ||
                nombreColisionador == "CajaMadera"
            )
        {
            if (!berserk)
            {
                CambiaAnimacion(0); //Corriendo
            }
        }
    }


    public void Golpe()
    {
        

        golpeado = true;
        vidas--;
        CambiaAnimacion(4); //Dañado
        ReproduceSonido(1); //Daño


        invulnerable = true;

        if (vidas == 0)
        {
            ParaSonido(0); //Tema principal
            CambiaAnimacion(5); //Muerto
        }
        else
        {
            StartCoroutine("EnumInvulnerable");

            Invoke("YaMeMuevo", 1.5f);
        }
    }

    public void YaMeMuevo()
    {
        CambiaAnimacion(0); //Corriendo

        meMuevo = true;
        golpeado = false;
    }

    public IEnumerator EnumInvulnerable()
    {
        Color colorInv = this.GetComponent<SpriteRenderer>().color;
        colorInv.a = 0;
        Color colorVis = this.GetComponent<SpriteRenderer>().color;
        colorVis.a = 1;
        print("acabado");
        float momentoGolpe = Time.time;
        float tiempoAlpha = 0.1f;
        while (Time.time - momentoGolpe < tiempoInvulnerable)
        {
            this.GetComponent<SpriteRenderer>().color = colorInv;
            if (Time.time - momentoGolpe < tiempoInvulnerable)
            {
                yield return new WaitForSeconds(tiempoAlpha);
            }

            this.GetComponent<SpriteRenderer>().color = colorVis;
            if (Time.time - momentoGolpe < tiempoInvulnerable)
            {
                yield return new WaitForSeconds(tiempoAlpha);
            }
        }

        invulnerable = false;
        
    }

    public void Desplazado()
    {
        float margenDesplazamiento = 1.2f;
        if (this.transform.position.y < trCamara.position.y - separaciony - margenDesplazamiento)
        {
            if (!invulnerable && !berserk && !scrPersMov.Meta)
            {
                Golpe();
            }
        } else
        {
            Invoke("VolverASituar",0.4f);
        }
    }

    public void VolverASituar()
    {
        if (!golpeado)
        {            

            this.transform.position = new Vector3(trCamara.position.x, this.transform.position.y-separaciony, this.transform.position.z);
        }
        
    }

    public void ReproduceSonido(int numeroSonido)
    {
        this.GetComponents<AudioSource>()[numeroSonido].Play();
    }

    public void ParaSonido(int numeroSonido)
    {
        this.GetComponents<AudioSource>()[numeroSonido].Stop();
    }

    public void SonidoMonedas(){
        GameObject objSonidoMoneda = Instantiate(objSonidosMonedas);
        objSonidoMoneda.GetComponents<AudioSource>()[monedas%6].Play();
    }

    public void CambiaAnimacion(int transicion)
    {
        if (SceneManager.GetActiveScene().buildIndex <= 3)
        {
            this.animMe.SetInteger("TransMe", transicion);
        }    
    }
}
