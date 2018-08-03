using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_ControlUI : MonoBehaviour {

    [SerializeField]
    private GameObject imgVida1;

    [SerializeField]
    private GameObject imgVida2;

    [SerializeField]
    private GameObject imgVida3;

    [SerializeField]
    private GameObject imgRaspa1;

    [SerializeField]
    private GameObject imgRaspa2;

    [SerializeField]
    private GameObject imgRaspa3;

    [SerializeField]
    private GameObject imgVidaExtra;

    [SerializeField]
    private GameObject imgMoneda_Unidad;

    [SerializeField]
    private GameObject imgMoneda_Decena;

    [SerializeField]
    private Sprite[] sprNumero;

    //[SerializeField]
    //private GameObject canvas;

    [SerializeField]
    private GameObject contenedorVidas;

    [SerializeField]
    private GameObject contenedorGameOver;

    [SerializeField]
    private GameObject contenedorFelicitacion;

    private GameObject personaje;

    

    // Use this for initialization
    void Start () {
        personaje = GameObject.FindGameObjectWithTag("Player");
	}

    // Update is called once per frame
    void Update() {
        bool intro;
        int vidas;
        bool meta;
        int monedas; 
        
        if (SceneManager.GetActiveScene().buildIndex <= 3)
        {
            intro = personaje.GetComponent<Scr_Personaje_Movimiento>().Intro;
            vidas = personaje.GetComponent<Scr_Personaje_Colisiones>().Vidas;
            meta = personaje.GetComponent<Scr_Personaje_Movimiento>().Meta;
            monedas = personaje.GetComponent<Scr_Personaje_Colisiones>().Monedas;
        } else
        {
            intro = personaje.GetComponent<Scr_Personaje_Movimiento>().Intro;
            vidas = personaje.GetComponent<Scr_Personaje_Colisiones>().Vidas;
            meta = personaje.GetComponent<Scr_Personaje_Movimiento>().Meta;
            monedas = personaje.GetComponent<Scr_Personaje_Colisiones>().Monedas;
        }

        if (intro)
        {
            if (vidas >= 1)
            {
                if (!meta)
                {
                    ControlaVidas(vidas);
                    ControlaMonedas(monedas);
                } else
                {
                    ControlaMeta();
                }
            } else
            {
                ControlaMuerte();
            }
        }

    }

    public void ControlaVidas(int vidas)
    {
        contenedorVidas.SetActive(true);
        if (vidas == 4)
        {
            imgVidaExtra.SetActive(true);
            imgVida3.SetActive(true);
            imgRaspa3.SetActive(false);
        }

        if (vidas == 3)
        {
            imgVidaExtra.SetActive(false);
            imgVida3.SetActive(true);
            imgRaspa3.SetActive(false);
        }

        if (vidas == 2)
        {
            imgVida3.SetActive(false);
            imgRaspa3.SetActive(true);
            imgVida2.SetActive(true);
            imgRaspa2.SetActive(false);
        }

        if (vidas == 1)
        {
            imgVida3.SetActive(false);
            imgRaspa3.SetActive(true);
            imgVida2.SetActive(false);
            imgRaspa2.SetActive(true);
            imgVida1.SetActive(true);
            imgRaspa1.SetActive(false);
        }
        
    }

    public void ControlaMeta()
    {
        contenedorFelicitacion.SetActive(true);
        contenedorVidas.SetActive(false);
    }

    public void ControlaMuerte()
    {
        contenedorGameOver.SetActive(true);
        contenedorVidas.SetActive(false);
    }

    public void ControlaMonedas(int monedas)
    {
        int unidades = monedas % 10;
        int decenas = (int) monedas / 10;

        imgMoneda_Unidad.GetComponent<Image>().sprite = sprNumero[unidades];
        imgMoneda_Decena.GetComponent<Image>().sprite = sprNumero[decenas];
    }

    public void ClickBtnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ClickBtnMenu()
    {
        SceneManager.LoadScene("Esc_Menu");
    }
}
