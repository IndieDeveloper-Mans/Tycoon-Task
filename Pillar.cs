using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    MeshRenderer _meshRenderer;
    [Space]
    [SerializeField] List<Perk> _perks;
    [Space]
    [SerializeField] Perk _currentPerk;
    [Space]
    [SerializeField] Color _defaultColor;
    [Space]
    public bool hasDefaultColor = true;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        _meshRenderer.material.color = _defaultColor;
    }

    public void SetColor()
    {
        int randomizer = Random.Range(0, _perks.Capacity);

        Color choosenColor = _perks[randomizer]._color;

        _meshRenderer.material.color = choosenColor;

        _currentPerk.perkType = _perks[randomizer].perkType;

        _currentPerk._color = _perks[randomizer]._color;

        hasDefaultColor = false;

        EventManager.onColorChange?.Invoke(_perks[randomizer], transform.position);
    }

    public void ResetColor()
    {
        _meshRenderer.material.color = _defaultColor;

        hasDefaultColor = true;
    }
}