using System;
using UnityEngine;

[Serializable]
public class C_水面_生成_角色移动波纹_单个角色信息
{
	public Transform _Tsf;

	private Vector2 lastPos;

	private int 轮询次数;

	public C_水面_生成_角色移动波纹_单个角色信息(Transform tsf)
	{
		_Tsf = tsf;
		lastPos = _Tsf.position;
		轮询次数 = 0;
	}

	public bool F_轮询()
	{
		轮询次数++;
		if (轮询次数 > 3)
		{
			轮询次数 = 0;
			lastPos = _Tsf.position;
			return true;
		}
		float num = Vector2.Distance(lastPos, _Tsf.position);
		lastPos = _Tsf.position;
		if (num < 0.5f)
		{
			return false;
		}
		轮询次数 = 0;
		return true;
	}
}
