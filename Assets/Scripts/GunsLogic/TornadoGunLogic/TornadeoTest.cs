using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadeoTest : MonoBehaviour
{
    [SerializeField] private ScriptableObjectTimeVortex tornadoParameters;

    private float attractionForce;
    private float attractionRadius;
    private float rotationSpeed;
    private float initialSpeed;
    private float destroyHeight;
    private float bounceForce;
    private float explosionForce;
    private LayerMask attractionLayer;

    private void Start()
    {
        attractionForce = tornadoParameters.GetAttractionForce();
        attractionRadius = tornadoParameters.GetAttractionRadius();
        rotationSpeed = tornadoParameters.GetRotationSpeed();
        initialSpeed = tornadoParameters.GetInitialSpeed();
        destroyHeight = tornadoParameters.GetDestroyHeight();
        bounceForce = tornadoParameters.GetBounceForce();
        explosionForce = tornadoParameters.GetExplosionForce();
        attractionLayer = tornadoParameters.GetAttractionLayer();
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.up * initialSpeed * Time.fixedDeltaTime;

        Collider[] colliders = Physics.OverlapSphere(transform.position, attractionRadius, attractionLayer);
        foreach (Collider col in colliders)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb != null && rb != GetComponent<Rigidbody>())
            {
                Vector3 direction = transform.position - col.transform.position;
                rb.AddForce(direction.normalized * attractionForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            }
        }

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (transform.position.y >= destroyHeight)
        {
            Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, attractionRadius, attractionLayer);
            foreach (Collider col in nearbyColliders)
            {
                Rigidbody rb = col.GetComponent<Rigidbody>();
                if (rb != null && rb != GetComponent<Rigidbody>())
                {
                    Vector3 randomDirection = Random.insideUnitSphere;
                    rb.AddForce(randomDirection * explosionForce, ForceMode.Impulse);
                }
            }
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attractionRadius);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 bounceDirection = collision.contacts[0].normal;
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }
    }
}
