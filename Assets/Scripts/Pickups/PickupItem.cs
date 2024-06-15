using UnityEngine;

public abstract class PickupItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnPickedUp(other.gameObject);

        Destroy(gameObject);
    }

    protected abstract void OnPickedUp(GameObject receiver);
}
