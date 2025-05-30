using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ativar : MonoBehaviour {
    public GameObject mod;
    public static GameObject modal;
    void Start () {
        modal = mod;
        Debug.Log ("Modal: " + modal.transform.name);

    }

    public void desativar(){
        modal.SetActive (false);
        Debug.Log ("Modal desativado");
    }

    public static void ativarModal(){
        modal.SetActive (true);
        Debug.Log ("Modal ativado");
    }
}