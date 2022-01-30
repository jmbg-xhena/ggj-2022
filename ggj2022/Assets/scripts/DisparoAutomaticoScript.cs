using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoAutomaticoScript : MonoBehaviour
{
    public AudioClip Sound;
    public AudioClip Hit_Sound;
    public AudioClip Bullet_Dmg_Sound;
    public float Speed;
    private Rigidbody2D Rigidbody2D;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = direction * Speed;
    }

    public void SetDirection(Vector2 direction2)
    {
        direction = direction2;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Sustituir JohnMovement por Componente de Jugador adecuado
        habilidades_jugador player = collision.GetComponent<habilidades_jugador>();

        if(player != null)
        {
            //player.Hit(); //Aqui se definira el efecto del danio del proyectil, se requiere saber cual sera el valor que controlara el HP y como se accedera
            Debug.Log("Hit!!");
            Camera.main.GetComponent<AudioSource>().PlayOneShot(Bullet_Dmg_Sound);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(Hit_Sound);
            DestroyBullet();
        }
        /*if(grunt != null)
        {
            grunt.Hit();
        }*/
    }
}
