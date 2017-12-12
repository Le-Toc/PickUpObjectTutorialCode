using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    Transform target;                                                                                       // Target to follow
    NavMeshAgent agent;                                                                                     // Reference to our agent

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }

    public void MoveToPoint(Vector3 point)                                                                  //Takes in a vector 3
    {
        agent.SetDestination(point);                                                                        //Set the destination of the agent using the point sent in
    }

    public void FollowTarget(Interactable newTarget)                                                        //Takes in an interactable
    {
        agent.stoppingDistance = newTarget.radius * 0.8f;                                                   //Set the stopping distance of the agent to an 80% of the radius of the target
        agent.updateRotation = false;                                                                       //Set updateRotation to false

        target = newTarget.interactionTransform;                                                            //Set the current target to the position of the new target
    }

    public void StopFollowingTarget()                                                                       //Stop the player from moving anymore
    {
        agent.stoppingDistance = 0f;                                                                        //Set the stoppingDistance to 0
        agent.updateRotation = true;                                                                        //Set updateRotation to true

        target = null;                                                                                      //Clear the target
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
