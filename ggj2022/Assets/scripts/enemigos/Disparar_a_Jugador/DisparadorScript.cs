using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparadorScript : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject player;
    public int Health;
    public AudioClip Dmg_sound_effect;
    public AudioClip Enemy_scream;
    public AudioClip Enemy_death;
    public float tiempo_de_disparo;
    private float LastShoot;

    private void Start()
    {
        player = GameObject.FindObjectOfType<habilidades_jugador>().gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        if (player == null) 
        {
            return;
        }
        
        Vector3 direction = player.transform.position - transform.position;
        if (direction.x >= 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        } else 
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }

        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);

        if (distance < 1.0f && Time.time > LastShoot + tiempo_de_disparo)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Vector3 direction;
        if(transform.localScale.x == 1.0f) 
        {
            direction = Vector2.right;
        } else 
        {
            direction = Vector2.left;
        }

        GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        //bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Dmg_sound_effect);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Enemy_scream);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Enemy_death);

        Health = Health - 1;
        if (Health == 0)
        {
            Destroy(gameObject);
        }
    }
}
