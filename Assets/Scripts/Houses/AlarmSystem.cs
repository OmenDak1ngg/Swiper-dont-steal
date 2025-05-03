using System.Collections;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField, Range(0.001f, 0.5f)] private float _volumeStep;
    [SerializeField, Range(0.001f, 2f)] private float _volumeFadeInterval;

    private WaitForSeconds _volumeChangeDelay;
    private float _maxVolume = 1.0f;
    private float _minVolume = 0f;
    private bool _isThiefInside;

    private void Awake()
    {
        _volumeChangeDelay = new WaitForSeconds(_volumeFadeInterval);
        _audioSource.volume = 0f;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Movement>(out _))
            StartCoroutine(Alarm());
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<Movement>(out _))
            StartCoroutine(StopAlarm());
    }

    private IEnumerator Alarm()
    {
        _audioSource.Play();
        _isThiefInside = true;

        while (_isThiefInside && _audioSource.volume < _maxVolume)
        {
            yield return _volumeChangeDelay;
            _audioSource.volume += _volumeStep;
        }
    }

    private IEnumerator StopAlarm()
    {
        _isThiefInside = false;

        while (_audioSource.volume > _minVolume && _isThiefInside == false)
        {
            _audioSource.volume -= _volumeStep;
            yield return _volumeChangeDelay;
        }

        _audioSource.Stop();
    }
}
