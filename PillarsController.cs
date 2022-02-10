using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PillarsController : MonoBehaviour
{
    [SerializeField] List<Pillar> _pillars;
    [Space]
    [SerializeField] float _changeColorDelay;
    [Space]
    public Pillar activePillar;

    private void Start()
    {
        ActivatePillar();
    }

    public IEnumerator ChangeColorCoroutine()
    {
        yield return new WaitForSeconds(_changeColorDelay);

        ChoosePillar();
    }

    public void ChoosePillar()
    {
        foreach (var pillar in _pillars)
        {
            if (!pillar.hasDefaultColor)
            {
                return;
            }
        }

        int randomizer = Random.Range(0, _pillars.Capacity);

        activePillar = _pillars[randomizer];

        _pillars[randomizer].SetColor();
    }

    public void ActivatePillar()
    {
        StartCoroutine(ChangeColorCoroutine());
    }
}