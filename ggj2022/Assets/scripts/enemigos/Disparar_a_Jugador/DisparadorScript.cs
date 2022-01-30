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

    public bool stunted;

    private void Start()
    {
        stunted = false;
        player = GameObject.FindObjectOfType<habilidades_jugador>().gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        if (player == null || stunted) 
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
            GetComponent<Animator>().Play("disparar");
            LastShoot = Time.time;
        }
    }

    public void Shoot()
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
        bullet.transform.localScale = transform.localScale/2;
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

    public void Des_stunt()
    {
        GetComponent<Animator>().speed = 1;
        stunted = false;
        this.gameObject.tag = "Enemy";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!stunted)
        {
            if (collision.CompareTag("Player"))
            {
                StopAllCoroutines();
                GetComponent<Animator>().Play("atacar");
            }

            if (collision.CompareTag("WeaponPlayer"))
            {
                Destroy(this.gameObject);
            }

            if (collision.CompareTag("Stunt"))
            {
                stunted = true;
                GetComponent<Animator>().speed = 0;
                StopAllCoroutines();
                Invoke("Des_stunt", 1f);
                this.gameObject.tag = "Untagged";
            }
        }
        else
        {
            Invoke("Des_stunt", 1f);
        }
    }
}
