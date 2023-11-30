using UnityEngine;
using System.Collections;

public class ComputerInput : InputSource
{
    [SerializeField] private float _delayBetweenPresses;
    [SerializeField] private float _delaySpread;

    private bool _isShootButtonPressed;

    public override bool IsShooting => _isShootButtonPressed;

    private void OnEnable()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        float elapsedTime = 0f;

        while (enabled)
        {
            _isShootButtonPressed = false;

            float delay = _delayBetweenPresses += Random.Range(-_delaySpread, _delaySpread);

            while (elapsedTime < delay)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _isShootButtonPressed = true;

            elapsedTime = 0f;

            yield return null;
        }
    }
}
