using UnityEngine;
using System;

public class DoorOpenerTrigger : MonoBehaviour
{
    public event Action Exited;
    public event Action Entered;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Mover>(out _))
        {
            Entered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<Mover>(out _))
        {
            Exited?.Invoke();
        }
    }
}

