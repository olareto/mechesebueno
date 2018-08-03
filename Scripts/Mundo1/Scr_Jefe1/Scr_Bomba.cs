using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Bomba : MonoBehaviour {

    [SerializeField]
    private GameObject socavon;

    [SerializeField]
    private GameObject socavonGrande;

    [SerializeField]
    private GameObject sonidoBomba;

    [SerializeField]
    private float fuerza;

    private bool esGrande;

    public bool EsGrande
    {
        get { return esGrande; }
        set { esGrande = value; }
    }


    // Use this for initialization
    void Start () {
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, fuerza * 100));
    }
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.y <= -4.3)
        {
            CreaSocavon();
        }
	}

    public void OnTriggerEnter2D(Collider2D colTr)
    {
        if (colTr.gameObject.name == "Suelo")
        {
            Destroy(colTr.gameObject);
        }
    }

    public void CreaSocavon()
    {
        float margen = 3f;

        if (esGrande == false)
        {
            socavon = Instantiate(socavon);
            socavon.transform.position = new Vector3(this.transform.position.x - margen, socavon.transform.position.y, socavon.transform.position.z);
            socavon.gameObject.name = "Vacio";
        }
        else
        {
            socavonGrande = Instantiate(socavonGrande);
            socavonGrande.transform.position = new Vector3(this.transform.position.x - margen, socavonGrande.transform.position.y, socavonGrande.transform.position.z);
            socavonGrande.gameObject.name = "Vacio";
        }

        Instantiate(sonidoBomba);
        Destroy(this.gameObject);
    }
}
