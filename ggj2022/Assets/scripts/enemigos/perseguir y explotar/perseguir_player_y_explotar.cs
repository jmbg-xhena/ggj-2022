using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perseguir_player_y_explotar : MonoBehaviour
{
    private Vector2 end_position;
    private Vector2 start_position;
    [SerializeField] private float desired_duration = 3f;
    private float elapsed_time;
    [SerializeField] private bool en_rango;
    [SerializeField] private bool explotado;
    [SerializeField] private float distancia_explosion = 0.5f;
    [SerializeField] private float distancia_deteccion = 1f;
    [SerializeField] private GameObject area_explosion;
    private GameObject target;
    public bool lanzado;
    public bool stunted;
    public AudioClip se_lanza;
    public AudioClip explota;
    public AudioClip Enemy_death;

    [SerializeField]
    private AnimationCurve curve;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindObjectOfType<habilidades_jugador>().gameObject;
        end_position = target.transform.position;
        start_position = transform.position;
        explotado = false;
        en_rango = false;
        area_explosion.SetActive(false);
        lanzado = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (stunted) return;
        if (Vector2.Distance(transform.position, target.transform.position) <= distancia_deteccion)
        {
            en_rango = true;
            start_position = transform.position;
            if (!lanzado) {

                GetComponent<Animator>().Play("iniciar lanzarse");
                Camera.main.GetComponent<AudioSource>().PlayOneShot(se_lanza);
                lanzado = true;
            }
        }
        else {
            en_rango = false;
            lanzado = false;
        }

        if (Vector2.Distance(transform.position, target.transform.position) <= distancia_explosion)
        {

            explotado = true;
            GetComponent<Animator>().Play("explotar");
            Camera.main.GetComponent<AudioSource>().PlayOneShot(explota);
            //area_explosion.SetActive(true);
        }

        if (en_rango && !explotado) {
            end_position = target.transform.position;
            elapsed_time += Time.deltaTime;
            float percentageComplete = elapsed_time / desired_duration;

            transform.position = Vector2.Lerp(start_position, end_position, curve.Evaluate(percentageComplete));
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
                GetComponent<Animator>().Play("morir");
                Destroy(this.gameObject);
                Camera.main.GetComponent<AudioSource>().PlayOneShot(Enemy_death);
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
}
