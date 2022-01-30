using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_player : MonoBehaviour
{
    [SerializeField] private Image face;
    [SerializeField] private Image hp_bar;
    [SerializeField] private Image hp_background;
    [SerializeField] private float hurt_danho;
    [SerializeField] private Image mp_bar;
    [SerializeField] private Sprite angel_hp;
    [SerializeField] private Sprite demonio_hp;
    [SerializeField] private Sprite angel_face_normal;
    [SerializeField] private Sprite demonio_face_normal;
    [SerializeField] private Sprite angel_face_hurt;
    [SerializeField] private Sprite demonio_face_hurt;

    private habilidades_jugador habilidades;

    // Start is called before the first frame update
    void Start()
    {
        habilidades = GameObject.FindObjectOfType<habilidades_jugador>();
        hp_bar.fillAmount = 1;
        mp_bar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        hp_bar.fillAmount = habilidades.hp / habilidades.max_hp;
        mp_bar.fillAmount = habilidades.mp / habilidades.max_mp;

        if (Preguntar_demonio())
        {
            hp_bar.sprite = demonio_hp;
            hp_background.sprite = hp_bar.sprite;

            if (habilidades.mp<=0)
            {
                face.sprite = demonio_face_hurt;
            }
            else
            {
                face.sprite = demonio_face_normal;
            }

        }
        else {
            hp_bar.sprite = angel_hp;
            hp_background.sprite = hp_bar.sprite;

            if (hp_bar.fillAmount >= hurt_danho)
            {
                face.sprite = angel_face_normal;
            }
            else {
                face.sprite = angel_face_hurt;
            }
        }

    }

    bool Preguntar_demonio() {
        return habilidades.demonio.modo_demonio;
    }
}
