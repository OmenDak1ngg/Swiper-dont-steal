using UnityEngine;

public class Door : MonoBehaviour
{
    private readonly int IsOpen = Animator.StringToHash("IsOpen");

    [SerializeField] private Animator _animator;

    public  void Close()
    {
        _animator.SetBool(IsOpen, false);
    }

    public void Open()
    {
        _animator.SetBool(IsOpen, true);
    }
}
