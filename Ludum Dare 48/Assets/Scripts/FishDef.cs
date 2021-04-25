using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fish", menuName = "Fish")]
public class FishDef : ScriptableObject
{
    public Fish Prefab;

    public int Count;

    public float Spacing;
}
