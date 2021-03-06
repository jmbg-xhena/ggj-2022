using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_Jugador : MonoBehaviour
{
    public float JumpForce;
    public float Speed;
    public float JumpForceAngel;
    public float SpeedAngel;
    public float JumpforceDemon;
    public float SpeedDemon;
    public habilidades_jugador habilidades;

    private Rigidbody2D rigidbody2D;
    private Animator animator; 
    private float horizontal;
    public bool DoubleJump;
    public bool is_jumping;
    public bool puede_aterrizar;
    public bool Grounded;

    public AudioClip jugador_aterrizar;
    public AudioClip jugador_primer_salto;
    public AudioClip jugador_segundo_salto;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        habilidades = GetComponent<habilidades_jugador>();
        animator.Play("idle_angel");
        is_jumping = false;
    }

    void Update()
    {
        if (!habilidades.usando_escudo)
        {
            if (!habilidades.atacando)
            {
                if (habilidades.demonio.modo_demonio && !habilidades.demonio.berserker)
                {
                    JumpForce = JumpforceDemon;
                    Speed = SpeedDemon;
                }
                else
                {
                    JumpForce = JumpForceAngel;
                    Speed = SpeedAngel;
                }

                horizontal = Input.GetAxisRaw("Horizontal");

                if (horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                else if (horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                if (!habilidades.demonio.modo_demonio)
                {
                    if (!is_jumping && Grounded && !habilidades.recibiendo_danho)
                    {
                        if (horizontal == 0.0f)
                        {
                            animator.Play("idle_angel");
                        }
                        else
                        {
                            animator.Play("fly");
                        }
                    }
                }
                else
                {
                    if (!is_jumping && Grounded && !habilidades.recibiendo_danho)
                    {
                        if (horizontal == 0.0f)
                        {
                            animator.Play("idle_demonio");
                        }
                        else
                        {
                            animator.Play("run_demonio");
                        }
                    }
                }

                if (is_jumping && Grounded && puede_aterrizar && !habilidades.recibiendo_danho)
                {
                    if (habilidades.demonio.modo_demonio)
                    {
                        animator.Play("landing_demon");
                    }
                    else
                    {
                        animator.Play("idle_angel");//aterrizaje angel
                    }
                }



                Debug.DrawRay(transform.position, Vector3.down * 0.125f, Color.red);
                if (Physics2D.Raycast(transform.position, Vector3.down, 0.125f))
                {
                    Grounded = true;
                }
                else
                {
                    Grounded = false;
                    puede_aterrizar = true;
                    if (!is_jumping && !habilidades.recibiendo_danho)
                    {
                        if (habilidades.demonio.modo_demonio)
                        {
                            animator.Play("falling_demon");
                        }
                        else
                        {
                            animator.Play("falling_angel");// en aire angel
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    if (Grounded)
                    {
                        GetComponent<AudioSource>().PlayOneShot(jugador_primer_salto);
                        Jump();
                    }
                    else if (DoubleJump && (!habilidades.demonio.modo_demonio || habilidades.demonio.berserker))
                    {
                        GetComponent<AudioSource>().PlayOneShot(jugador_segundo_salto);
                        rigidbody2D.AddForce(Vector2.zero);
                        Jump();
                        DoubleJump = false;
                    }
                }


                if (Grounded)
                    DoubleJump = true;

                if (habilidades.recibiendo_danho)
                {
                    if (habilidades.demonio.modo_demonio)
                    {
                        animator.Play("da?o_demonio");
                    }
                    else
                    {
                        animator.Play("da?o_angel");
                    }
                }
            }
            else
            {
                if (habilidades.demonio.modo_demonio && !habilidades.recibiendo_danho)
                {
                    animator.Play("ataque_demonio");
                }
                else
                {
                    animator.Play("atack_angel");
                }
            }
        }
        else {
            if (!habilidades.demonio.modo_demonio)
            {
                animator.Play("shield");
            }
        }
    }

    public void aterrizar() {
        GetComponent<AudioSource>().PlayOneShot(jugador_aterrizar);
    }

    private void Jump()
    {
        is_jumping = true;
        puede_aterrizar = false;
        if (habilidades.demonio.modo_demonio && !habilidades.recibiendo_danho)
        {
            animator.Play("init_jump_demon");
            print("jump demon");
        }
        else {
            animator.Play("jump_angel");//salto angel
        }
    }

    public void PhisicJump() {
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    public void EndJump() {
        is_jumping = false;
    }

    public void PoderAterrizar() {
        puede_aterrizar = true;
    }


    private void FixedUpdate()
    {
        if(!habilidades.atacando && !habilidades.usando_escudo)
        rigidbody2D.velocity = new Vector2(horizontal * Speed, rigidbody2D.velocity.y);
    }
}
