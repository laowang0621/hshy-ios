using UnityEngine;

public class EnemyState_Boss_追击 : EnemyState_Boss
{
	protected Vector2 _MoveDirSpeed;

	protected float _MoveSpeed = 8f;

	protected int _累计追击判定次数;

	protected int _判断X次后出手;

	protected float 期望 = 5f;

	protected float _间隔 = 0.2f;

	protected int _1 = 5;

	protected int _2 = 10;

	public int _AniID_站立 = Animator.StringToHash("站立");

	public int _AniID_移动 = Animator.StringToHash("移动");

	public EnemyState_Boss_追击()
	{
	}

	public EnemyState_Boss_追击(float moveSpeed)
	{
		_MoveSpeed = moveSpeed;
	}

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		base.Init(data);
		F_计算攻击欲望();
		return this;
	}

	public void F_计算攻击欲望()
	{
		_1 = (int)(_Data._追击时间_x / _间隔) + 1;
		_2 = (int)(_Data._追击时间_y / _间隔) + 1;
		_累计追击判定次数 = 0;
		_判断X次后出手 = Random.Range(_1, _2);
	}

	public override void OnEnter()
	{
		_累计追击判定次数++;
		Vector2 vector = base._Mono.transform.position;
		Vector2 vector2 = _Data._Pos_追击目标位置;
		_Timer = _间隔;
		base._Mono.transform.localScale = new Vector3((vector2.x > vector.x) ? 1 : (-1), 1f, 1f);
		if (Vector2.Distance(vector, vector2) < 6f)
		{
			base._Ani.Play(_AniID_站立);
			_MoveDirSpeed = Vector2.zero;
			return;
		}
		base._Ani.Play(_AniID_移动);
		Vector2 vector3 = vector2 + new Vector2((float)((vector2.x < vector.x) ? 1 : (-1)) * Random.Range(3f, 5f), Random.Range(-0.5f, 0.5f));
		_MoveDirSpeed = (vector3 - vector).normalized * _MoveSpeed;
		base._Mono组件._Tsf_移动目标.transform.position = vector3;
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer > 0f)
		{
			base._Rb2D.velocity = Vector2.Lerp(base._Rb2D.velocity, _MoveDirSpeed, Time.deltaTime * 10.9f);
		}
		else if (F_判断出手())
		{
			base._Mono.ChangeState(EnemyStateName_Type.攻击);
		}
		else
		{
			base._Mono.ChangeState(EnemyStateName_Type.追击);
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
