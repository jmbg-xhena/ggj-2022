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
    private bool Grounded;
    private bool DoubleJump;


    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        habilidades = GetComponent<habilidades_jugador>();
    }

    void Update()
    {
        if (habilidades.demonio.modo_demonio)
        {
            JumpForce = JumpforceDemon;
            Speed = SpeedDemon;
        }else
        {
            JumpForce = JumpForceAngel;
            Speed = SpeedAngel;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        animator.SetBool("Running", horizontal != 0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else Grounded = false;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Grounded)
            {
            Jump();
            }else if (DoubleJump && !habilidades.demonio.modo_demonio)
            {
                rigidbody2D.AddForce(Vector2.zero);
                Jump();
                DoubleJump = false;
            }
        }


        if (Grounded)
            DoubleJump = true;
    }

    private void Jump()
    {
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.AddForce(Vector2.up * JumpForce);
    }


    private void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(horizontal * Speed, rigidbody2D.velocity.y);
    }
}
