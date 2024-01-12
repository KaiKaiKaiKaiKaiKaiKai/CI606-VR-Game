using UnityEngine;

public class FloodWater : MonoBehaviour
{
    // Define a delegate that takes a GameObject as a parameter
    public delegate void CollisionHandler(GameObject collidedWith);

    // Define an event of type CollisionHandler
    public event CollisionHandler OnCollisionOccurred;

    private void OnTriggerEnter(Collider other)
    {
        // When the trigger is entered, raise the event and pass the collided GameObject
        OnCollisionOccurred?.Invoke(other.gameObject);
    }
}