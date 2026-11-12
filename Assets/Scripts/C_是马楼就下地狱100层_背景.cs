using UnityEngine;

public class C_是马楼就下地狱100层_背景 : C_是马楼就下地狱100层_墙壁
{
	public float _速度 = 1f;

	public override void F_Init()
	{
		base.F_Init();
		Vector3 localPosition = _Tsf_锚点.localPosition;
		localPosition.y = 0f;
		_Tsf_锚点.localPosition = localPosition;
	}

	public override void F_镜头移动调用(float 镜头y)
	{
		Vector3 position = _Tsf_锚点.position;
		position.y -= _速度 * Time.deltaTime;
		_Tsf_锚点.position = position;
		base.F_镜头移动调用(镜头y);
	}
}
