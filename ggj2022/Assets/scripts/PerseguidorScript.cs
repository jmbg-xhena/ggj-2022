using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PerseguidorScript : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float monster_damage;
    public AudioClip Sound;
    public AudioClip Hit_Sound;
    public float knockTime;
    public float thrustForce;
    private Transform target;
    private GameObject player;
    private float LastHit;
    public bool stunted;
    
    //Sustituir JohnMovement por habilidades_jugador -------
    //private JohnMovement target;

    
    // Start is called before the first frame update
    private void Start()
    {
        //Sustituir JohnMovement por habilidades_jugador -------
        player = GameObject.FindObjectOfType<habilidades_jugador>().gameObject;
        target = player.GetComponent<Transform>();

        //Obtiene la posicion del jugador, ustituir JohnMovement por habilidades_jugador -------
        //target = GameObject.FindObjectOfType<JohnMovement>();

    }

    // Update is called once per frame
    private void Update()
    {

        //Verifica si el jugador todavia vive
        if (player == null || stunted) 
        {
            return;
        }

        //Cambia la direccion a la que mira el enemigo en base al jugador
        Vector3 direction = player.transform.position - transform.position;
        if (direction.x >= 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        } else 
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }

        //Verifica si el jugador esta cerca para proceder a seguirlo
        float distanceX = Mathf.Abs(player.transform.position.x - transform.position.x);
        float distanceY = Mathf.Abs(player.transform.position.y - transform.position.y);
        if (distanceX < 1.0f && distanceY < 1.0f)
        {
            //Se considera que si el jugador esta dentro del rango de alcance del enemigo
            if(Vector2.Distance(transform.position, target.position) > stoppingDistance)
            {
                //Se persigue al Jugador
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            
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
        //Se ataca al jugador al colisionar
        //Sustituir JohnMovement por habilidades_jugador -------
        habilidades_jugador player = collision.GetComponent<habilidades_jugador>();

        //Se puede colocar un if aqui para verificar si la colision es con el jugador o con alguna colision de ataque del jugador

        if(player != null)
        {
            //Aqui se definira el efecto del danio del proyectil, se requiere saber cual sera el valor que controlara el HP y como se accedera
            if (Time.time > LastHit + 2f)
            {
                Debug.Log("Hit!!");
                Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
                Camera.main.GetComponent<AudioSource>().PlayOneShot(Hit_Sound);
                player.hp -= monster_damage;
                player.Knock(GetComponent<Rigidbody2D>(), knockTime, thrustForce);
                LastHit = Time.time;
            }
            
        }

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
                StopAllCoroutines();
                Invoke("Des_stunt", 1f);
                this.gameObject.tag = "Untagged";
                GetComponent<Animator>().speed = 0;
            }
        }
        else
        {
            Invoke("Des_stunt", 1f);
        }
    }

    public void Knock(Rigidbody2D rb, float knockTime)
    {
        StartCoroutine(KnockCo(rb, knockTime));
    }

    private IEnumerator KnockCo(Rigidbody2D rb, float knockTime)
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(knockTime);
            rb.velocity = Vector2.zero;
            rb.velocity = Vector2.zero;
        }
    }

}
