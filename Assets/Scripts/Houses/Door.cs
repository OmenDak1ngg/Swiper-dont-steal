using UnityEngine;

public class Door : MonoBehaviour
{
    private readonly int IsOpen = Animator.StringToHash("IsOpen");

    [SerializeField] private DoorOpenerTrigger _doorOpener;
    [SerializeField] private Animator _animator;


    private void OnEnable()
    {
        _doorOpener.DetectedSomeone += ToggleDoor;
    }

    public void ToggleDoor(bool doorState)
    {
        _animator.SetBool(IsOpen, doorState);
    }
}
