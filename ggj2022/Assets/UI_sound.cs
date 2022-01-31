using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_sound : MonoBehaviour
{
    public AudioClip click;
    public AudioClip hover;

    public void Click() {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(click);
    }

    public void Hover()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(hover);
    }
}
