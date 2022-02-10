using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPillarTrigger : MonoBehaviour
{
    [SerializeField] PillarsController _pillarsController;

    private void Start()
    {
        if (_pillarsController == null)
        {
            _pillarsController = GetComponentInParent<PillarsController>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var playerController))
        {
            if (!_pillarsController.activePillar.hasDefaultColor)
            {
                _pillarsController.activePillar.ResetColor();

                _pillarsController.ActivatePillar();

                EventManager.onPilarFix?.Invoke();
            }
        }
    }
}