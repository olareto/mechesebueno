using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_ControladorMenu : MonoBehaviour {

    [SerializeField]
    private GameObject objMenu1;

    [SerializeField]
    private GameObject objMenu2;

    [SerializeField]
    private GameObject objBtn1;

    [SerializeField]
    private GameObject objBtn2;

    [SerializeField]
    private GameObject objBtn3;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickBtnStart()
    {
        objMenu1.SetActive(false);
        objMenu2.SetActive(true);
    }

    public void ClickBtnMundo1()
    {
        objBtn1.SetActive(true);
        objBtn2.SetActive(true);
        objBtn3.SetActive(true);
    }

    public void ClickBtn1()
    {
        SceneManager.LoadScene("Esc_Mundo1_1");
    }

    public void ClickBtn2()
    {
        SceneManager.LoadScene("Esc_Mundo1_2");
    }

    public void ClickBtn3()
    {
        SceneManager.LoadScene("Esc_Mundo1_3");
    }

    public void ClickBtnExit()
    {
        Application.Quit();
    }
}
