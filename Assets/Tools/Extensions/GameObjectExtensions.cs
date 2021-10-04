using System;
using System.Collections;
using UnityEngine;

public static class MonoBehaviourExtensions
{

	public static Coroutine DoAfterSeconds(this MonoBehaviour monoBehaviour, float seconds, Action onFinish)
	{
		if (seconds > 0 && onFinish != null)
			return monoBehaviour.StartCoroutine(DOAfterSeconds_Co(seconds, onFinish));

		return null;
	}

	static IEnumerator DOAfterSeconds_Co(float seconds, Action onFinish)
	{
		float currentTime = 0f;
		while (currentTime < seconds)
		{
			yield return null;

			currentTime += Time.deltaTime;
		}
		
		onFinish.Invoke();
	}
	
}
