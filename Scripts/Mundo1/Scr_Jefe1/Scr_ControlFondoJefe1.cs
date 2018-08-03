using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ControlFondoJefe1 : MonoBehaviour {

    [SerializeField]
    private GameObject[] trozo;

    [SerializeField]
    private GameObject suelo;

    private GameObject personaje;
    private GameObject trozoNuevo;

    private int contadorTrozos;

    private float tamañoTotalTrozos;
    private float tamañoTrozos;
    private float distanciaTrozo;
    


    // Use this for initialization
    void Start () {
        personaje = GameObject.FindGameObjectWithTag("Player");

        tamañoTotalTrozos = 709.75f;
        tamañoTrozos = tamañoTotalTrozos / 6;
        distanciaTrozo = 40; //la primera vez que se crea un nuevo trozo
        contadorTrozos = 1;
    }
	
	// Update is called once per frame
	void Update () {
		if (personaje.transform.position.x >= distanciaTrozo)
        {
            CreaTrozo(contadorTrozos);
        }
	}

    public void CreaTrozo(int i)
    {
        int repetidos = (int)i / trozo.Length;
        trozoNuevo = Instantiate(trozo[i%trozo.Length]);       
        trozoNuevo.transform.position = new Vector3(trozoNuevo.transform.position.x + tamañoTotalTrozos * repetidos, trozoNuevo.transform.position.y, trozoNuevo.transform.position.z);
        contadorTrozos++;
        distanciaTrozo = personaje.transform.position.x + tamañoTrozos;

        GameObject sueloInstancia = Instantiate(suelo);
        sueloInstancia.transform.position = new Vector3(sueloInstancia.transform.position.x + tamañoTrozos * (contadorTrozos-1) + 50, sueloInstancia.transform.position.y, sueloInstancia.transform.position.z);
        sueloInstancia.gameObject.name = "Suelo";
        
    }
}
