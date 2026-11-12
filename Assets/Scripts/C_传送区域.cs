using DSB;
using UnityEngine;

public class C_传送区域 : MonoBehaviour
{
	public int _Next_MapID = -1;

	public int _Next_传送点ID = -1;

	public Transform _PlayerPos;

	[HideInInspector]
	public bool _已上锁;

	[HideInInspector]
	public bool _已上锁_解锁;

	public Vector2 F_GetPlayerPos()
	{
		return _PlayerPos.position;
	}

	public void F_显示()
	{
		_已上锁 = false;
		_已上锁_解锁 = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (GameManager._CanPlay && (!_已上锁 || _已上锁_解锁) && _Next_MapID >= 0 && _Next_传送点ID >= 0 && collision.gameObject.layer == 3)
		{
			DSB_MonoSingleton<UIManager>.Instance.F_通过传送区域切换场景(_Next_MapID, _Next_传送点ID);
		}
	}
}
