using System;
using UnityEngine;

public class Turret : Base_Controller
{
    [SerializeField] private GameObject turretTarget;
    [SerializeField] private Transform[] traps;
    [SerializeField] private float detectionRange;
    [SerializeField] private float trapRange;

    private void Start()
    {
        nbBullets = choice.actualDifficulty.turret.nbBullets;
        pv = choice.actualDifficulty.turret.pv;
        detectionRange = choice.actualDifficulty.turret.detectionRange;
        trapRange = choice.actualDifficulty.turret.trapRange;
    }

    private void Update()
    {
        foreach(Transform spawn in bulletSpawnPosition) Debug.DrawRay(spawn.position, spawn.up * detectionRange, Color.red);
        foreach(Transform trap in traps) Debug.DrawRay(trap.position, trap.forward * trapRange, Color.red);
        RotateToTarget(turretTarget.transform.position);
        if (CheckTrapDistance() || CheckTargetDistance())
        {
            Fire();
        }
    }

    private bool CheckTargetDistance()
    {
        foreach (Transform spawn in bulletSpawnPosition)
        {
            if (Physics.Raycast(spawn.position, spawn.up, out RaycastHit hit, detectionRange))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }
    
    private bool CheckTrapDistance()
    {
        foreach (Transform trap in traps)
        {
            if (Physics.Raycast(trap.position, trap.forward, out RaycastHit hit, trapRange))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            UpdatePv();
        }
    }
}