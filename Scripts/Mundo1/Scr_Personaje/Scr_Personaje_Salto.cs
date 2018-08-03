using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Personaje_Salto : MonoBehaviour {

    [SerializeField]
    private float fuerzaSalto;  //17 mas o menos

    private Scr_Personaje_Movimiento scrPersMov;
    private Scr_Personaje_Colisiones scrPersCol;

    private Rigidbody2D rbMe;
    private int salto;
    private bool heTocado;
    

	// Use this for initialization
	void Start () {
        scrPersMov = this.GetComponent<Scr_Personaje_Movimiento>();
        scrPersCol = this.GetComponent<Scr_Personaje_Colisiones>();
        
        rbMe = this.GetComponent<Rigidbody2D>();
        salto = 0;
        heTocado = false;
	}
	
	// Update is called once per frame
	void Update () {
        //Para ordenador
        if (scrPersMov.Intro && Input.GetKeyDown("space") && !scrPersCol.Golpeado && !scrPersMov.Meta)
        {
            Salto();
        }

        //Para movil
        if (scrPersMov.Intro && Input.touchCount > 0 && !heTocado && !scrPersCol.Golpeado && !scrPersMov.Meta)
        {
            heTocado = true;
            Salto();
        }

        if (Input.touchCount == 0)
        {
            heTocado = false;
        }
    }


    public void Salto()
    {
        if (this.salto < 2)
        {
            //Le damos un impulso inicial
            this.transform.position = this.transform.position + Vector3.up;

            rbMe.velocity = Vector3.zero;
            rbMe.AddForce(new Vector2(0, fuerzaSalto * 100));
            salto += 1;
            if (!scrPersCol.Berserk)
            {
                this.GetComponent<Animator>().SetInteger("TransMe", salto);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D colisionador)
    {
        string nombreColisionador = colisionador.gameObject.name;

        if (
                nombreColisionador == "Suelo" ||
                nombreColisionador == "Alfeizar" ||
                nombreColisionador == "Tejado" ||
                nombreColisionador == "Caja" ||
                nombreColisionador == "Caja2" ||
                nombreColisionador == "CajaMadera"       
            )
        {
            salto = 0;
        }
    }

 }

