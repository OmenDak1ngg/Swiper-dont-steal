using UnityEngine;

public class Door : MonoBehaviour
{
    private readonly int IsOpen = Animator.StringToHash("IsOpen");

    [SerializeField] private DoorOpenerTrigger _doorOpener;
    [SerializeField] private Animator _animator;


    private void OnEnable()
    {
        _doorOpener.Entered += OpenDoor;
        _doorOpener.Exited += CloseDoor;
    }

    private void OnDisable()
    {
        _doorOpener.Entered -= OpenDoor;
        _doorOpener.Exited-= CloseDoor;
    }

    public void OpenDoor()
    {
        _animator.SetBool(IsOpen, true);
    }

    public void CloseDoor()
    {
        _animator.SetBool(IsOpen, false);
    }
}
