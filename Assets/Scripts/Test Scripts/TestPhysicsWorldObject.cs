using NaughtyAttributes;
using StardropTools;
using UnityEngine;

public class TestPhysicsWorldObject : WorldPhysicsObject
{
    [Header("Random Force Settings")]
    [SerializeField] private float maxRange = 1f;
    [SerializeField] private float maxForce = 5f;

    [Header("Random Tween Force Settings")]
    [SerializeField] private float maxInitialVelocity = 5f;
    [SerializeField] private float maxAcceleration = 5f;
    [SerializeField] private float maxDuration = 3f;

    [Button("Add Random Force")]
    public void AddRandomForce()
    {
        // Generate a random direction
        Vector3 randomDirection = new Vector3(
            Random.Range(-maxRange, maxRange),
            Random.Range(-maxRange, maxRange),
            Random.Range(-maxRange, maxRange)
        ).normalized;

        // Generate a random force magnitude
        float randomForceMagnitude = Random.Range(5f, maxForce);

        // Apply the force
        AddForce(randomForceMagnitude, randomDirection, ForceMode.Impulse);
    }
}
