using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public Interactable focus;                                                                  //Declare an interactable called focus
    public LayerMask movementMask;                                                              //Declare a layerMask called movementMask.  This represents the ground

    Camera cam;                                                                                 //Declare a camera called cam
    PlayerMotor motor;                                                                          //Declare a playerMotor called motor

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;                                                                      //Assing the main camera to the variable cam
        motor = GetComponent<PlayerMotor>();                                                    //Get a playerMotor and assign it to motor
    }

    void Update()//Is called once per frame.  Asks if the mouse has been clicked
    {
        //Move the player
        if (Input.GetMouseButtonDown(0))                                                        //If the left mouse button is clicked...
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);                                //Declare a ray called ray which points to the mouse positon
            RaycastHit hit;                                                                     //Declare a raycastHit called hit

            if (Physics.Raycast(ray, out hit, 100, movementMask))                               //If the ray hits the ground...
            {
                motor.MoveToPoint(hit.point);                                                   //Call the method MoveToPoint in playerMotor
                RemoveFocus();
            }
        }

        //Move the player to a object
        if (Input.GetMouseButtonDown(1))                                                        //If the right mouse button is clicked...
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);                                //Declare a ray called ray using the mouse position
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))//If the ray hit an object...
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();          //Declare an interactable called interactable.  Assign whatever the ray hit to it.
                if(interactable != null)                                                        //If the interactable is something
                {
                    SetFocus(interactable);                                                     //Set the focus of the player to it
                }
            }
        }
    }

    void SetFocus(Interactable newFocus)                                                        //Takes in an interactable
    {
        if(newFocus != focus)                                                                   //If the new focus is not the same as the current focus...
        {
            if(focus != null)                                                                   //If the focus is not null...
                focus.OnDefocused();                                                            //Deactiveate the current focus

            focus = newFocus;                                                                   //Set the new focus to the current focus
            motor.FollowTarget(newFocus);                                                       //Set the player to move to the current focus
        }
        
        newFocus.OnFocused(transform);                                                          //Call the onfocused method for the new focus
    }

    void RemoveFocus()                                                                          //Clears the focus of the player
    {
        if (focus != null)                                                                      //If the focus is not null...
            focus.OnDefocused();

        focus = null;                                                                           //Set fucus to null
        motor.StopFollowingTarget();
    }
}