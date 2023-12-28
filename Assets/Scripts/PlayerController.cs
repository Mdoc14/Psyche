using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject[] keyIndicators;
    public int keyNumber;
    private Vector3 respawnPoint; 
    private Rigidbody rb;
    public float speed = 10.0f;
    public Vector3 direction;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    public bool canWalk;
    public bool falling;
    public bool dead;
    private CameraFollow cam;

    private void Start()
    {
        //Accedemos al rigidbody;
        rb = GetComponent<Rigidbody>();
        //Accedemos a la camara
        cam=Camera.main.GetComponent<CameraFollow>();
        respawnPoint=transform.position;
        loseKeys(3);
    }

    void FixedUpdate()
    {
        //Comprovaci?n de suelo
        CheckGround();

        // Se guarda la entrada horizontal y vertical
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        direction = new Vector3(direction.x, 0f, direction.z).normalized;

        ////////MOVIMIENTO/////////

        //transform.position = transform.position + (Vector3.forward * Time.deltaTime * speed * direction.z);
        //transform.position = transform.position + (Vector3.right * Time.deltaTime * speed * direction.x);

        //Mejor velocidad por Rigidbody, para que interactue mejor con el entorno
        if (canWalk && !falling)
        {
            rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, direction.z * speed);
        }
        else if (!canWalk && !falling)//Para no quedarse atascado en rampas en las que no se puede caminar
        {
            rb.velocity = new Vector3(direction.x * speed, -speed * 2, direction.z * speed);
        }

        ///////ROTACI?N////////
        if (direction.magnitude != 0)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    private void CheckGround()
    {
        //Comprobamos si puede caminar en el terreno
        RaycastHit hit; //Aqui se guarda la informacion del raycast
        Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), Vector3.down, out hit);//Raycast para comprobar si est? en el suelo
        if (hit.point != null && hit.collider != null)
        {
            if (hit.distance < 0.25f)//Se comprueba si el punto detectado est? suficientemente cerca
            {
                falling = false;
                if (!dead)
                {
                    if (hit.transform.CompareTag("Walkable"))//Se comprueba si la superficie tiene la tag "Walkable" para ver si se deberia andar sobre ella
                    {
                        canWalk = true;
                    }
                    else
                    {
                        canWalk = false;
                    }
                }
                else
                {
                    canWalk = false;
                }

            }
            else
            {
                falling = true;
            }
        }
        else
        {
            falling = true;
            canWalk = false;
        }
    }

    public void Die()
    {
        if (!dead)
        {
            cam.DieAnimation();
            dead = true;
            keyNumber = 0;
            Invoke("Respawn", 3.5f);
        }
    }

    private void Respawn()
    {
        transform.position = respawnPoint;
        cam.CamResetDeath();
        Invoke("SetAlive", 0.5f);

    }

    private void SetAlive()
    {
        dead = false;
    }

    public void setRespawn(Vector3 pos)
    {
        respawnPoint = pos;
    }

    public void GetKey()
    {
        keyNumber++;
        if (keyNumber >= 1)
        {
            keyIndicators[0].SetActive(true);
            if (keyNumber >= 2)
            {
                keyIndicators[1].SetActive(true);
                if (keyNumber >= 3)
                {
                    keyIndicators[2].SetActive(true);
                }
                else
                {
                    keyIndicators[2].SetActive(false);
                }
            }
            else
            {
                keyIndicators[1].SetActive(false);
                keyIndicators[2].SetActive(false);
            }
        }
        else
        {
            keyIndicators[0].SetActive(false);
            keyIndicators[1].SetActive(false);
            keyIndicators[2].SetActive(false);
        }
    }

    public void loseKeys(int cuantity)
    {
        keyNumber-= cuantity;
        if (keyNumber < 0)
        {
            keyNumber = 0;
        }
        if (keyNumber >= 1)
        {
            keyIndicators[0].SetActive(true);
            if (keyNumber >= 2)
            {
                keyIndicators[1].SetActive(true);
                if (keyNumber >= 3)
                {
                    keyIndicators[2].SetActive(true);
                }
                else
                {
                    keyIndicators[2].SetActive(false);
                }
            }
            else
            {
                keyIndicators[1].SetActive(false);
                keyIndicators[2].SetActive(false);
            }
        }
        else
        {
            keyIndicators[0].SetActive(false);
            keyIndicators[1].SetActive(false);
            keyIndicators[2].SetActive(false);
        }
    }
}


