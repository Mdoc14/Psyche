using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Vector3.zero,0.25f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("setRespawn", transform.position);
        }
    }
}
