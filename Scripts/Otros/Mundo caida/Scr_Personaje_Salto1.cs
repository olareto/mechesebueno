using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Personaje_Salto1 : MonoBehaviour {

[SerializeField]
    private float tiempoInvulnerable;

    [SerializeField]
    private float fuerzaSalto;  //17 mas o menos

    private Scr_Personaje_Movimiento1 scrPersMov;
    private Scr_Personaje_Colisiones1 scrPersCol;

    private Rigidbody2D rbMe;
    private int salto;
    private bool heTocado;
    private bool enfriamiento;
    

	// Use this for initialization
	void Start () {
        enfriamiento=false;
        scrPersMov = this.GetComponent<Scr_Personaje_Movimiento1>();
        scrPersCol = this.GetComponent<Scr_Personaje_Colisiones1>();
        
        rbMe = this.GetComponent<Rigidbody2D>();
        salto = 0;
        heTocado = false;

	}
	
	// Update is called once per frame
	void Update () {
        //Para ordenador
        //Input.
        if (Input.GetKeyDown("space") && !scrPersCol.Golpeado && !scrPersMov.Meta && !scrPersCol.Ataque&& !enfriamiento)
        {
            enfriamiento=true;
            StartCoroutine("Ataque");
            StartCoroutine("cooldown");
            //Salto();
        }
        if (Input.GetKey("left") && !scrPersCol.Golpeado && !scrPersMov.Meta)
        {
            Mov(-1);
        }
         if (Input.GetKeyDown("up") && !scrPersCol.Golpeado && !scrPersMov.Meta)
        {
            Salto();
        }
         if (Input.GetKeyDown("down") && !scrPersCol.Golpeado && !scrPersMov.Meta)
        {
            //Salto();
            scrPersCol.Golpe();
        }
         if (Input.GetKey("right") && !scrPersCol.Golpeado && !scrPersMov.Meta)
        {
            Mov(1);
        }

        //Para movil
        if (Input.touchCount > 0 && !heTocado && !scrPersCol.Golpeado && !scrPersMov.Meta)
        {
            heTocado = true;
            Salto();
        }

        if (Input.touchCount == 0)
        {
            heTocado = false;
        }
    }

    public void Mov(int direccion){
        this.transform.Translate(10 * Time.deltaTime*direccion, 0, 0);
        //this.transform.position = this.transform.position + Vector3.up;

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
            
             
           // salto = 0;
        }
    }
    public IEnumerator cooldown()
    {
        yield return new WaitForSeconds(1);
        enfriamiento=false;
    }
    public IEnumerator Ataque()
    {
        scrPersCol.Ataque=true;
        Color colorInv = this.GetComponent<SpriteRenderer>().color;
        colorInv.a = 0;
        Color colorVis = this.GetComponent<SpriteRenderer>().color;
        colorVis.a = 1;

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

        scrPersCol.Ataque = false;
        
    }



 }

