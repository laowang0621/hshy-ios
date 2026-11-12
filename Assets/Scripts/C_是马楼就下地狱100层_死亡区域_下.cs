using UnityEngine;

public class C_是马楼就下地狱100层_死亡区域_下 : MonoBehaviour
{
	private C_是马楼就下地狱100层 _是马楼就下地狱100层;

	public void F_Init(C_是马楼就下地狱100层 是马楼就下地狱100层)
	{
		_是马楼就下地狱100层 = 是马楼就下地狱100层;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3 && collision.gameObject.CompareTag("Player") && collision.TryGetComponent<PlayerController>(out var component) && !component.F_玩家已经死亡())
		{
			component.F_杀死玩家();
			_是马楼就下地狱100层.F_停止背景墙壁等移动();
		}
	}
}
