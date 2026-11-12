using System;
using System.Collections.Generic;
using DSB;
using UnityEngine;

public class C_弹道 : MonoBehaviour
{
	[SerializeField]
	protected Rigidbody2D _RB2D;

	[SerializeField]
	protected Collider2D _Col2D;

	public bool _命中销毁 = true;

	public float _击退力度 = 5f;

	public int _阵营;

	public float _伤害 = 10f;

	public float _僵直伤害;

	public bool _是远程伤害 = true;

	public bool _能被反弹;

	public bool _有僵直 = true;

	public float _持续时间 = 5f;

	protected HashSet<IBeAttack> _Hsah_本次受击人员 = new HashSet<IBeAttack>();

	public float _speed = 20f;

	public event Action _攻击命中回调 = delegate
	{
	};

	public virtual void F_Init_按距离销毁(Vector2 dir, float dis)
	{
		_RB2D.velocity = dir * _speed;
		DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(base.gameObject, dis / 20f, F_销毁);
	}

	public virtual void F_Init(Vector2 dir)
	{
		_RB2D.velocity = dir * _speed;
		DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(base.gameObject, _持续时间, F_销毁);
	}

	public virtual void F_Init(Vector2 dir, float 伤害)
	{
		_RB2D.velocity = dir * _speed;
		_伤害 = 伤害;
		DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(base.gameObject, _持续时间, F_销毁);
	}

	public virtual void F_Init(Vector2 dir, float 伤害, IBeAttack 排除对象)
	{
		if (排除对象 != null)
		{
			_Hsah_本次受击人员.Add(排除对象);
		}
		_RB2D.velocity = dir * _speed;
		_伤害 = 伤害;
		DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(base.gameObject, _持续时间, F_销毁);
	}

	public void F_销毁()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	protected void F_销毁(DSB_EventArgs args)
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	protected void OnEnable()
	{
		_Hsah_本次受击人员.Clear();
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_切换地图_开始._EventID, F_销毁);
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_回到主页._EventID, F_销毁);
	}

	protected virtual void OnDestroy()
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.UnSubscribe(EventArgs_切换地图_开始._EventID, F_销毁);
		DSB_MonoSingleton<DSB_EventManager>.Instance.UnSubscribe(EventArgs_回到主页._EventID, F_销毁);
	}

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<IBeAttack>(out var component) && component._阵营 != _阵营 && !_Hsah_本次受击人员.Contains(component))
		{
			this._攻击命中回调();
			_Hsah_本次受击人员.Add(component);
			component.BeAttack(new AttackInfo
			{
				atk_生命 = _伤害,
				atk_僵直 = _僵直伤害,
				atk_击退力度 = _击退力度,
				atk_Pos = base.transform.position,
				atk_Is远程 = _是远程伤害,
				atk_Is地面 = false,
				atk_强破坏力 = false,
				atk_有僵直 = _有僵直
			});
			if (_是远程伤害 && _能被反弹 && component._开启反弹弹道)
			{
				F_反弹(component._阵营);
			}
			else if (_命中销毁)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	protected virtual void F_反弹(int 阵营)
	{
		_RB2D.velocity *= -1f;
		_伤害 *= 3f;
		_阵营 = 阵营;
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_反弹箭();
	}
}
