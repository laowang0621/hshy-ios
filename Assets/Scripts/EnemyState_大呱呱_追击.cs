using UnityEngine;

public class EnemyState_大呱呱_追击 : EnemyState_Boss_追击
{
	private int _前X次有跳跃;

	public override void OnEnter()
	{
		_累计追击判定次数++;
		if (_前X次有跳跃 < 0 && Random.value < 0.4f)
		{
			_前X次有跳跃 = 8;
			base._Ani.Play("跳跃");
			Vector2 insideUnitCircle = Random.insideUnitCircle;
			float num = Mathf.Abs(base._Mono.transform.localScale.x);
			base._Mono.transform.localScale = new Vector3((insideUnitCircle.x > 0f) ? num : (0f - num), num, num);
			base._Rb2D.velocity = insideUnitCircle * 40f;
			_Timer = 0.33f;
		}
		else
		{
			base._Ani.Play("站立");
			base._Rb2D.velocity = Vector2.zero;
			_前X次有跳跃--;
			_Timer = _间隔;
			Vector2 vector = base._Mono.transform.position;
			Vector2 vector2 = _Data._Pos_追击目标位置;
			float num2 = Mathf.Abs(base._Mono.transform.localScale.x);
			base._Mono.transform.localScale = new Vector3((vector2.x > vector.x) ? num2 : (0f - num2), num2, num2);
		}
	}

	public override void OnExit()
	{
		base._Ani.Play("站立");
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
		if (_累计追击判定次数 > Random.Range(_1, _2))
		{
			_累计追击判定次数 = 0;
			return true;
		}
		return false;
	}
}
