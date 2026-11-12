using UnityEngine;

public class EnemyState_幽魂_追击 : EnemyState_Boss
{
	private Vector2 _targetPos;

	private Vector2 _StartPos;

	private float _Speed = 2f;

	private float _Speed1 = 2.25f;

	private float _Speed2 = 4f;

	public float _Distance;

	private float _累计判定距离;

	private int _累计判定次数;

	public override void OnEnter()
	{
		if (!_Data._视野范围有敌人)
		{
			base._Mono.ChangeState(EnemyStateName_Type.站立);
			return;
		}
		base._Ani.Play("移动");
		_StartPos = base._Mono.transform.position;
		Vector2 vector = _Data._Pos_追击目标位置;
		_targetPos = vector + new Vector2((float)((vector.x < _StartPos.x) ? 1 : (-1)) * Random.Range(1.5f, 4f), Random.Range(-0.5f, 0.5f));
		base._Mono.transform.localScale = new Vector3((vector.x < _StartPos.x) ? 1 : (-1), 1f, 1f);
		base._Mono组件._Tsf_移动目标.transform.position = _targetPos;
		_Distance = Vector2.Distance(_StartPos, _targetPos);
		if (_Data._视野范围有敌人)
		{
			_Timer = Random.Range(1f, 1.5f);
		}
		else
		{
			_Timer = Random.Range(0.75f, 1f);
		}
		if (_Distance < 2f)
		{
			_Speed = _Speed1;
		}
		else
		{
			_Speed = _Speed2;
		}
		_累计判定次数++;
		_累计判定距离 += Mathf.Clamp(_Distance, 0f, 5f);
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer > 0f)
		{
			Vector2 b = _Speed * (_targetPos - base._Rb2D.position).normalized;
			base._Rb2D.velocity = Vector2.Lerp(base._Rb2D.velocity, b, Time.deltaTime * 10.9f);
			return;
		}
		EnemyStateName_Type state = EnemyStateName_Type.追击;
		if (Random.value < 0.8f && (_累计判定次数 >= 4 || (_累计判定距离 >= 20f && _Distance > 3f) || (_累计判定距离 >= 5f && _Distance < 3f)))
		{
			_累计判定次数 = 0;
			_累计判定距离 = 0f;
			state = EnemyStateName_Type.攻击;
		}
		base._Mono.ChangeState(state);
	}
}
