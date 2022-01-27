using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_player : MonoBehaviour
{
    [SerializeField] private Image hp_bar;
    [SerializeField] private Image mp_bar;
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
    }
}
