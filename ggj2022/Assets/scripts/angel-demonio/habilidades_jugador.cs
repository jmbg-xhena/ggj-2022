using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class habilidades_jugador : MonoBehaviour
{
    [System.NonSerialized] public Modo_demonio demonio;

    public float hp;
    public float max_hp;

    [Header("mp")]
    public float max_mp;
    public float mp;
    public float mp_a_recuperar_over_time;
    public float mp_a_recuperar_arma;
    public float mp_a_recuperar_escudo;

    [Header("weapon")]
    public GameObject weapon;
    public float weapon_time = 0.5f;
    public float cooldown_weapon;
    public bool puede_usar_weapon;
    public bool atacando;

    [Header("shield")]
    public GameObject shield;
    public float shield_time = 0.5f;
    public float cooldown_shield;
    public bool puede_usar_shield;

    [Header("invencibilidad")]
    public bool invencible;
    public float tiempo_invencible;

    [Header("over time")]
    public float mp_loss_over_time;
    public float hp_loss_over_time;

    [Header("melee atack")]
    public float mp_loss_melee_atack;
    public float hp_loss_melee_atack;

    [Header("enemy damage")]
    public float mp_loss_enemy_damage;
    public float hp_loss_enemy_damage;
    private Rigidbody2D rigidbody;
    public bool recibiendo_danho = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        demonio = GetComponent<Modo_demonio>();
        demonio.modo_demonio = false;
        invencible = false;
        demonio.berserker = false;
        recibiendo_danho = false;
        puede_usar_weapon = true;
        puede_usar_shield = true;
        mp = max_mp;
        hp = max_hp;
    }

    // Update is called once per frame
    void Update()
    {
        Meele_atack();
        demonio.Cambiar_modo();
        if (demonio.modo_demonio) //habilidades demonio
        {
            Drenado_barra_demonio();
        }
        else //habilidades angel
        {
            Shield_protecction();
            Recuperar_Barra_demonio_over_time(mp_a_recuperar_over_time);// recuperar barra de demonio estando en modo angel
        }
    }

    void Drenado_barra_demonio() {
        if (demonio.modo_demonio)
        {
            if (mp > mp_loss_over_time)//si la barra demonio no esta vacia
            {
                mp -= mp_loss_over_time * Time.deltaTime;//perder barra de demonio con el tiempo
            }
            else {
                demonio.berserker = true;
                hp -= (hp_loss_over_time - mp) * Time.deltaTime;//usar barra de vida cuando la de demonio se acabe
                mp = 0;
            }
        }
    }

    void Meele_atack() {
        if (Input.GetKeyDown(KeyCode.Space) && puede_usar_weapon && !shield.activeSelf) { // si el jugdor puede usar su arma y si presiono espacio
            atacando = true;
            puede_usar_weapon = false;//activar cooldown
            weapon.SetActive(true);//activar arma
            Invoke("Desactivar_arma", weapon_time);//mandar a desactivar el arma

            if (demonio.modo_demonio)
            {//ataque fuerte
                if (mp > mp_loss_melee_atack)//si la barra demonio no esta vacia
                {
                    mp -= mp_loss_melee_atack;//perder barra de demonio con el ataque
                }
                else
                {
                    hp -= hp_loss_melee_atack - mp;//usar barra de vida cuando la de demonio se acabe
                    mp = 0;
                }
            }
            else {//ataque stunt
               //stunt
            }
        }
    }

    void Shield_protecction() {
        if (Input.GetKeyDown(KeyCode.Q) && puede_usar_shield && !weapon.activeSelf) {
            puede_usar_shield = false;//activar cooldown
            shield.SetActive(true);//activar arma
            Invoke("Desactivar_escudo", shield_time);//mandar a desactivar el arma
        }
    }

    void End_cooldown_arma() {
        puede_usar_weapon = true;
    }
    void End_cooldown_escudo()
    {
        puede_usar_shield = true;
    }

    void Desactivar_arma() {
        weapon.SetActive(false);
        Invoke("End_cooldown_arma", cooldown_weapon);
    }

    void Desactivar_escudo()
    {
        shield.SetActive(false);
        Invoke("End_cooldown_escudo", cooldown_shield);
    }

    public void Recuperar_Barra_demonio(float recuperar) {
        if (mp < max_mp) {
            mp += recuperar;
        }
    }
    public void Recuperar_Barra_demonio_over_time(float recuperar)
    {
        if (mp < max_mp)
        {
            mp += recuperar*Time.deltaTime;
        }
    }

    void Deshacer_invencible()
    {
        invencible = false;
    }

    public void dejar_de_recibir_da�o() {
        recibiendo_danho = false;
    }

    public void dejar_de_atacar() {
        atacando = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if ((go.CompareTag("Enemy") || go.CompareTag("Spikes")|| go.CompareTag("ProjectileEnemy")) && !invencible) {
            if (demonio.modo_demonio)
            {
                if (mp > mp_loss_enemy_damage)
                {
                    mp -= mp_loss_enemy_damage;
                }
                else {
                    hp -= hp_loss_enemy_damage - mp;//usar barra de vida cuando la de demonio se acabe
                    mp = 0;
                }
            }
            else {
                hp -= hp_loss_enemy_damage;
            }
            invencible = true;
            recibiendo_danho = true;
            Invoke("Deshacer_invencible",tiempo_invencible);
        }
    }

    public void Knock(Rigidbody2D rb, float knockTime, float thrustForce)
    {
        Vector3 difference = rb.transform.position - transform.position;
        difference = difference.normalized * thrustForce;
        rigidbody.DOMove(rb.transform.position - difference, knockTime);
        StartCoroutine(KnockCo(rigidbody, knockTime));
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
