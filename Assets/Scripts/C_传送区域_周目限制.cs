using DSB;
using UnityEngine;

public class C_传送区域_周目限制 : MonoBehaviour
{
	public int _几周目后开启 = 2;

	public int _Next_MapID = -1;

	public int _Next_传送点ID = -1;

	public Transform _PlayerPos;

	[HideInInspector]
	public bool _已上锁;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (GameManager._CanPlay && !_已上锁 && _Next_MapID >= 0 && _Next_传送点ID >= 0 && collision.gameObject.layer == 3)
		{
			DSB_MonoSingleton<UIManager>.Instance.F_通过传送区域切换场景(_Next_MapID, _Next_传送点ID);
		}
	}

	private void OnEnable()
	{
		if (DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._周目 >= 2)
		{
			_已上锁 = false;
		}
		else
		{
			_已上锁 = true;
		}
	}
}
