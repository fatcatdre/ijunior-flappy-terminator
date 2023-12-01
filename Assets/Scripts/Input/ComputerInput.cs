using UnityEngine;
using System.Collections;

public class ComputerInput : InputSource
{
    [SerializeField] private float _delayBetweenPresses;
    [SerializeField] private float _delaySpread;

    private bool _isShootButtonPressed;
    private WaitForSecondsRandom _randomDelay;

    public override bool IsShooting => _isShootButtonPressed;

    private void Awake()
    {
        _randomDelay = new WaitForSecondsRandom(
            _delayBetweenPresses - _delaySpread,
            _delayBetweenPresses + _delaySpread);
    }

    private void OnEnable()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (enabled)
        {
            _isShootButtonPressed = false;

            yield return _randomDelay;

            _isShootButtonPressed = true;

            _randomDelay.Reset();

            yield return null;
        }
    }
}
