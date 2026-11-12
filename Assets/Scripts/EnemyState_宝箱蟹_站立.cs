using DSB;
using UnityEngine;

public class EnemyState_宝箱蟹_站立 : EnemyState_宝箱蟹
{
	public override void OnEnter()
	{
		_Data._UI_HP._可见性 = false;
		_Data._Mono组件._Rb2D.bodyType = RigidbodyType2D.Kinematic;
		_Data._Mono组件._Rb2D.velocity = Vector2.zero;
		base._Ani.Play("站立");
		base._Mono组件.gameObject.layer = 0;
	}

	public override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3)
		{
			base._Mono._ItemUI_交互键._可见性 = true;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 += F_开箱;
		}
	}

	public override void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3)
		{
			base._Mono._ItemUI_交互键._可见性 = false;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 -= F_开箱;
		}
	}

	public override void OnExit()
	{
		base._Mono组件.gameObject.layer = 6;
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 -= F_开箱;
		_Data._Mono组件._Rb2D.bodyType = RigidbodyType2D.Dynamic;
		base._Mono._ItemUI_交互键._可见性 = false;
	}

	public void F_开箱()
	{
		base._Mono.F_朝向玩家();
		ChangeState(EnemyStateName_Type.攻击);
	}
}
