using UnityEngine;

public class Turret : Base_Controller
{
    [SerializeField] private GameObject turretTarget;
    [SerializeField] private Transform[] traps;
    [SerializeField] private float detectionRange = 6f;
    [SerializeField] private float trapRange = 4f;
    
    private void Update()
    {
        RotateToTarget(turretTarget.transform.position);
        if (CheckTrapDistance() || CheckTargetDistance())
        {
            Fire();
        }
    }

    private bool CheckTargetDistance()
    {
        if (Physics.Raycast(bulletSpawnPosition.position, bulletSpawnPosition.up, out RaycastHit hit, detectionRange))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                return true;
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