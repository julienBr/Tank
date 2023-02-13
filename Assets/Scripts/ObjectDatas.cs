using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData")]

public class ObjectDatas : ScriptableObject
{
    public float nbBullets;
    public float pv;
    public float detectionRange;
    public float trapRange;
    public float speed;
    public float speedBoost;
    public float maxBullets;
}