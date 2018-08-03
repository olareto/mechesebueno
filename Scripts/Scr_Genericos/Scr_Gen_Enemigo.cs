using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Gen_Enemigo : MonoBehaviour {

    [SerializeField]
    [Tooltip("Valor de 22 a -9")]
    private float distanciaParaActuar;

    private bool meMuevo;
	private GameObject camara;


	//Get y Set
	public bool MeMuevo  
	{  
		get { return meMuevo; }  
		set { meMuevo = value; }  
	} 

	//Get y Set
	public GameObject Camara  
	{  
		get { return camara; }  
		set { camara = value; }  
	}

    //Get y Set
    public float DistanciaParaActuar  
	{  
		get { return distanciaParaActuar; }  
		set { distanciaParaActuar = value; }  
	} 

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public virtual void Movimiento(){
	}

	public void Visto(){
        float distanciaAcamara = this.transform.position.x - camara.transform.position.x;
		if (distanciaAcamara < distanciaParaActuar) {
			meMuevo = true;
		}
	}
}
