using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private Transform player;
    public float rotationSpeed;

    private void Start()
    {
        player = transform.parent;
        transform.parent = null;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
        //Raycast desde la camera atraves de la posición del mouse.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100,1<<3))
        {
            float angle=Vector3.SignedAngle(transform.forward, new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position,Vector3.up);
            transform.Rotate(Vector3.up, angle * rotationSpeed * Time.deltaTime);
        }
    }
}
