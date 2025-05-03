using System.Collections;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField, Range(0.001f, 0.5f)] private float _volumeStep;
    [SerializeField, Range(0.001f, 2f)] private float _volumeFadeInterval;
    [SerializeField] private DoorOpenerTrigger _doorOpener;

    private WaitForSeconds _volumeChangeDelay;
    private float _maxVolume = 1.0f;
    private float _minVolume = 0f;
    private bool _isThiefInside;

    private void Awake()
    {
        _volumeChangeDelay = new WaitForSeconds(_volumeFadeInterval);
        _audioSource.volume = 0f;
        _doorOpener.IsThiefInside += ChangeAlarmState;
    }

    private void OnDestroy()
    {
        _doorOpener.IsThiefInside -= ChangeAlarmState;
    }

    private void ChangeAlarmState()
    {
        StartCoroutine(FadeVolume());
    }

    private IEnumerator FadeVolume() 
    {
        if (_isThiefInside)
        {
            _isThiefInside = false;

            while (_audioSource.volume > _minVolume && _isThiefInside == false)
            {
                _audioSource.volume -= _volumeStep;
                yield return _volumeChangeDelay;
            }

            _audioSource.Stop();
        }
        else
        {
            _audioSource.Play();
            _isThiefInside = true;

            while (_isThiefInside && _audioSource.volume < _maxVolume)
            {
                yield return _volumeChangeDelay;
                _audioSource.volume += _volumeStep;
            }
        }
    }
}
