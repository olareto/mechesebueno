using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Tuberia : MonoBehaviour
{

    [SerializeField]
    private GameObject agua;

    [SerializeField]
    private GameObject sonidoAgua;

    private int contador;

    // Use this for initialization
    void Start()
    {
        this.name = "TuberiaJefe";

        contador = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LanzarAgua()
    {
        StartCoroutine("EnumLanzaAgua");
    }

    public IEnumerator EnumLanzaAgua()
    {
        Instantiate(sonidoAgua);
        while (contador < 60)
        {
            yield return new WaitForSeconds(0.03f);
            GameObject aguaInst = Instantiate(agua);
            aguaInst.transform.position = new Vector3(this.transform.position.x - 2f, -2.53967f, 0f);

            if (contador % 2 == 0)
            {
                aguaInst.transform.Rotate(180, 0, 0);
            }
            
            contador++;
        }
    }
}