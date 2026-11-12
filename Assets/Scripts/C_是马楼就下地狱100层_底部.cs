using DSB;
using UnityEngine;

public class C_是马楼就下地狱100层_底部 : MonoBehaviour
{
	private bool _到达底部;

	private C_是马楼就下地狱100层 _是马楼就下地狱100层;

	public Transform _Tsf_摄像机下边缘;

	public void F_Init(C_是马楼就下地狱100层 是马楼就下地狱100层)
	{
		_是马楼就下地狱100层 = 是马楼就下地狱100层;
		_到达底部 = false;
		base.gameObject.SetActive(value: false);
	}

	public void F_到达底部(float posY)
	{
		_到达底部 = true;
		base.gameObject.SetActive(value: true);
		base.transform.position = new Vector2(0f, posY);
		_是马楼就下地狱100层.F_设置_镜头移动下限(_Tsf_摄像机下边缘.position.y);
	}

	public void F_镜头移动调用(float 镜头y, ref bool _背景墙壁等可以运动)
	{
		if (_到达底部 && _背景墙壁等可以运动 && 镜头y - Lunch._镜头默认大小 > base.transform.position.y)
		{
			_背景墙壁等可以运动 = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3 && collision.gameObject.CompareTag("Player") && collision.TryGetComponent<PlayerController>(out var _))
		{
			_是马楼就下地狱100层.F_恢复玩家视野();
			MyLog.LogError("C_是马楼就下地狱100层_底部: F_地狱100层中让玩家转起来");
			DSB_MonoSingleton<GameManager>.Instance._PC.F_地狱100层中让玩家转起来();
		}
	}
}
