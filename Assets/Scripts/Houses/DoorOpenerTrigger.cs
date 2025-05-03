using UnityEngine;

public class DoorOpenerTrigger : MonoBehaviour
{
    [SerializeField] private Door _door;

    private bool _hasOpener = false;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.TryGetComponent<Movement>(out _))
        {
            _hasOpener = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<Movement>(out _))
        {
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

