using System;
using System.Collections.Generic;
using UnityEngine;

public class C_攻击判断 : MonoBehaviour
{
	public int _阵营;

	public float _伤害 = 10f;

	public float _僵直伤害 = 10f;

	public float _击退力度 = 5f;

	public bool _是地面伤害;

	public bool _可以被闪避 = true;

	public bool _是强破坏力;

	public bool _开启僵直效果 = true;

	private HashSet<IBeAttack> _Hsah_本次受击人员 = new HashSet<IBeAttack>();

	public event Action _攻击命中回调 = delegate
	{
	};

	private void OnEnable()
	{
		F_清空_本次受击人员();
	}

	public void F_清空_本次受击人员()
	{
		_Hsah_本次受击人员.Clear();
	}

	public void F_对范围人员造成伤害()
	{
		foreach (IBeAttack item in _Hsah_本次受击人员)
		{
			if (item != null)
			{
				F_对角色造成伤害(item);
			}
		}
	}

	private void F_对角色造成伤害(IBeAttack Character)
	{
		if (base.transform.parent == null)
		{
			Character.BeAttack(new AttackInfo
			{
				atk_生命 = _伤害,
				atk_僵直 = _僵直伤害,
				atk_击退力度 = _击退力度,
				atk_Pos = base.transform.position,
				atk_Is远程 = false,
				atk_Is地面 = _是地面伤害,
				atk_强破坏力 = _是强破坏力,
				atk_有僵直 = _开启僵直效果,
				atk_可以被闪避 = _可以被闪避
			});
		}
		else
		{
			Character.BeAttack(new AttackInfo
			{
				atk_生命 = _伤害,
				atk_僵直 = _僵直伤害,
				atk_击退力度 = _击退力度,
				atk_Pos = base.transform.parent.position,
				atk_Is远程 = false,
				atk_Is地面 = _是地面伤害,
				atk_强破坏力 = _是强破坏力,
				atk_有僵直 = _开启僵直效果,
				atk_可以被闪避 = _可以被闪避
			});
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<IBeAttack>(out var component) && component._阵营 != _阵营 && !_Hsah_本次受击人员.Contains(component))
		{
			this._攻击命中回调?.Invoke();
			_Hsah_本次受击人员.Add(component);
			F_对角色造成伤害(component);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.TryGetComponent<IBeAttack>(out var component) && _Hsah_本次受击人员.Contains(component))
		{
			_Hsah_本次受击人员.Remove(component);
		}
	}
}
