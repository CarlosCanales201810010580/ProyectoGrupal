using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Vector3 offset;
    private Transform target;
    [Range (0,1)] public float lerpValue;
    public float sensibilidad;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform; //buscar un objeto que tenga el objeto player y queremos la posicion
    }

    // Update is called once per frame
    void LateUpdate() //se ejecuta al final 
    {
        //Vector3 lo que hace es mover la posicion de un objeto de un vector a otro vector y que tan rapido
        transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpValue); //camara hasta target + una distancia
      
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X" ) * sensibilidad, Vector3.up) * offset;  //para que la camara gire
        transform.LookAt(target); //camara mire al jugador
    }
}
