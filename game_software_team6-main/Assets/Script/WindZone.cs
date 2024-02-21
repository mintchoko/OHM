using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    [SerializeField]
    float _windForce = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Start changing wind direction every 5 second
        InvokeRepeating("ChangeWindDirection", 0f, 5f);
    }

    private void ChangeWindDirection()
    {
        // Generate a random angle in radians
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);

        // Calculate the direction vector in the horizontal plane
        Vector3 newDirection = new Vector3(Mathf.Cos(randomAngle), 0f, Mathf.Sin(randomAngle));

        // Assign the new direction to the wind zone's forward vector
        transform.forward = newDirection;

        // You might also want to log the new wind direction
        Debug.Log("New Wind Direction: " + newDirection);
    }

    private void OnTriggerStay(Collider other)
    {
        var hitObj = other.gameObject;
        if (hitObj != null)
        {
            var rb = hitObj.GetComponent<Rigidbody>();
            var dir = transform.forward; // Use forward instead of up for horizontal direction
            rb.AddForce(dir * _windForce);
        }
    }
}
