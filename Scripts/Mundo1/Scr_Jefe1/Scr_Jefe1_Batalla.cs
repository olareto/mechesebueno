using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Jefe1_Batalla : MonoBehaviour
{

    [SerializeField]
    private GameObject bomba;

    [SerializeField]
    private GameObject periodico;

    [SerializeField]
    private GameObject tuberia;

    [SerializeField]
    private GameObject jefeMojado;

    [SerializeField]
    private float velocidad;

    
    private int numEmbestidas;

    

    private Animator animBrazo;
    private Animator animMochila;

    private GameObject personaje;

    private bool intro;
    private bool embestida;
    private bool embestidaFinal;
    private bool heEstadoDetras;
    private bool heEsperado;
    private bool mojado;

    private int patronBomba;
    private int patronEmbestida;
    private int vidas;

    // Use this for initialization
    void Start()
    {
        intro = false;
        embestida = false;
        embestidaFinal = false;
        heEstadoDetras = false;
        heEsperado = false;
        mojado = false;

        patronBomba = 0;
        patronEmbestida = 0;
        vidas = 3;

        //StartCoroutine("FinIntro");

        Invoke("FinIntro", 1.0f);

        //animBrazo = this.GetComponentsInChildren<Animator>()[1];
        //animMochila = this.GetComponentsInChildren<Animator>()[2];
        personaje = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine("EnumTirarBombas");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Embestida();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            LanzarPeriodico(1);
        }*/

        if (vidas > 0)
        {
            if (intro)
            {
                if (!embestida && !embestidaFinal && !mojado)
                {
                    Movimiento();
                }

                if (embestida)
                {
                    MovimientoEmbestida();
                }

                if (embestidaFinal)
                {
                    MovimientoEmbestidaFinal();
                }

                if (mojado)
                {
                    MovimientoMojado();
                }

            }
        } 
        
    }

    public void Movimiento()
    {
        //Si golpeo al Personaje se descuadra
        this.transform.Translate(velocidad * Time.deltaTime, 0, 0);
    }

    public void MovimientoEmbestida()
    {
        float distEsperandoAtaque = 6f;
        float distPararPeq = 12f;
        float distPararGrande = 16f;
        float distFinal = 80f;
        float distTuberia = 39f; //58 si distFinal = 120, 39 si distFinal = 80

        if (heEstadoDetras && heEsperado)
        {
            if (patronEmbestida >= numEmbestidas && this.transform.position.x >= personaje.transform.position.x + distFinal)
            {
                embestida = false;
                //Preparamos la embestida final
                heEstadoDetras = false;
                GameObject tuberiaInst = Instantiate(tuberia);
                tuberiaInst.transform.position = new Vector3(this.transform.position.x - distTuberia, tuberiaInst.transform.position.y, tuberiaInst.transform.position.z);
                //this.transform.Translate(50f, 0, 0);
                this.transform.Rotate(0,180,0); //Nos damos media vuelta 
                embestidaFinal = true;
                
            }

            if ((patronEmbestida < numEmbestidas) &&
                    (
                        (patronEmbestida % 2 == 0 && this.transform.position.x >= personaje.transform.position.x + distPararGrande) ||
                        (patronEmbestida % 2 == 1 && this.transform.position.x >= personaje.transform.position.x + distPararPeq)
                    )
               )
            {
                heEstadoDetras = false;
                heEsperado = false;
                patronEmbestida++;
            }
        }

        if (!heEstadoDetras && !heEsperado && this.transform.position.x <= personaje.transform.position.x - distEsperandoAtaque)
        {
            heEstadoDetras = true;
            StartCoroutine("EnumEsperaEmbestir");
        }

        if (heEstadoDetras)
        {
            if (!heEsperado)
            {
                this.transform.Translate(velocidad * Time.deltaTime, 0, 0);
            } else
            {
                this.transform.Translate(velocidad * 2 * Time.deltaTime, 0, 0);
            }
        }
    }

    public void MovimientoEmbestidaFinal()
    {
        float distOriginal = 25f;

        //Detras del personaje
        if (!heEstadoDetras)
        {
            if (this.transform.position.x >= personaje.transform.position.x - 20)
            {
                //Como ha rotado va en el sentido contrario
                this.transform.Translate(velocidad * Time.deltaTime, 0, 0);
            } else
            {
                heEstadoDetras = true;
                this.transform.Rotate(0, -180, 0); //Nos damos media vuelta
            }
        }

        if (heEstadoDetras)
        {
            if (this.transform.position.x < personaje.transform.position.x + distOriginal)
            {
                //Como ha vuelto a rotar va en el sentido correcto
                this.transform.Translate(velocidad * 2 * Time.deltaTime, 0, 0);
            } else
            {
                //Fin de la embestida final
                embestidaFinal = false;
                this.transform.position = new Vector3(personaje.transform.position.x + distOriginal, this.transform.position.y, this.transform.position.z);
                StartCoroutine("EnumEsperaLanzaBombas");
            }
        }       
    }

    
    public void MovimientoMojado()
    {
        float distOriginal = 25f;

        if (!heEsperado)
        {
            if (this.transform.position.x <= personaje.transform.position.x + 100)
            {
                //Como ha rotado va en el sentido contrario
                this.transform.Translate(-velocidad * 3 * Time.deltaTime, 0, 0);
            } else
            {
                heEsperado = true;
                this.transform.Rotate(0, -180, 0); //Nos damos media vuelta
                vidas--;
                if (vidas == 0)
                {
                    personaje.GetComponent<Scr_Personaje_Movimiento>().LlegoMeta();
                }
            }
        } else
        {
            if (this.transform.position.x <= personaje.transform.position.x + distOriginal)
            {
                mojado = false;
                StartCoroutine("EnumTirarBombas");
            }
        }
    }


    public void TirarBomba(bool _esGrande)
    {
        //animMochila.SetBool("atacando", true);
        GameObject bombaInstancia = Instantiate(bomba);
        bombaInstancia.transform.position = new Vector3(this.transform.position.x, bombaInstancia.transform.position.y, bombaInstancia.transform.position.z);

        bombaInstancia.GetComponent<Scr_Bomba>().EsGrande = _esGrande;
    }

    public void LanzarPeriodico(int _tipo)
    {
        GameObject periodicoInstancia = Instantiate(periodico);
        periodicoInstancia.transform.position = new Vector3(this.transform.position.x, periodicoInstancia.transform.position.y, periodicoInstancia.transform.position.z);
        periodicoInstancia.GetComponent<Scr_Periodico>().Tipo = _tipo;
    }

    //Desde detrás y luego marcha atrás
    public void Embestida()
    {
        heEstadoDetras = false;
        heEsperado = false;
        patronEmbestida = 0;
        embestida = true;

        if (vidas == 3)
        {
            numEmbestidas = 2;
        }

        if (vidas == 2)
        {
            numEmbestidas = 3;
        }

        if (vidas == 1)
        {
            numEmbestidas = 4;
        }

        //numEmbestidas = 0; //Para pruebas

        //animBrazo.SetBool("atacando", true);
        //Esperamos 1 segundo
        //animBrazo.SetBool("atacando", false);
    }

    /*public IEnumerator FinIntro()
    {
        yield return new WaitForSeconds(1f);
        intro = true;

        //Comienzo Primer ataque
        //yield return new WaitForSeconds(1f);
        StartCoroutine("EnumTirarBombas");
    }*/

    public void FinIntro()
    {
        intro = true;
        StartCoroutine("EnumTirarBombas");
    }

    public IEnumerator EnumTirarBombas()
    {
        float segundosPeq = 0.9f; //pequeño
        float segundosGrande = 1.25f; //grande
        int numSeriesDeBombas = 0;
        
        if (vidas == 3)
        {
            numSeriesDeBombas = 1;
        }

        if (vidas == 2)
        {
            numSeriesDeBombas = 2;
        }

        if (vidas == 1)
        {
            numSeriesDeBombas = 3;
        }

        //numSeriesDeBombas = 0; //Para pruebas

        patronBomba = 0;
        int contador = 0;
        while (contador < numSeriesDeBombas)
        {
            if (patronBomba % 6 == 0)
            {
                TirarBomba(false);
                yield return new WaitForSeconds(segundosPeq);
                TirarBomba(true);
                yield return new WaitForSeconds(segundosGrande);
                TirarBomba(false);
                yield return new WaitForSeconds(segundosPeq);
            }

            if (patronBomba % 6 == 1)
            {
                TirarBomba(true);
                yield return new WaitForSeconds(segundosGrande);
                TirarBomba(false);
                yield return new WaitForSeconds(segundosPeq);
                TirarBomba(true);
                yield return new WaitForSeconds(segundosGrande);
            }

            if (patronBomba % 6 == 2)
            {
                TirarBomba(false);
                yield return new WaitForSeconds(segundosPeq);
                TirarBomba(false);
                yield return new WaitForSeconds(segundosPeq);
                TirarBomba(true);
                yield return new WaitForSeconds(segundosGrande);
            }

            if (patronBomba % 6 == 3)
            {
                TirarBomba(true);
                yield return new WaitForSeconds(segundosGrande);
                TirarBomba(true);
                yield return new WaitForSeconds(segundosGrande);
                TirarBomba(false);
                yield return new WaitForSeconds(segundosPeq);
            }

            if (patronBomba % 6 == 4)
            {
                TirarBomba(false);
                yield return new WaitForSeconds(segundosPeq);
                TirarBomba(true);
                yield return new WaitForSeconds(segundosGrande);
                TirarBomba(true);
                yield return new WaitForSeconds(segundosGrande);
            }

            if (patronBomba % 6 == 5)
            {
                TirarBomba(true);
                yield return new WaitForSeconds(segundosGrande);
                TirarBomba(false);
                yield return new WaitForSeconds(segundosPeq);
                TirarBomba(false);
                yield return new WaitForSeconds(segundosPeq);
            }

            //Siempre acabamos con uno grande
            TirarBomba(true);

            patronBomba++;
            contador++;

            //Tiempo entre cada patrón
            yield return new WaitForSeconds(2f);
        }

        //Fin Primer ataque
        yield return new WaitForSeconds(1f);

        //Comienzo Segundo ataque
        StartCoroutine("EnumLanzarPeriodicos");

    }

    public IEnumerator EnumLanzarPeriodicos()
    {
        int numPeriodicos = 0;
        if (vidas == 3)
        {
            numPeriodicos = 2;
        }

        if (vidas == 2)
        {
            numPeriodicos = 4;
        }

        if (vidas == 1)
        {
            numPeriodicos = 6;
        }

        //numPeriodicos = 0; //Para pruebas
        
        for (int i = 0; i < numPeriodicos; i++)
        {
            LanzarPeriodico(i%3);
            yield return new WaitForSeconds(3f);
        }

        //Fin Segundo ataque
        //yield return new WaitForSeconds(3f);

        //Comienzo Tercer ataque
        Embestida();
    }

    public IEnumerator EnumEsperaEmbestir()
    {
        yield return new WaitForSeconds(1f);
        heEsperado = true;
    }

    public IEnumerator EnumEsperaLanzaBombas()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine("EnumTirarBombas");
    }


    public void OnTriggerEnter2D(Collider2D colTr)
    {
        if (colTr.gameObject.name == "TuberiaJefeAgua" && mojado == false)
        {
            GameObject jefeMojadoInst = Instantiate(jefeMojado);
            jefeMojadoInst.transform.position = new Vector3(this.transform.position.x+1, jefeMojadoInst.transform.position.y, jefeMojadoInst.transform.position.z);

            embestidaFinal = false;
            heEsperado = false;
            this.transform.position = new Vector3(this.transform.position.x + 50, this.transform.position.y, this.transform.position.z);
            mojado = true;
        }
    }
 }
