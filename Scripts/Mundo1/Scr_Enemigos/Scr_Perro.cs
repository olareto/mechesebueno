using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Perro : Scr_Gen_Enemigo {

	[SerializeField]
    private float velocidad;

	[SerializeField]
    private float fuerzaSalto;

    [SerializeField]
    private float fuerzaGravedad;

    [SerializeField]
    private float distanciaParaSalto;

    private Rigidbody2D rbMio; //2D
    //private Rigidbody rbMio; //3D

    public bool salto;
    private bool hasHechoSonido;

    private float yInicial;
        
    // Use this for initialization
    void Start () {
        this.name = "Perro";

        //Fijos
        Camara = GameObject.FindGameObjectWithTag ("MainCamera");
		MeMuevo = false;

        //Propios
        rbMio = this.GetComponent<Rigidbody2D>();

        salto = false;
        hasHechoSonido = false;
        yInicial = this.transform.position.y;
        
    }

    // Update is called once per frame
    void Update () {
        if (MeMuevo == false)
        {
            Visto();
        }
        else
        {
            Movimiento();
        }
    }

	public override void Movimiento ()
	{
        //Movimiento lateral
        this.transform.Translate(-velocidad * Time.deltaTime, 0, 0);

        //salto
        float distanciaAcamara = this.transform.position.x - Camara.transform.position.x;
        if (salto == false && distanciaAcamara < distanciaParaSalto)
        {
            salto = true;
            rbMio.AddForce (new Vector2 (0, fuerzaSalto * 100));
            rbMio.gravityScale = fuerzaGravedad;

            if (hasHechoSonido == false)
            {
                HazSonido();
            }
        }

        if (this.transform.position.y < yInicial && salto == true)
        {
            rbMio.gravityScale = 0;
            rbMio.velocity = new Vector2 (0, 0);
            this.transform.position = new Vector3(this.transform.position.x, yInicial, this.transform.position.z);
            
            salto = false; //si queremos que salte varias veces
        }

    }

    public void HazSonido()
    {
        this.GetComponent<AudioSource>().Play();
        hasHechoSonido = true;
    }
}
