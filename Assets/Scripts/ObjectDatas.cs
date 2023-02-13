using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData")]

public class ObjectDatas : ScriptableObject
{
    public float maxBullets;
    public float nbBullets;
    public float actualTankNbBullets;
    public float pv;
    public float actualTankPv;
    public float maxHealth;
    public float detectionRange;
    public float trapRange;
    public float speed;
    public float speedBoost;
}