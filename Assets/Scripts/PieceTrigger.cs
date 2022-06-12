using UnityEngine;

public class PieceTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Falling"))
            Destroy(other.gameObject);
    }
}
