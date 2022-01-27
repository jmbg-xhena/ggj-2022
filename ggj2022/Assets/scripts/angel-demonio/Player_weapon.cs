using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_weapon : MonoBehaviour
{
    private habilidades_jugador habilidades;
    private void Start()
    {
        habilidades = GetComponentInParent<habilidades_jugador>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) {
            if (habilidades.demonio.modo_demonio)
            {
                //que pasa cuando conecta un ataque en modo demonio?
            }
            else {
                habilidades.Recuperar_Barra_demonio(habilidades.mp_a_recuperar_arma);
            }
        }
    }
}
