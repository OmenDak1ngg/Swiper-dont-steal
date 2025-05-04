using System.Collections;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField, Range(0.001f, 0.5f)] private float _volumeStep;
    [SerializeField] private DoorOpenerTrigger _doorOpener;

    private float _maxVolume = 1.0f;
    private float _minVolume = 0f;
    private float _targetVolume;
    private bool _isThiefInside = false;
    private Coroutine _activeCoroutine;

    private void Awake()
    {
        _audioSource.volume = 0f;
    }

    private void OnEnable()
    {
        _doorOpener.ThiefDetected += ChangeAlarmState;
    }

    private void OnDisable()
    {
        _doorOpener.ThiefDetected -= ChangeAlarmState;
    }

    private void ChangeAlarmState()
    {
        if (_activeCoroutine != null)
            StopCoroutine(_activeCoroutine);

        _activeCoroutine = StartCoroutine(ChangeAlarmVolume());
    }

    private IEnumerator ChangeAlarmVolume()
    {
        if (_isThiefInside)
        {
            _isThiefInside = false;
            _targetVolume = _minVolume;
        }
        else
        {
            _audioSource.Play();

            _isThiefInside = true;
            _targetVolume = _maxVolume;
        }

        while(_audioSource.volume != _targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, _volumeStep);
            yield return null;
        }

        if (_isThiefInside == false)
            _audioSource.Stop();
    }
}
