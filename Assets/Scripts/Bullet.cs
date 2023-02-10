using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 10f;
    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.up * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}