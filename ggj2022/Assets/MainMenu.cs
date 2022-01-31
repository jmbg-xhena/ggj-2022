using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Escena_Juego()
    {
        Invoke("Cargar_jergo", 0.5f);
    }

    public void Cargar_jergo()
    {
        SceneManager.LoadScene("Juego");
    }

    public void Escena_Creditos()
    {
        Invoke("Cargar_creditos", 0.5f);
    }

    public void Cargar_creditos()
    {
        SceneManager.LoadScene("Creditos");
    }
}
