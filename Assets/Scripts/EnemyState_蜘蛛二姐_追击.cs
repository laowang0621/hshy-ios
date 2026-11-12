using UnityEngine;

public class EnemyState_蜘蛛二姐_追击 : EnemyState_Boss_追击
{
	public override void OnEnter()
	{
		_累计追击判定次数++;
		Vector2 vector = base._Mono.transform.position;
		Vector2 vector2 = _Data._Pos_追击目标位置;
		_Timer = _间隔;
		base._Mono.transform.localScale = new Vector3((vector2.x > vector.x) ? 1 : (-1), 1f, 1f);
		if (Vector2.Distance(vector, vector2) < 6f)
		{
			base._Ani.Play("站立");
			_MoveDirSpeed = Vector2.zero;
		}
		else
		{
			Vector2 vector3 = vector2 + new Vector2((float)((vector2.x < vector.x) ? 1 : (-1)) * Random.Range(3f, 5f), Random.Range(-0.5f, 0.5f));
			_MoveDirSpeed = (vector3 - vector).normalized * _MoveSpeed;
			base._Mono组件._Tsf_移动目标.transform.position = vector3;
		}
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (!(_Timer > 0f))
		{
			if (F_判断出手())
			{
				base._Mono.ChangeState(EnemyStateName_Type.攻击);
			}
			else
			{
				base._Mono.ChangeState(EnemyStateName_Type.追击);
			}
		}
	}

	private bool F_判断出手()
	{
		if (_累计追击判定次数 > _判断X次后出手)
		{
			_累计追击判定次数 = 0;
			_判断X次后出手 = Random.Range(_1, _2);
			return true;
		}
		return false;
	}
}
