using System.Collections;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField, Range(0.001f, 0.5f)] private float _volumeStep;
    [SerializeField] private DoorOpenerTrigger _doorOpener;

    private float _maxVolume = 1.0f;
    private float _minVolume = 0f;
    private int _countOfThiefes;

    private void Awake()
    {
        _audioSource.volume = 0f;   
    }

    private void OnEnable()
    {
        _doorOpener.Exited += StopAlarm;
        _doorOpener.Entered += StartAlarm;
    }

    private void OnDisable()
    {
        _doorOpener.Exited-= StopAlarm;
        _doorOpener.Entered-= StartAlarm;
    }

    private void StartAlarm()
    {
        _countOfThiefes++;

        if (_countOfThiefes > 1)
            return;

        StartCoroutine(TransitionVolume(_maxVolume));
        _audioSource.Play();
    }

    private void StopAlarm()
    {
        _countOfThiefes--;

        if (_countOfThiefes != 0)
            return;

        StartCoroutine(TransitionVolume(_minVolume));
    }

    private  IEnumerator TransitionVolume(float targetVolume)
    {
        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _volumeStep);
            yield return null;
        }

        if(_audioSource.volume == _minVolume)
        {
            _audioSource.Stop();
        }
    }
}
