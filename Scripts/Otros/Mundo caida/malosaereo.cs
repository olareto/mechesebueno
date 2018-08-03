using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class malosaereo : MonoBehaviour {

    [SerializeField]
    private GameObject pfCarta;
    [SerializeField]
    private int tipo;
    [SerializeField]
    private int patronataque;


    private bool ataque;
    private bool ataque2;
    [SerializeField]
    private float esperaataque;
    private Vector3 posini;
    private Vector3 final;
    private Vector3 traslacion;



    private float amplitudeX;
    private float amplitudeY;
    private float omegaX ;
    private float omegaY;
    private float index;



    private float t;

	// Use this for initialization
	void Start () {
        float valor=20;
        amplitudeX = 2.0f*valor;
        omegaX = 0.25f*valor;
        index=0;





        posini=this.transform.position;
        this.name = "Enemigo";
        t=0;
		//Propios
        final=new Vector3(99f+tipo*5,26.5f, 0f);
        ataque = false;
        ataque2 = false;
        switch(patronataque){
            case 0:
                traslacion=new Vector3(5f,5f, 0f);
            break;
            case 1:
                traslacion=new Vector3(0f,10f, 0f);
            break;
            case 2:
                traslacion=new Vector3(-5f,5f, 0f);
            break;
            
        }
        


	}
	
	// Update is called once per frame
	void Update () {
        if (ataque == false)
        {
            Visto();
        }
        else
        {
            if (ataque2== true){
                Movimiento();
            }
        }
    }
    public void Visto(){
        if(t<=1){
            t=t+Time.deltaTime;
 
            this.transform.position=Vector3.Lerp(posini,final,t);

        }
        else{
            ataque=true;
            StartCoroutine("EnumLanzaCartas");
        }

    }
	public  void Movimiento(){
        if(patronataque!=5){
            this.transform.Translate(traslacion*Time.deltaTime);
        }
        else{
             

             index += Time.deltaTime;
             float x = amplitudeX*Mathf.Cos (omegaX*index);
             float y = 10;
             this.transform.Translate(new Vector3(x,y,0)*Time.deltaTime);
        }
        //StartCoroutine("EnumLanzaCartas");

		
	}

    IEnumerator EnumLanzaCartas()
    {
        yield return new WaitForSeconds(esperaataque);
        ataque2=true;
    }
}
