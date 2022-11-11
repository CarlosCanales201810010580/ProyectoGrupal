using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushRigidBody : MonoBehaviour
{
    
    public float pushPower = 2.0f;//definir la fuerza con la que queremos empujar el objeto
    private float targetMass; //masa del objeto 

    private void OnControllerColliderHit(ControllerColliderHit hit) 
    {
        Rigidbody body = hit.collider.attachedRigidbody; //detectar el Rigidbody con el que chocamos y lo almacene en body
        
        //si el objeto  no tiene Rigidbody que no entre en el ciclo  o si tiene el rigidbody 
        //pero le hemos mencionado que no sea afectado por la fisica que no haga nada
        if(body == null || body.isKinematic)  
        {
            return; 
        }

        /* si nos movemos hacia abajo y chocamos con el objeto que no haga nada
         * para que se quede arriba del objeto      
         */
        if(hit.moveDirection.y < -0.3)
        {
            return;
        }

        targetMass = body.mass;

        //diceccion en la que estamos golpeando el objeto  0 para que no sea afectado eje y
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        body.velocity = pushDir * pushPower / targetMass; //velocidad del objeto = direccion * fuerza / masa
    }
}
