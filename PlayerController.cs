using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgentController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] List<Perk> _playerPerks;
    [Space]
    [SerializeField] bool _isMoving;
    [Space]
    [SerializeField] Vector3 _targetPos;
    [Space]
    [SerializeField] Vector3 _startPos;
    [Space]
    NavMeshAgentController _agentController;
   
    void OnEnable()
    {
        EventManager.onColorChange += MoveToPillar;

        EventManager.onPilarFix += MoveToStartPosition;
    }

    void OnDisable()
    {
        EventManager.onColorChange -= MoveToPillar;

        EventManager.onPilarFix -= MoveToStartPosition;
    }

    private void Start()
    {
        _agentController = GetComponent<NavMeshAgentController>();

        _startPos = transform.position;
    }

    public void MoveToPillar(Perk perk, Vector3 targetPosValue)
    {
        for (int i = 0; i < _playerPerks.Capacity; i++)
        {
            if (perk.perkType == _playerPerks[i].perkType)
            {
                StartCoroutine(MoveToTarget(targetPosValue));
            }
        }
    }

    public IEnumerator MoveToTarget(Vector3 target)
    {
        _isMoving = true;

        _agentController.agent.isStopped = false;

        _agentController.targetDistanceReached = false;

        while (true)
        {
            _agentController.SetAgentPathDestination(target);

            if (_agentController.targetDistanceReached)
            {
                _agentController.agent.isStopped = true;

                _isMoving = false;

                Debug.Log("Target Reached");

                yield break;
            }

            yield return null;
        }
    }

    public void MoveToStartPosition()
    {
        if (_isMoving)
        {
            Debug.Log("Move To Start Position");

            StopAllCoroutines();

            _agentController.agent.isStopped = true;

            _agentController.targetDistanceReached = true;

            _agentController.agent.ResetPath();

            _agentController.agent.velocity = Vector3.zero;

            StartCoroutine(MoveToTarget(_startPos));
        }
    }
}