using System;
using System.Collections.Generic;
using UnityEngine;

public class C_滑沙_障碍物 : MonoBehaviour, IEnemy
{
	[SerializeField]
	protected Collider2D _Co2D;

	[SerializeField]
	protected GameObject _GO_死亡特效;

	public int _阵营 = 2;

	public float _伤害 = 20f;

	public float _击退力度 = 20f;

	protected HashSet<IBeAttack> _Hsah_本次受击人员 = new HashSet<IBeAttack>();

	public bool _Is死亡;

	public event Action _攻击命中回调 = delegate
	{
	};

	private void OnEnable()
	{
		_Hsah_本次受击人员.Clear();
	}

	public void F_显示()
	{
		if (_Is死亡)
		{
			base.gameObject.SetActive(value: false);
			return;
		}
		_Co2D.enabled = true;
		base.gameObject.SetActive(value: true);
	}

	public void F_重置()
	{
		_Is死亡 = false;
		_Co2D.enabled = true;
	}

	public void F_隐藏()
	{
		if (_Is死亡)
		{
			base.gameObject.SetActive(value: false);
			return;
		}
		base.gameObject.SetActive(value: true);
		_Co2D.enabled = true;
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
				atk_僵直 = 0f,
				atk_击退力度 = _击退力度,
				atk_Pos = base.transform.position,
				atk_Is远程 = false,
				atk_Is地面 = false,
				atk_强破坏力 = false
			});
			_Is死亡 = true;
			UnityEngine.Object.Instantiate(_GO_死亡特效, base.transform.position, Quaternion.identity);
			base.gameObject.SetActive(value: false);
		}
	}
}
