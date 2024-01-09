using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexWallDissapear : MonoBehaviour
{
    //Para que objetos de las paredes también se oculten sin tener que modificar la interfaz
    //Utilizar solo si se va a ocultar un objeto interactuable o complejo
    public List<GameObject> objects = new List<GameObject>();
    // Update is called once per frame
    void Update()
    {
        if (transform.parent.GetComponent<MeshRenderer>() != null)
        {
            if (transform.parent.GetComponent<MeshRenderer>().enabled == true)
            {
                foreach (GameObject mr in objects)
                {
                    if (mr != null)
                    {
                        if (mr.activeSelf == false)
                        {
                            mr.SetActive(true);
                        }
                    }

                }
            }
            else if (transform.parent.GetComponent<MeshRenderer>().enabled == false)
            {
                foreach (GameObject mr in objects)
                {
                    if (mr != null)
                    {
                        if (mr.activeSelf == true)
                        {
                            mr.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}
