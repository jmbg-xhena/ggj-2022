using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaAtravesable : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float WaitTime;
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            WaitTime = 0.5f;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            WaitTime = 0.5f;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if(WaitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                WaitTime = 0.5f;
            }else
            {
                WaitTime -= Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            effector.rotationalOffset = 0;
        }
    }
}
