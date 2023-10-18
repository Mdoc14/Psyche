using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public Vector3 direction;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    void Update()
    {
        // Se guarda la entrada horizontal y vertical
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        direction = new Vector3(direction.x, 0f, direction.z).normalized;

        ////////MOVIMIENTO/////////

        transform.position = transform.position + (Vector3.forward * Time.deltaTime * speed * direction.z);
        transform.position = transform.position + (Vector3.right * Time.deltaTime * speed * direction.x);


        ///////ROTACIÓN////////
        if (direction.magnitude != 0)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

    }
}
