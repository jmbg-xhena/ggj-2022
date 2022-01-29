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
        SceneManager.LoadScene("Juego");
    }

    public void Escena_Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }
}
