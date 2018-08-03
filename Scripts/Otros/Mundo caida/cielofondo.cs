using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cielofondo : MonoBehaviour {

    [SerializeField]
    private GameObject F1;

    [SerializeField]
    private GameObject F2;  //17 mas o menos
    [SerializeField]
    private GameObject F3;  //17 mas o menos

    private float anchura;
    private float inicial;
    private float vel;
    private bool go;


    

	// Use this for initialization
	void Start () {
       anchura=148;
       inicial=148;
       vel=20;
       go=true;

	}
	
	// Update is called once per frame
	void Update () {
        if(go){
            F1.transform.Translate(0, vel * Time.deltaTime, 0);
            F2.transform.Translate(0, vel * Time.deltaTime, 0);
            F3.transform.Translate(0, vel * Time.deltaTime, 0);
            if(F1.transform.localPosition.y>=inicial){
                Vector3 posi=F1.transform.localPosition;
                posi.y=posi.y-anchura*2;
                F3.transform.localPosition =posi;
                //F3.transform.Lerp(0,pos+296,0,1);
                GameObject aux=F1;
                F1=F2;
                F2=F3;
                F3=aux;
                //go=false;
            }
          

        }
    }


 }

