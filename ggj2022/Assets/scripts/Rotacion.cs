using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacion : MonoBehaviour
{
    private float rotZ;
    public float rotationSpeed;
    public bool clockWiseRotation;

    // Update is called once per frame
    void Update()
    {
        if (clockWiseRotation == false)
        {
            rotZ += Time.deltaTime * rotationSpeed;
        }
        else
        {
            rotZ += -Time.deltaTime * rotationSpeed;
        }

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
