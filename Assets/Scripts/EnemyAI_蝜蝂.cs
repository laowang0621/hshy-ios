using DG.Tweening;
using DSB;
using UnityEngine;

public class EnemyAI_蝜蝂 : EnemyAI_Boss
{
	public Vector2 _MoveDir;

	public float _MoveSpeed = 4f;

	public float _MoveSpeed2 = 8f;

	public bool _在移动;

	public GameObject _震荡波Prefab;

	public Transform _Tsf_我来助你;

	public Animator _Ani_我来助你;

	public bool _易伤;

	public Transform _Tsf_Model2;

	private float _定身前Drag;

	private Tween _Tween_震动;

	public override Enum_对话 _对话类型_见面 => Enum_对话.斯哈里国蝜蝂前置对话;

	public override Enum_对话 _对话类型_死亡 => Enum_对话.战胜蝜蝂;

	public override void F_添加状态()
	{
		_FSM.AddState(EnemyStateName_Type.站立, new EnemyState_蝜蝂_站立().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.追击, new EnemyState_蝜蝂_追击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.受击, new EnemyState_蝜蝂_受击().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.死亡, new EnemyState_蝜蝂_死亡().Init(_Data));
		_FSM.AddState(EnemyStateName_Type.攻击, new EnemyState_蝜蝂_攻击().Init(_Data));
		_Tsf_我来助你.parent = null;
		_在移动 = false;
		_易伤 = false;
	}

	public void F_Ani_生成震荡波()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_地震();
		C_攻击判断 componentInChildren = Object.Instantiate(_震荡波Prefab, base.transform.position, Quaternion.identity).GetComponentInChildren<C_攻击判断>();
		if (componentInChildren != null)
		{
			componentInChildren._伤害 = _Data._Atk;
		}
	}

	protected override void OnDestroy()
	{
		if (_Tsf_我来助你 != null)
		{
			Object.Destroy(_Tsf_我来助你.gameObject);
		}
		base.OnDestroy();
	}

	public override void F_定身(float 时间)
	{
		base.F_定身(时间);
		_定身前Drag = _Data._Mono组件._Rb2D.drag;
		_Data._Mono组件._Rb2D.drag = 5f;
	}

	public override void F_定身结束()
	{
		base.F_定身结束();
		_Data._Mono组件._Rb2D.drag = _定身前Drag;
	}

	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		if (_在移动 && !base._Is被定)
		{
			Vector2 normal = collision.contacts[0].normal;
			_MoveDir = Vector2.Reflect(_MoveDir, normal);
			_Data._Mono组件._Rb2D.velocity = _MoveDir * _MoveSpeed;
			_Data._Mono.transform.localScale = new Vector3((_MoveDir.x < 0f) ? 1 : (-1), 1f, 1f);
		}
	}

	protected override void OnCollisionStay2D(Collision2D collision)
	{
	}

	private void F_震动()
	{
		if (_Tween_震动 != null)
		{
			_Tween_震动.Kill();
		}
		MyLog.LogError("震动");
		_Tsf_Model2.localPosition = Vector2.zero;
		_Tween_震动 = _Tsf_Model2.DOShakePosition(0.5f, 0.2f);
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		if (!_Data._Is死亡 && !_Data._Is虚幻)
		{
			F_震动();
			if (base._Is被定)
			{
				attackInfo.atk_生命 += DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1402);
				attackInfo.atk_生命 *= DSB_MonoSingleton<MyDataManager>.Instance.F_Get_PlayerLevel_BuffVal(1403);
			}
			if (_易伤)
			{
				attackInfo.atk_生命 *= 30f;
			}
			else
			{
				attackInfo.atk_生命 *= 10f;
			}
			if (_Cor_受击变色 != null)
			{
				StopCoroutine(_Cor_受击变色);
			}
			_Cor_受击变色 = StartCoroutine(Cor_受击变色());
			_Data._Curr生命 -= attackInfo.atk_生命;
			_Data.F_设置UI血量(_Data._Curr生命, _Data._MaxHP);
			if (_Data._Curr生命 <= 0f)
			{
				DSB_MonoSingleton<GameManager>.Instance.F_获得经验(_Data._击杀经验值);
				ChangeState(EnemyStateName_Type.死亡);
			}
		}
	}
}
