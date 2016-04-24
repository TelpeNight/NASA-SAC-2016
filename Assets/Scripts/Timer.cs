using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
	public event Action onTimeout;

	[SerializeField]
	float _timeLeft = 0;
	private bool _isStarted = false;


	// Use this for initialization
	void Start()
	{

	}

	public void Init(float timeoutInSeconds)
	{
		_timeLeft = timeoutInSeconds;
	}

	public void Run()
	{
		_isStarted = true;
	}

	public void Pause()
	{
		_isStarted = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (_isStarted)
		{
			_timeLeft -= Time.deltaTime;
			if (_timeLeft <= 0)
			{
				if (onTimeout != null)
				{
					onTimeout();
				}
			}
		}
	}
}
