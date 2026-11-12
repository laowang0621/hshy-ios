using System;
using System.Collections;
using System.Collections.Generic;
using DSB;
using UnityEngine;

public class PlayerState_下地狱100层 : Player_StateBase
{
	private float _跳跃高度 = 4f;

	private Vector2 _垂直速度范围 = new Vector2(-16.5f, 80f);

	private float _重力 = 11.5f;

	public bool _退出恢复刚体设置;

	private ContactFilter2D _CF2D = new ContactFilter2D
	{
		useTriggers = false,
		useLayerMask = true,
		layerMask = ~LayerMask.GetMask("Player")
	};

	private List<Collider2D> results = new List<Collider2D>();

	private bool _跳跃中;

	private int _Curr跳跃次数 = 2;

	private int _最大跳跃次数 = 2;

	protected Coroutine _Cor_受击变色;

	public override void OnEnter()
	{
		_Data._Mono组件._Co_自身碰撞器.enabled = true;
		_Data._下地狱100层_可以跳跃 = true;
		_Timer = 0f;
		base._Mono._m开启反弹弹道 = false;
		base._Rb2D.drag = 0.114514f;
		base._Rb2D.gravityScale = _重力;
		base._Mono组件._SR_影子.SetAlpha(0f);
		base._Mono组件._Co_自身碰撞器.direction = CapsuleDirection2D.Vertical;
		base._Mono组件._Co_自身碰撞器.offset = base._Mono组件._碰撞器_下地狱偏移;
		base._Mono组件._Co_自身碰撞器.size = base._Mono组件._碰撞器_下地狱大小;
		_退出恢复刚体设置 = true;
		_Curr跳跃次数 = _最大跳跃次数;
		_跳跃中 = true;
	}

	public override void OnExit()
	{
		if (_退出恢复刚体设置)
		{
			base._Rb2D.drag = 5f;
			base._Rb2D.gravityScale = 0f;
		}
		else
		{
			base._Rb2D.gravityScale = 5f;
		}
	}

	public override void OnUpdate()
	{
		bool flag = F_下地狱Move(12f);
		if (_跳跃中)
		{
			base._Ani.Play("翻滚");
		}
		else if (flag)
		{
			base._Ani.Play("跑步");
		}
		else
		{
			base._Ani.Play("站立");
		}
		if (_跳跃中 && base._Rb2D.velocity.y < 0.1f && Physics2D.OverlapCircle(base._Mono组件.transform.position, 0.25f, _CF2D, results) > 0)
		{
			_Curr跳跃次数 = _最大跳跃次数;
			_跳跃中 = false;
		}
	}

	private void DrawDebugCircle(Vector2 center, float radius, Color color)
	{
		float num = 10f;
		Vector2 vector = center + new Vector2(radius, 0f);
		for (int i = 1; i <= 36; i++)
		{
			float num2 = num * (float)i;
			Vector2 vector2 = center + new Vector2(Mathf.Cos(MathF.PI / 180f * num2) * radius, Mathf.Sin(MathF.PI / 180f * num2) * radius);
			Debug.DrawLine(vector, vector2, color, Time.deltaTime);
			vector = vector2;
		}
	}

	public bool F_下地狱Move(float speed, bool can转向 = true)
	{
		float num = Mathf.Clamp(MyInputManager.Horizontal, -1f, 1f);
		Vector2 velocity = new Vector2(speed * num, base._Rb2D.velocity.y);
		velocity.y = Mathf.Clamp(velocity.y, _垂直速度范围.x, _垂直速度范围.y);
		base._Rb2D.velocity = velocity;
		if (can转向)
		{
			F_玩家转向(num);
		}
		if (num == 0f)
		{
			return false;
		}
		return true;
	}

	public override void F_跳跃Down()
	{
		if (_Curr跳跃次数 <= 0 && base._Rb2D.velocity.y < 0.1f && Physics2D.OverlapCircle(base._Mono组件.transform.position, 0.25f, _CF2D, results) > 0)
		{
			_Curr跳跃次数 = _最大跳跃次数;
			_跳跃中 = false;
		}
		if (_Curr跳跃次数 > 0)
		{
			_Curr跳跃次数--;
			if (_Data._下地狱100层_可以跳跃)
			{
				F_跳跃(_跳跃高度);
				DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_玩家_跳跃_翻滚();
				_跳跃中 = true;
			}
		}
	}

	public override void F_特殊操作_1()
	{
		_跳跃中 = true;
		MyLog.LogError("特殊操作_1: 下地狱100层翻滚");
	}

	private void F_跳跃(float 高度)
	{
		Vector2 velocity = base._Rb2D.velocity;
		velocity.y = Mathf.Sqrt(4f * 高度 * base._Rb2D.gravityScale * base._Rb2D.mass);
		base._Rb2D.velocity = velocity;
	}

	public override void F_外部施加跳跃高度(float 高度)
	{
		F_跳跃(高度);
	}

	public override void F_重攻击Down()
	{
		_Timer = 0f;
	}

	public override void F_重攻击Up()
	{
	}

	public override void F_轻攻击Down()
	{
		F_重攻击Down();
	}

	public override void F_轻攻击Up()
	{
		F_重攻击Up();
	}

	public override void BeAttack(AttackInfo attackInfo)
	{
		base._Mono组件._Rb2D.drag = 5f;
		base._Mono._BeAttack_Pos = attackInfo.atk_Pos;
		base._Mono._BeAttack_击退力度 = attackInfo.atk_击退力度;
		_Data._Curr生命 -= attackInfo.atk_生命;
		if (_Data._Curr生命 <= 0f)
		{
			base._Mono.ChangeToState(Player_StateName_Type.死亡);
			return;
		}
		if (_Cor_受击变色 != null)
		{
			base._Mono组件.StopCoroutine(_Cor_受击变色);
		}
		_Cor_受击变色 = base._Mono组件.StartCoroutine(Cor_受击变色());
	}

	protected IEnumerator Cor_受击变色()
	{
		_Data._Mono组件._SR_Model.color = new Color(1f, 0.5f, 0.5f);
		yield return new WaitForSeconds(0.15f);
		_Data._Mono组件._SR_Model.color = new Color(1f, 1f, 1f);
		_Cor_受击变色 = null;
	}
}
