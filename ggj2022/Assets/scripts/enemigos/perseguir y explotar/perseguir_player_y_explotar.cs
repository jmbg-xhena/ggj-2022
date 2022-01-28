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
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.transform.position) <= distancia_deteccion)
        {
            en_rango = true;
            start_position = transform.position;
        }
        else {
            en_rango = false;
        }

        if (Vector2.Distance(transform.position, target.transform.position) <= distancia_explosion)
        {
            explotado = true;
            area_explosion.SetActive(true);
        }

        if (en_rango && !explotado) {
            end_position = target.transform.position;
            elapsed_time += Time.deltaTime;
            float percentageComplete = elapsed_time / desired_duration;

            transform.position = Vector2.Lerp(start_position, end_position, curve.Evaluate(percentageComplete));
        }
    }
}
