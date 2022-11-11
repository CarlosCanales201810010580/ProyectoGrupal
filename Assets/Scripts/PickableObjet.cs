using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObjet : MonoBehaviour
{
    public bool isPickable = true; //el objeto se puede hagarrar

    private void OnTriggerEnter(Collider other) // cuando el cubo entre en el trigger(cuadro delante del player)
    {
        if(other.tag == "PlayerInteractionZone") //si la etiqueta del trigger es InteractionZone"     
        {
            other.GetComponentInParent<PickUpObjects>().ObjectToPickUp = this.gameObject;
        }
    }   
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerInteractionZone") //si estamos saliendo  de la interaccion
        {
            other.GetComponentInParent<PickUpObjects>().ObjectToPickUp = null;
        }
    }

}
