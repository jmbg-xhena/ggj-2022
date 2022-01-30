using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaScript : MonoBehaviour
{
    public AudioClip Sound;
    public AudioClip Hit_Sound;
    public AudioClip Bullet_Dmg_Sound;
    public float Speed;
    public float bullet_damage;
    //public float Time_to_Destroy;

    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;
    private habilidades_jugador target;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);

        target = GameObject.FindObjectOfType<habilidades_jugador>();
        //Obtener direccion del Jugador
        Direction = (target.transform.position - transform.position).normalized * Speed;

        Rigidbody2D.velocity = new Vector2 (Direction.x, Direction.y);
        
    }

    /* Update is called once per frame
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }*/

    /*public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }*/

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        habilidades_jugador player = collision.GetComponent<habilidades_jugador>();

        if(player != null)
        {
            //player.Hit(); //Aqui se definira el efecto del danio del proyectil, se requiere saber cual sera el valor que controlara el HP y como se accedera
            Debug.Log("Hit!!");
            Camera.main.GetComponent<AudioSource>().PlayOneShot(Bullet_Dmg_Sound);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(Hit_Sound);
            //player.hp -= bullet_damage;
            DestroyBullet();
        }
        /*if(grunt != null)
        {
            grunt.Hit();
        }*/
    }
}
