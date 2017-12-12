using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    public virtual void Interact()//This method will allow interactions to take place based on what's in the inherited method
    {
        // This method is meant to be overridden
        Debug.Log("Interacting with " + transform.name);
    }

    void Update()//Is called once per frame
    {
        if(isFocus && !hasInteracted)//If this object is inFocus of the player and the player hasn't interacted with it yet...
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);//Get the 
            if(distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)//Takes in a transform
    {
        isFocus = true;//Set isFocus to true
        player = playerTransform;//Assign the transform of the player to the new transform
        hasInteracted = false;//Set hasInteracted to false
    }

    public void OnDefocused()//Defocus the focus
    {
        isFocus = false;
        player = null;
        hasInteracted = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}