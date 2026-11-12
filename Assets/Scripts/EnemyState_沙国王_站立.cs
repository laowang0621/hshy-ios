using System.Collections.Generic;
using UnityEngine;

public class EnemyState_沙国王_站立 : EnemyState_沙国王
{
	private float _1 = 1f;

	private float _2 = 5f;

	private float _朝向玩家Timer;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		base.Init(data);
		_1 = _Data._追击时间_x;
		_2 = _Data._追击时间_y;
		return this;
	}

	public override void OnEnter()
	{
		if (_mMono_沙国王._在地面)
		{
			base._Ani.Play("站立");
		}
		else
		{
			base._Ani.Play("趴");
		}
		_Timer = Random.Range(_1, _2);
		_朝向玩家Timer = 0.5f;
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		_朝向玩家Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			if (_mMono_沙国王._在地面 && F_爬柱子())
			{
				return;
			}
			if (_Data._视野范围有敌人)
			{
				if (!(Random.value < 0.1f) || !F_爬柱子())
				{
					ChangeState(EnemyStateName_Type.攻击);
				}
			}
			else
			{
				ChangeState(EnemyStateName_Type.站立);
			}
		}
		else if (_朝向玩家Timer <= 0f && _Data._视野范围有敌人)
		{
			_朝向玩家Timer = 0.5f;
			base._Mono.transform.localScale = new Vector3((_Data._Pos_追击目标位置.x > base._Mono.transform.position.x) ? 1 : (-1), 1f, 1f);
		}
	}

	private bool F_爬柱子()
	{
		Enemy_不济谷柱子 enemy_不济谷柱子 = F_判断爬柱子();
		if (enemy_不济谷柱子 != null)
		{
			if (_mMono_沙国王._当前柱子 != null)
			{
				_mMono_沙国王._当前柱子._On_碎裂 -= _mMono_沙国王.F_柱子碎裂;
			}
			_mMono_沙国王._当前柱子 = enemy_不济谷柱子;
			_mMono_沙国王._在地面 = false;
			_mMono_沙国王._当前柱子._On_碎裂 += _mMono_沙国王.F_柱子碎裂;
			ChangeState(EnemyStateName_Type.追击);
			return true;
		}
		return false;
	}

	private Enemy_不济谷柱子 F_判断爬柱子()
	{
		if (_mMono_沙国王._List_不济谷柱子.Count == 0)
		{
			return null;
		}
		List<Enemy_不济谷柱子> list = new List<Enemy_不济谷柱子>();
		for (int num = _mMono_沙国王._List_不济谷柱子.Count - 1; num >= 0; num--)
		{
			if (_mMono_沙国王._List_不济谷柱子[num]._Is死亡)
			{
				_mMono_沙国王._List_不济谷柱子.RemoveAt(num);
			}
			else
			{
				list.Add(_mMono_沙国王._List_不济谷柱子[num]);
			}
		}
		if (_mMono_沙国王._当前柱子 != null)
		{
			list.Remove(_mMono_沙国王._当前柱子);
		}
		if (list.Count == 0)
		{
			return null;
		}
		return list[Random.Range(0, list.Count)];
	}
}
