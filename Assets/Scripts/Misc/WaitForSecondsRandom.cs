using UnityEngine;

public class WaitForSecondsRandom : CustomYieldInstruction
{
    private float _minSeconds;
    private float _maxSeconds;
    private float _endTime;

    public WaitForSecondsRandom(float minSeconds, float maxSeconds)
    {
        _minSeconds = minSeconds;
        _maxSeconds = maxSeconds;

        GenerateRandomEndTime();
    }

    public override void Reset()
    {
        GenerateRandomEndTime();
    }

    public override bool keepWaiting => Time.time < _endTime;

    private void GenerateRandomEndTime()
    {
        _endTime = Time.time + Random.Range(_minSeconds, _maxSeconds);
    }
}
