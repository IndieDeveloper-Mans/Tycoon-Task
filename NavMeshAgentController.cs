using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshAgentController : MonoBehaviour
{
    public NavMeshAgent agent;
    [Space]
    public bool targetDistanceReached;
    [Space]
    public float forwardAmount;
    [Space]
    public float turnAmount;
    [Space]
    public float moveSpeedMultiplier;
    [Space]
    public float turnSpeedMultiplier;
    [Space]
    public float moveAcceleratePerSecond;
    [Space]
    private float acceleration = 0;

    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    public void SetAgentPathDestination(Vector3 target)
    {
        agent.SetDestination(target);

        if (agent.hasPath)
        {
            Move(agent.velocity);
        }

        CheckIfDistanceReached();
    }

    public bool CheckIfDistanceReached()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.magnitude == 0f)
                {
                    targetDistanceReached = true;

                    return true;
                }
            }
        }

        targetDistanceReached = false;

        return false;
    }

    public void Move(Vector3 move)
    {
        if (move.magnitude > 1f) move.Normalize();

        move = transform.InverseTransformDirection(move);

        acceleration += moveAcceleratePerSecond * Time.deltaTime;

        forwardAmount = Mathf.Min(acceleration, moveSpeedMultiplier);

        turnAmount = Mathf.Atan2(move.x, move.z) * turnSpeedMultiplier;

        FaceTarget();
    }

    void FaceTarget()
    {
        var turnTowardNavSteeringTarget = agent.steeringTarget;

        Vector3 direction = (turnTowardNavSteeringTarget - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeedMultiplier);
    }
}