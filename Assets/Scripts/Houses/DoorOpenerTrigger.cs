using UnityEngine;
using System;

public class DoorOpenerTrigger : MonoBehaviour
{
    public event Action ThiefDetected;
    public event Action<bool> DetectedSomeone;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Movement>(out _))
        {
            DetectedSomeone?.Invoke(true);
            ThiefDetected?.Invoke();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<Movement>(out _))
        {
            DetectedSomeone?.Invoke(false);
            ThiefDetected?.Invoke();
        }
    }
}

