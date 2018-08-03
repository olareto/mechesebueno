using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CochePayasos : MonoBehaviour {

    [SerializeField]
    private GameObject[] objMaletero;
    [SerializeField]
    private GameObject[] objCanyon;
    [SerializeField]
    private GameObject objMisil;
    [SerializeField]
    private GameObject objDiana;
    [SerializeField]
    private GameObject objCanyonMontado;

    private GameObject personaje;

    private int rondaMisiles;
    private int nMisilLanzado;
    private int nObjetoCaido;
    private int nObjetosQueSeCaen;
    private int nObjetoCogido;
    private int vidas;

    private bool subida;
    private bool bajada;
    private bool centro;
    private bool estaDiana;
    private bool disparaste;

    public int Vidas
    {
        get { return vidas; }
        set { vidas = value; }
    }

    public bool EstaDiana
    {
        get { return estaDiana; }
        set { estaDiana = value; }
    }

    public bool Disparaste
    {
        get { return disparaste; }
        set { disparaste = value; }
    }

    public int NObjetoCogido
    {
        get { return nObjetoCogido; }
        set { nObjetoCogido = value; }
    }

    // Use this for initialization
    void Start () {
        personaje = GameObject.FindGameObjectWithTag("Player");
        GameObject canyonMontado = Instantiate(objCanyonMontado);
        canyonMontado.transform.parent = personaje.transform;

        rondaMisiles = 0;
        nMisilLanzado = 0;
        nObjetoCaido = 0;
        nObjetosQueSeCaen = 0;
        nObjetoCogido = 0;
        vidas = 3;

        subida = false;
        bajada = false;
        centro = false;
        estaDiana = false;
        disparaste = false;

        InvokeRepeating("Patron1_Misiles", 3f, 0.6f);
        
        //Para hacer pruebas
        //Invoke("Patron3_Disparo", 2f);
    }
	
	// Update is called once per frame
	void Update () {
        Movimiento();

        if (disparaste)
        {
            HemosDisparado();
        }
    }

    public void Movimiento()
    {
        //Misma velocidad que Mecheese
        float velocidad = 22;

        //Movimiento horizontal
        //Condición para que la primera vez se coloque a la distancia correcta de Mecheese
        if(this.transform.position.x <= personaje.transform.position.x + 24 || this.transform.position.x > 80)
        {
            this.transform.Translate(-velocidad * Time.deltaTime, 0, 0);
        }

        //Movimiento vertical
        //Seguir a Mecheese
        //this.transform.position = new Vector3 (this.transform.position.x, personaje.transform.position.y+0.5f, this.transform.position.z) ;

        float yDesplazada = 8.4f;
        float velDespl = 7f;
        float yInicial = 4f;
        float yCentro = (yDesplazada + yInicial) / 2;
        if (centro)
        {
            if (subida)
            {
                yDesplazada = yCentro;   
            } else
            {
                yInicial = yCentro;
            }
        }
        
        //Subida
        if (subida && !bajada)
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
                subida = false;
                this.GetComponent<SpriteRenderer>().sortingOrder = -2;
                if (centro)
                {
                    centro = false;
                }
            }
        }

        //Bajada
        if (!subida && bajada)
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
                bajada = false;
                this.GetComponent<SpriteRenderer>().sortingOrder = 0;
                if (centro)
                {
                    centro = false;
                }
            }
        }
    }

    public void HazMovVertical(bool _subida, bool _centro)
    {
        if (_subida)
        {
            subida = true;
            bajada = false;
        } else
        {
            subida = false;
            bajada = true;
        }

        centro = _centro;
    }

    public void Patron1_Misiles()
    {
        //El coche comienza situado abajo.

        //Si es el primer misil lanzado es el principio de una ronda de misiles
        if (nMisilLanzado == 0)
        {
            rondaMisiles++;
        }
  
        //Creamos el misil a la altura del coche
        GameObject misil = Instantiate(objMisil);
        misil.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 1.5f, this.transform.position.z);
        nMisilLanzado++;

        //RONDAS 1
        if (rondaMisiles == 1)
        {
            if (nObjetoCogido == 0)
            {
            }  

            if (nObjetoCogido == 1)
            {
                if (nMisilLanzado == 1 || nMisilLanzado == 5)
                {
                    //El misil se mueve hacia arriba
                    misil.GetComponent<Scr_Misil>().Subida = true;
                }

                if (nMisilLanzado == 4)
                {
                    //El misil se mueve hacia abajo
                    misil.GetComponent<Scr_Misil>().Bajada = true;
                }
            }

            if (nObjetoCogido == 2)
            {
                if (nMisilLanzado == 3)
                {
                    //El misil se mueve hacia arriba
                    misil.GetComponent<Scr_Misil>().Subida = true;
                }

                if (nMisilLanzado == 2 || nMisilLanzado == 6)
                {
                    //El misil se mueve hacia abajo
                    misil.GetComponent<Scr_Misil>().Bajada = true;
                }
            }
        }

        //RONDAS 2
        if (rondaMisiles == 2)
        {
            if (nObjetoCogido == 0)
            {
                if (nMisilLanzado == 5)
                {
                    //El misil se mueve hacia arriba
                    misil.GetComponent<Scr_Misil>().Subida = true;
                }

                if (nMisilLanzado == 2)
                {
                    //El misil se mueve hacia abajo
                    misil.GetComponent<Scr_Misil>().Bajada = true;
                }
            }

            if (nObjetoCogido == 1)
            {
                if (nMisilLanzado == 1 || nMisilLanzado == 3)
                {
                    //El misil se mueve hacia arriba
                    misil.GetComponent<Scr_Misil>().Subida = true;
                }

                if (nMisilLanzado == 4 || nMisilLanzado == 6)
                {
                    //El misil se mueve hacia abajo
                    misil.GetComponent<Scr_Misil>().Bajada = true;
                }
            }

            if (nObjetoCogido == 2)
            {
                if (nMisilLanzado == 1 || nMisilLanzado == 3 || nMisilLanzado == 5)
                {
                    //El misil se mueve hacia arriba
                    misil.GetComponent<Scr_Misil>().Subida = true;
                }

                if (nMisilLanzado == 2 || nMisilLanzado == 4 || nMisilLanzado == 6)
                {
                    //El misil se mueve hacia abajo
                    misil.GetComponent<Scr_Misil>().Bajada = true;
                }
            }
        }

        //RONDAS 3 (Igual que la ronda 2, para no dificultar demasiado)
        if (rondaMisiles == 3)
        {
            if (nObjetoCogido == 0)
            {
                if (nMisilLanzado == 5)
                {
                    //El misil se mueve hacia arriba
                    misil.GetComponent<Scr_Misil>().Subida = true;
                }

                if (nMisilLanzado == 2)
                {
                    //El misil se mueve hacia abajo
                    misil.GetComponent<Scr_Misil>().Bajada = true;
                }
            }

            if (nObjetoCogido == 1)
            {
                if (nMisilLanzado == 1 || nMisilLanzado == 3)
                {
                    //El misil se mueve hacia arriba
                    misil.GetComponent<Scr_Misil>().Subida = true;
                }

                if (nMisilLanzado == 4 || nMisilLanzado == 6)
                {
                    //El misil se mueve hacia abajo
                    misil.GetComponent<Scr_Misil>().Bajada = true;
                }
            }

            if (nObjetoCogido == 2)
            {
                if (nMisilLanzado == 1 || nMisilLanzado == 3 || nMisilLanzado == 5)
                {
                    //El misil se mueve hacia arriba
                    misil.GetComponent<Scr_Misil>().Subida = true;
                }

                if (nMisilLanzado == 2 || nMisilLanzado == 4)
                {
                    //El misil se mueve hacia abajo
                    misil.GetComponent<Scr_Misil>().Bajada = true;
                }
            }

        }

        //Si el coche está parado:
        if (!subida && !bajada)
        {
            //Si está arriba baja y si no sube
            if (this.transform.position.y >= 6.2f)
            { 
                HazMovVertical(false, false);
            } else
            {
                HazMovVertical(true, false);
            }
        } else
        {
            //Si esta bajando sube, y si esta subiendo baja
            HazMovVertical(!subida, false);
        }

        //Después de 6 misiles terminamos una ronda
        if (nMisilLanzado == 6)
        {
            nMisilLanzado = 0;

            //Terminamos la ronda de misiles
            CancelInvoke();

            if (
                    (vidas == 3 && rondaMisiles == 1) ||
                    (vidas == 2 && rondaMisiles == 2) ||
                    (vidas == 1 && rondaMisiles == 3)
               )
            {
                //El coche se sitúa en el centro para el siguiente patrón
                HazMovVertical(subida, true);
                rondaMisiles = 0;

                //Se le caen los objetos del maletero
                InvokeRepeating("Patron2_ObjetosCaidos", 2f, 0.15f);
            } else
            {
                InvokeRepeating("Patron1_Misiles", 3f, 0.6f);

            }
            
        }
    }

    public void Patron2_ObjetosCaidos()
    {
        //Se le caen 2 objetos
        if (nObjetosQueSeCaen < 2)
        {
            GameObject objetoCaido = Instantiate(objMaletero[nObjetoCaido % objMaletero.Length]);

            //Cada objeto cae en un sitio distinto, usando epsilon (arriba, en medio)
            float epsilon = 0;
            if (nObjetoCaido % objMaletero.Length == 0)
            {
                epsilon = 2;
            }
            if (nObjetoCaido % objMaletero.Length == 1)
            {
                epsilon = -2;
            }

            if (nObjetoCaido % objMaletero.Length == 2)
            {
                epsilon = -2;
            }

            if (nObjetoCaido % objMaletero.Length == 3)
            {
                epsilon = 2;
            }

            objetoCaido.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 2f + epsilon, this.transform.position.z);
            objetoCaido.name = "ObjetoMaletero";

            nObjetoCaido++;
            nObjetosQueSeCaen++;
        }
        else
        {
            nObjetosQueSeCaen = 0;

            float epsilon = 0;
            //Se le cae el objeto del canyon que nos toca coger
            GameObject objetoCanyon = Instantiate(objCanyon[nObjetoCogido]);
            objetoCanyon.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 2f + epsilon, this.transform.position.z);
            objetoCanyon.name = "ObjetoCanyon";

            CancelInvoke();

            //Se sitúa abajo
            HazMovVertical(false, false);

            //Hay que darle un tiempo para ver si coge o no coge el objeto del cañón
            Invoke("ElegimosPatron", 2f);
            
        }
    }

    public void ElegimosPatron()
    {
        //Si no hemos cogido todos los objetos volvemos a los misiles. Si no, al disparo del cañón
        if (nObjetoCogido != objCanyon.Length)
        {
            InvokeRepeating("Patron1_Misiles", 1f, 0.6f);
        }
        else
        {
            //InvokeRepeating("Patron3_Disparo", 1f, 0.6f);
            Invoke("Patron3_Disparo", 2f);

            nObjetoCogido = 0;
        }
    }

    public void Patron3_Disparo()
    {
        //Creamos la diana
        GameObject diana = Instantiate(objDiana);
        Transform trCamara = GameObject.FindGameObjectWithTag("MainCamera").transform;

        //Situamos la diana donde la tenemos inicialmente en el Prefab
        diana.transform.position = new Vector3((trCamara.position.x - 58) + diana.transform.position.x, diana.transform.position.y, diana.transform.position.z);
        diana.transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;

        estaDiana = true;
    }

    public void HemosDisparado()
    {
        disparaste = false;
        //Si le hemos dado 3 veces lo hemos derrotado
        if (vidas == 0)
        {
            //Congratulations
            personaje.GetComponent<Scr_Personaje_Movimiento>().LlegoMeta();
        } else
        {
            //Creamos el CanyonMontado vacío
            GameObject canyonMontado = Instantiate(objCanyonMontado);
            canyonMontado.transform.parent = personaje.transform;

            //Volvemos a lanzar misiles
            InvokeRepeating("Patron1_Misiles", 3f, 0.6f);
        }
        
    }
}
