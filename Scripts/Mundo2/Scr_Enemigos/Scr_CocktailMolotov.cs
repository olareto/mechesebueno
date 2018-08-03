using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CocktailMolotov : MonoBehaviour {

    [SerializeField]
    private GameObject fuego;

    [SerializeField]
    private float fuerza;

    [SerializeField]
    private float velocidad;

    public bool arriba;

    public bool Arriba
    {
        get
        {
            return arriba;
        }

        set
        {
            arriba = value;
        }
    }

    // Use this for initialization
    void Start () {
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, fuerza * 100));
    }
	
	// Update is called once per frame
	void Update () {
        Movimiento();
        float yfinal;
        if (arriba)
        {
            yfinal = 7f;
        } else
        {
            yfinal = 2.6f;
        }
        if (this.transform.position.y <= yfinal)
        {
            GameObject objFuego = Instantiate(fuego);
            objFuego.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+1f, objFuego.transform.position.z);
            objFuego.name = "Fuego";
            Destroy(this.gameObject);
        }
    }

    public void Movimiento()
    {
        this.transform.Translate(velocidad * Time.deltaTime, 0, 0);
    }
}
