using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Personaje_SaltoTrampolin : MonoBehaviour {

    private Rigidbody2D rbMe;

    private float yInicial;

    private bool trampolin;
    private bool subidaTrampolin;

    public bool Trampolin
    {
        get
        {
            return trampolin;
        }

        set
        {
            trampolin = value;
        }
    }

    public bool SubidaTrampolin
    {
        get
        {
            return subidaTrampolin;
        }

        set
        {
            subidaTrampolin = value;
        }
    }

    // Use this for initialization
    void Start () {
        rbMe = GetComponent<Rigidbody2D>();
        yInicial = this.transform.position.y;
        trampolin = false;
        subidaTrampolin = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (trampolin)
        {
            MovimientoTrampolin();
        }
    }

    public void MovimientoTrampolin()
    {
        rbMe.freezeRotation = false;
        this.GetComponent<Collider2D>().isTrigger = false;

        //Si ha hecho el salto completo
        if (this.transform.position.y <= yInicial - 0.2)
        {
            rbMe.freezeRotation = true;
            this.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            rbMe.gravityScale = 0;
            rbMe.velocity = Vector3.zero;
            this.GetComponent<Collider2D>().isTrigger = true;
            this.transform.position = new Vector3(this.transform.position.x, yInicial, this.transform.position.z);
            trampolin = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D colTr)
    {
        string nombreColTr = colTr.gameObject.name;
        if (nombreColTr == "Trampolin")
        {
            trampolin = true;
            subidaTrampolin = true;
        }

        if (nombreColTr == "FinTrampolin" && trampolin)
        {
            subidaTrampolin = false;
            //rbMe.AddForce(new Vector3(0f, 800f, 0f));
            rbMe.AddForce(new Vector2(0f, 800f));
        }
    }
}
