using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject[] keyIndicators;
    public List<GameObject> KeyReferences;
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
    private CameraFollowCursor cam;
    private Animator animator;
    private Transform modelTransform;
    private Quaternion initmodelRotation;

    private void Start()
    {
        //Accedemos al rigidbody;
        rb = GetComponent<Rigidbody>();
        //Accedemos a la camara
        cam=Camera.main.GetComponent<CameraFollowCursor>();
        respawnPoint=transform.position;
        modelTransform = transform.Find("Model").Find("Character"); //Refencia al transform del modelo 3D
        animator = modelTransform.GetComponent<Animator>(); //Referencia al animator
        loseKeys(3);
        initmodelRotation = modelTransform.localRotation;
    }

    void FixedUpdate()
    {
        if (!dead)
        {
            modelTransform.localPosition = Vector3.zero;
            modelTransform.transform.localRotation = initmodelRotation;
        }
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
        if (direction.magnitude > 0 && !dead)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        /////ANIMACION////////
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walking", false);
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
            animator.SetBool("Idle", false);     //Boleanos de control de las animaciones para que se eejcuten correctamente
            animator.SetBool("Walking", false);
            animator.SetBool("Dead", true);
            dead = true;
            loseKeys(keyNumber);
            Invoke("Respawn", 3.5f);
        }
    }

    private void Respawn()
    {
        animator.SetBool("Dead", false);    //Boleanos de control de las animaciones para que se eejcuten correctamente
        animator.SetBool("Idle", true);
        modelTransform.position = transform.position;   //Hacemos coincidir la posicion del modelo con la de su contenedor ya que la posicion del modelo cambia en las animaciones
        modelTransform.localRotation = Quaternion.Euler(0f, 180f, 0f);  //Reiniciamos su rotacion
        transform.position = respawnPoint;
        cam.CamResetDeath();
        if (KeyReferences!=null)
        {
            foreach(GameObject key in KeyReferences)
            {
                key.SendMessage("Reapear");
            }
            for (int i = 0; i < KeyReferences.Count; i++)
            {
                KeyReferences.RemoveAt(0);
            }
        }

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

    public void GetKey(GameObject key)
    {
        keyNumber++;
        KeyReferences.Add(key);
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


