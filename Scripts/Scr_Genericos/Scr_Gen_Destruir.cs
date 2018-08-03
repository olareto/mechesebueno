using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Gen_Destruir : MonoBehaviour {

    [SerializeField]
    private float tiempo;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine("Destruir");
    }

    public IEnumerator Destruir()
    {
        yield return new WaitForSeconds(tiempo);
        GameObject.Destroy(this.gameObject);
    }
}
