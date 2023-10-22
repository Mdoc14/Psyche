using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityControl : MonoBehaviour
{
    public List<GameObject> Habitacion = new List<GameObject>();
    public List<GameObject> Pasillo = new List<GameObject>();
    public List<GameObject> Bano = new List<GameObject>();
    public List<GameObject> Triggers = new List<GameObject>();

    private void Awake()
    {
        //
        quitRoom(Pasillo);
        //quitRoom(Bano);

        //
        chargeRoom(Habitacion);
        //Desactivamos las paredes del punto del spawn
        Pasillo[0].GetComponent<MeshRenderer>().enabled = false;
        Pasillo[1].GetComponent<MeshRenderer>().enabled = false;
        Triggers[0].SetActive(true);
        Triggers[1].SetActive(true);
    }

    public void quitRoom(List<GameObject> lista)
    {
        foreach (GameObject obj in lista)
        {
            obj.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void chargeRoom(List<GameObject> lista)
    {
        foreach (GameObject obj in lista)
        {
            obj.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    public void ChangeRooms(int room)
    {
        //Esta saliendo de la habitacion
        if (room == 1)
        {
            Triggers[0].SetActive(false);
            Triggers[1].SetActive(true);
            quitRoom(Habitacion);
            chargeRoom(Pasillo);

        } else if (room == 2)
        {
            Triggers[0].SetActive(true);
            Triggers[1].SetActive(false);
            quitRoom(Pasillo);
            chargeRoom(Habitacion);
        }   
    }
}
