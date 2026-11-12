using System;
using DSB;
using UnityEngine;

public class Enemy_精魄生成 : MonoBehaviour
{
	public C_精魄 _精魄;

	[Header("'_解锁精魄ID' 同 '_宝箱ID' 主要用于同步位置(玩家未拾取需要保留)")]
	public int _解锁精魄ID;

	private void Awake()
	{
		_精魄._解锁精魄ID = _解锁精魄ID;
		EnemyMono组件 component = GetComponent<EnemyMono组件>();
		if (!(component != null))
		{
			return;
		}
		component._Event_Evemy隐藏 = (Action)Delegate.Combine(component._Event_Evemy隐藏, (Action)delegate
		{
			_精魄.F_隐藏();
		});
		component._Event_Evemy显示 = (Action<Action>)Delegate.Combine(component._Event_Evemy显示, (Action<Action>)delegate(Action action)
		{
			if (DSB_MonoSingleton<MyDataManager>.Instance.F_TryGet空降宝箱位置(_解锁精魄ID, out var 宝箱pos))
			{
				action();
				if (F_判断精魄是否解锁())
				{
					MyLog.LogError("该精魄已经获得");
				}
				else
				{
					_精魄.F_显示(宝箱pos);
				}
			}
		});
		component._Event_Evemy死亡 = (Action)Delegate.Combine(component._Event_Evemy死亡, (Action)delegate
		{
			if (F_判断精魄是否解锁())
			{
				MyLog.LogError("该精魄已经获得");
			}
			else
			{
				Vector2 pos = base.transform.position;
				DSB_MonoSingleton<MyDataManager>.Instance.F_保存空降宝箱位置(_解锁精魄ID, pos);
				DSB_MonoSingleton<MyDataManager>.Instance.F_保存数据();
				_精魄.F_显示(pos);
			}
		});
	}

	private bool F_判断精魄是否解锁()
	{
		if (DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._List_已解锁_精魄.Contains(_解锁精魄ID))
		{
			return true;
		}
		return false;
	}
}
