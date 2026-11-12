using DSB;
using UnityEngine;

public class C_传送区域_小雷音寺大门 : MonoBehaviour
{
	private int _Next_MapID = -1;

	private int _Next_传送点ID = -1;

	public Vector2Int _黄眉1_Map_传送点;

	public Vector2Int _人种袋_Map_传送点;

	public Vector2Int _黄眉2_Map_传送点;

	public Transform _PlayerPos;

	[HideInInspector]
	public bool _已上锁;

	[HideInInspector]
	public bool _已上锁_解锁;

	public Vector2 F_GetPlayerPos()
	{
		return _PlayerPos.position;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!GameManager._CanPlay || (_已上锁 && !_已上锁_解锁) || _Next_MapID < 0 || _Next_传送点ID < 0)
		{
			return;
		}
		MyLog.LogError("C_传送区域_小雷音寺大门");
		if (collision.gameObject.layer != 3)
		{
			return;
		}
		MyLog.LogError("C_传送区域_小雷音寺大门2");
		if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断Boss是否已经击败(20308))
		{
			if (DSB_MonoSingleton<MyDataManager>.Instance.F_判断Boss是否已经击败(20307))
			{
				MyLog.LogError("C_传送区域_小雷音寺大门2  _黄眉2_Map_传送点");
				DSB_MonoSingleton<UIManager>.Instance.F_通过传送区域切换场景(_黄眉2_Map_传送点.x, _黄眉2_Map_传送点.y);
			}
			else
			{
				MyLog.LogError("C_传送区域_小雷音寺大门2 _人种袋_Map_传送点");
				DSB_MonoSingleton<UIManager>.Instance.F_通过传送区域切换场景(_人种袋_Map_传送点.x, _人种袋_Map_传送点.y);
			}
		}
		else
		{
			MyLog.LogError("C_传送区域_小雷音寺大门2 _人种袋_Map_传送点 _黄眉1_Map_传送点");
			DSB_MonoSingleton<UIManager>.Instance.F_通过传送区域切换场景(_黄眉1_Map_传送点.x, _黄眉1_Map_传送点.y);
		}
	}
}
