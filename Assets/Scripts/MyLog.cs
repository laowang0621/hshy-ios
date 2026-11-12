using UnityEngine;

public static class MyLog
{
	public static void Log(object message)
	{
		if (Lunch._开启Debug)
		{
			Debug.Log(message);
		}
	}

	public static void LogWarning(object message)
	{
		if (Lunch._开启Debug)
		{
			Debug.LogWarning(message);
		}
	}

	public static void LogError(object message)
	{
		if (Lunch._开启Debug)
		{
			Debug.LogError(message);
		}
	}
}
