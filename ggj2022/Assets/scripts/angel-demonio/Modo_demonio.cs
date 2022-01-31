using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modo_demonio : MonoBehaviour
{
    public bool modo_demonio;
    public bool berserker = false;
    public bool puede_cambiar_de_modo;
    public float cool_down_cambio_de_modo;
    public AudioClip a_demonio;
    public AudioClip a_angel;
    // Start is called before the first frame update
    void Start()
    {
        modo_demonio = false;
        puede_cambiar_de_modo = true;
    }

    public void Habilitar_cambio_de_modo() {
        puede_cambiar_de_modo = true;
    }

    public void Cambiar_modo() {//si se puede cambiar de modo y se presiona la tecla shift, se cmabia de modo
        if (puede_cambiar_de_modo) {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (modo_demonio)
                {
                    berserker = false;
                    //a angel
                    GetComponent<AudioSource>().PlayOneShot(a_angel);
                }else
                {
                    //a demonio
                    GetComponent<AudioSource>().PlayOneShot(a_demonio);
                }
                modo_demonio = !modo_demonio;
                puede_cambiar_de_modo = false;
                Invoke("Habilitar_cambio_de_modo", cool_down_cambio_de_modo);//cooldown de cambio de modo
            }
        }
    }
}
