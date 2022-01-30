using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzadorScript : MonoBehaviour
{
    public GameObject BulletPrefab;
    public AudioClip dmg_sound;
    public bool right_shot;
    public float time_between_shots;
    private float LastShoot;
    public int Health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > LastShoot + time_between_shots)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Vector3 direction;
        //Direccion del proyectil
        if (right_shot)
        {
            direction = Vector2.right;
        }else 
        {
            direction = Vector2.left;
        }

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<DisparoAutomaticoScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Health = Health - 1;
        Camera.main.GetComponent<AudioSource>().PlayOneShot(dmg_sound);
        if (Health == 0)
        {
            Destroy(gameObject);
        }
    }
}
