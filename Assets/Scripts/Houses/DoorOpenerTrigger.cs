using UnityEngine;
using System;

public class DoorOpenerTrigger : MonoBehaviour
{
    [SerializeField] private Door _door;

    private bool _hasOpener = false;

    public event Action IsThiefInside;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.TryGetComponent<Movement>(out _))
        {
            IsThiefInside?.Invoke();
            _hasOpener = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<Movement>(out _))
        {
            IsThiefInside?.Invoke();
            _hasOpener = false;
        }
    }

    private void Update()
    {
        if(_hasOpener) 
            _door.Open();
        else
            _door.Close();
    }
}

