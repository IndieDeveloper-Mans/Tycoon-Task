using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Perk
{
    public enum PerkType
    {
        green,
        blue,
        red
    }

    public PerkType perkType;

    [Space]
    public Color _color;
}