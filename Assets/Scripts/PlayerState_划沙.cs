using DG.Tweening;
using DSB;
using UnityEngine;

public class PlayerState_划沙 : Player_StateBase
{
	private TrailRenderer _TR_拖尾;

	private TrailRenderer _TR_拖尾2;

	private int _ID_拖尾2;

	private float _按住跳跃延时旋转 = 0.15f;

	private float _按住跳跃延时旋转Timer;

	private bool _头着地回正中;

	private float _头着地回正中Timer;

	private Vector3 _头着地回正中StartAng;

	private bool _完成后空翻;

	private float _旋转角度;

	private bool _开启按住跳;

	public override void OnEnter()
	{
		_TR_拖尾 = base._Mono组件._GO_拖尾.GetComponent<TrailRenderer>();
		_TR_拖尾2 = base._Mono组件._GO_拖尾2.GetComponent<TrailRenderer>();
		base._Mono组件._Rb2D.drag = 0.018f;
		base._Mono组件._Rb2D.AddForce(Vector2.right * 50f, ForceMode2D.Impulse);
		base._Mono组件._Rb2D.gravityScale = 1.25f;
		_开启按住跳 = false;
		base._Mono组件._GO_拖尾.SetActive(value: true);
		base._Mono组件._GO_拖尾2.SetActive(value: true);
		base._Ani.Play("滑行_金箍棒", 0, 0f);
		_TR_拖尾.emitting = true;
		_TR_拖尾2.emitting = false;
		_ID_拖尾2 = DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(base._Mono组件._GO_拖尾2, 0.1f, delegate
		{
			_TR_拖尾2.emitting = false;
		});
		_按住跳跃延时旋转Timer = _按住跳跃延时旋转;
		_头着地回正中 = false;
		_完成后空翻 = false;
		_旋转角度 = 0f;
		_ = _完成后空翻;
	}

	public override void OnExit()
	{
		base._Mono组件._Rb2D.drag = 5f;
		base._Mono组件._Rb2D.gravityScale = 0f;
		base._Mono组件._GO_拖尾.SetActive(value: false);
		base._Mono组件._SR_Model.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		base._Mono组件._SR_影子.transform.localPosition = new Vector3(0.08f, 0f, 0f);
		Camera.main.DOOrthoSize(Lunch._镜头默认大小, 0.75f);
	}

	public override void OnUpdate()
	{
		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, Mathf.Clamp(base._Mono组件._Rb2D.velocity.magnitude / 3f * 2f, Lunch._镜头默认大小, 25f), Time.deltaTime * 5f);
		Vector2 rhs = Vector2.up;
		float num = 1f;
		RaycastHit2D raycastHit2D = Physics2D.Raycast(base._Mono组件.transform.position, Vector2.down, float.PositiveInfinity, 1);
		if ((bool)raycastHit2D)
		{
			base._Mono组件._SR_影子.transform.position = raycastHit2D.point;
			rhs = raycastHit2D.normal;
			num = Vector2.Distance(base._Mono组件.transform.position, raycastHit2D.point);
		}
		if (_头着地回正中)
		{
			_头着地回正中Timer += Time.deltaTime * 1.8f;
			base._Mono组件._SR_Model.transform.eulerAngles = Vector3.Lerp(_头着地回正中StartAng, Vector3.zero, _头着地回正中Timer);
			if (Mathf.Abs(base._Mono组件._SR_Model.transform.eulerAngles.z) <= 10f)
			{
				_头着地回正中 = false;
			}
			return;
		}
		if (_开启按住跳 && num > 0.5f)
		{
			if (_按住跳跃延时旋转Timer > 0f)
			{
				_按住跳跃延时旋转Timer -= Time.deltaTime;
			}
			else
			{
				float num2 = Time.deltaTime * 140f;
				_旋转角度 += num2;
				base._Mono组件._SR_Model.transform.Rotate(new Vector3(0f, 0f, 1f), num2);
			}
		}
		else
		{
			if (num < 0.5f)
			{
				if (Vector2.Dot(base._Mono组件._SR_Model.transform.up, rhs) < 0.3f)
				{
					_头着地回正中 = true;
					base._Mono组件._Rb2D.velocity = Vector2.zero;
					base._Mono组件._Rb2D.AddForce(new Vector2(10f, 40f), ForceMode2D.Impulse);
					_头着地回正中StartAng = base._Mono组件._SR_Model.transform.eulerAngles;
					_头着地回正中Timer = 0f;
					_Data._Curr生命 -= 30f;
					base._Mono._BeAttack_Pos = Vector2.down;
					base._Mono._BeAttack_击退力度 = 5f;
					if (_Data._Curr生命 < 0f)
					{
						base._Mono.ChangeToState(Player_StateName_Type.死亡);
						EventArgs_Player_死亡.Trigger();
					}
					return;
				}
				if (_旋转角度 > 160f)
				{
					_旋转角度 = 0f;
					base._Mono组件._Rb2D.AddForce(base._Mono.transform.right * 100f, ForceMode2D.Impulse);
					DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_跳跃加速();
					DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(base._Mono组件._GO_拖尾2, _ID_拖尾2, 1.18f, delegate
					{
						_TR_拖尾2.emitting = false;
					});
					_TR_拖尾2.emitting = true;
				}
				else
				{
					_旋转角度 = 0f;
				}
			}
			float num3 = Vector2.Dot(base._Mono组件._SR_Model.transform.right, rhs);
			float num4 = Time.deltaTime * 60f;
			if (num3 > 0.05f)
			{
				_旋转角度 -= num4;
				base._Mono组件._SR_Model.transform.Rotate(new Vector3(0f, 0f, 1f), 0f - num4);
			}
			else if (num3 < -0.05f)
			{
				_旋转角度 += num4;
				base._Mono组件._SR_Model.transform.Rotate(new Vector3(0f, 0f, 1f), num4);
			}
		}
		if (base._Mono组件._SR_Model.transform.right.x > 0f && Mathf.Abs(base._Mono组件._SR_Model.transform.right.y) < 0.8f)
		{
			_TR_拖尾.emitting = true;
		}
		else
		{
			_TR_拖尾.emitting = false;
		}
	}

	public override void F_跳跃Down()
	{
		F_Down();
	}

	public override void F_跳跃Up()
	{
		F_Up();
	}

	public override void F_轻攻击Down()
	{
		F_Down();
	}

	public override void F_轻攻击Up()
	{
		F_Up();
	}

	public override void F_重攻击Down()
	{
		F_Down();
	}

	public override void F_重攻击Up()
	{
		F_Up();
	}

	public void F_Down()
	{
		_旋转角度 = 0f;
		if ((bool)Physics2D.OverlapCircle(base._Mono组件.transform.position, 0.5f, 1))
		{
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_跳跃_翻滚();
			if (base._Mono组件._Rb2D.velocity.x < 10f)
			{
				base._Mono组件._Rb2D.AddForce(new Vector2(80f, 60f), ForceMode2D.Impulse);
			}
			else
			{
				base._Mono组件._Rb2D.AddForce(new Vector2(20f, 60f), ForceMode2D.Impulse);
			}
		}
		_按住跳跃延时旋转Timer = _按住跳跃延时旋转;
		_开启按住跳 = true;
	}

	public void F_Up()
	{
		_开启按住跳 = false;
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		_Data._Curr生命 -= attackInfo.atk_生命;
		_旋转角度 = 0f;
		base._Mono._BeAttack_Pos = attackInfo.atk_Pos;
		base._Mono._BeAttack_击退力度 = attackInfo.atk_击退力度;
		if (_Data._Curr生命 > 0f)
		{
			Vector2 normalized = ((Vector2)base._Mono组件.transform.position - attackInfo.atk_Pos).normalized;
			base._Mono组件._Rb2D.velocity = Vector2.zero;
			base._Mono组件._Rb2D.AddForce(normalized * 40f, ForceMode2D.Impulse);
		}
		else
		{
			base._Mono.ChangeToState(Player_StateName_Type.死亡);
			EventArgs_Player_死亡.Trigger();
		}
	}
}
