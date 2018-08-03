using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Carta : MonoBehaviour {

    [SerializeField]
    private float velocidad;

    private int tipo;
    private bool rotada;

    public int Tipo
    {
        get { return tipo; }
        set { tipo = value; }
    }

    // Use this for initialization
    void Start () {
        this.name = "Carta";
        rotada = false;
	}
	
	// Update is called once per frame
	void Update () {
        Movimiento();
        if (rotada == false)
        {
            Rota();
        }
    }

    public void Movimiento()
    {
        this.transform.Translate(-velocidad * Time.deltaTime, 0, 0);     
    }

    //Colisiones Trigger
    public void OnTriggerEnter2D(Collider2D colTr)
    {
        string nombreColTr = colTr.gameObject.name;
        Destroy(this.gameObject);
    }

    //Colisiones
    public void OnCollisionEnter2D(Collision2D colisionador)
    {
        string nombreColisionador = colisionador.gameObject.name;
        Destroy(this.gameObject);
    }

    public void Rota()
    {
        //Abajo
        if (tipo == 1)
        {
            this.transform.Rotate(new Vector3(0, 0, 45));
            rotada = true;
        }

        //Centro
        if (tipo == 2)
        {
            rotada = true;
        }
        
        //Arriba
        if (tipo == 3)
        {
            this.transform.Rotate(new Vector3(0, 0, -45));
            rotada = true;
        }
        
    }
}
