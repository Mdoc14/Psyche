using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisibility : MonoBehaviour
{
    private Collider trigger;
    public GameObject Mapa;
    private void OnTriggerEnter(Collider other)
    {
        trigger = other;
        if (other.CompareTag("RoomT1"))
        {
            Mapa.GetComponent<VisibilityControl>().ChangeRooms(1);
        } else if (other.CompareTag("RoomT2"))
        {
            Mapa.GetComponent<VisibilityControl>().ChangeRooms(2);
        }
    }
}
