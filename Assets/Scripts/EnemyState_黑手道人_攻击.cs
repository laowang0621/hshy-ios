using System.Collections;
using System.Collections.Generic;
using DSB;
using UnityEngine;

public class EnemyState_黑手道人_攻击 : EnemyState_Boss
{
	private EnemyAI_黑手道人 _Mono_黑手道人;

	private DataInfo_Enemy招式数值 冲刺;

	private DataInfo_Enemy招式数值 冲刺斩;

	private DataInfo_Enemy招式数值 二连;

	private DataInfo_Enemy招式数值 空中飞刀;

	private DataInfo_Enemy招式数值 挥刀斩;

	private DataInfo_Enemy招式数值 旋风斩;

	private DataInfo_Enemy招式数值 八字斩;

	private DataInfo_Enemy招式数值 挥刃斩;

	private DataInfo_Enemy招式数值 跳跃斩;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private float _EnterDrag;

	private int _攻击序号 = 10;

	private List<int> _出招序号表 = new List<int> { 1, 2, 3, 4, 5 };

	private int _x次攻击内不会重复出升空;

	private Vector2 第一把飞刀位置;

	private GameObject go_飞刀1;

	private GameObject go_飞刀2;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		_Mono_黑手道人 = (EnemyAI_黑手道人)data._Mono;
		冲刺 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(15001);
		冲刺斩 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(15002);
		二连 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(15003);
		空中飞刀 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(15004);
		挥刀斩 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(15005);
		旋风斩 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(15006);
		八字斩 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(15007);
		挥刃斩 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(15008);
		跳跃斩 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(15009);
		_x次攻击内不会重复出升空 = 3;
		return base.Init(data);
	}

	public override void OnEnter()
	{
		base._Rb2D.velocity = Vector2.zero;
		_EnterDrag = base._Rb2D.drag;
		_Data._Mono组件.StopAllCoroutines();
		base._Mono组件.StartCoroutine(Cor_攻击());
	}

	public override void OnExit()
	{
		_Data._Mono组件.StopAllCoroutines();
		_Mono_黑手道人._冲刺中 = false;
		base._Rb2D.drag = _EnterDrag;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		Physics2D.IgnoreLayerCollision(6, 10, ignore: false);
	}

	public override void OnUpdate()
	{
	}

	private IEnumerator Cor_攻击()
	{
		_攻击序号 = _出招序号表.GetRandom();
		if (_Mono_黑手道人._开启固定出招)
		{
			_攻击序号 = _Mono_黑手道人._固定出招序号;
		}
		switch (_攻击序号)
		{
		case 1:
			yield return base._Mono组件.StartCoroutine(Cor_冲刺(5f));
			if (Random.value < 0.6f)
			{
				yield return base._Mono组件.StartCoroutine(Cor_冲刺(5f));
			}
			else
			{
				yield return base._Mono组件.StartCoroutine(Cor_冲刺斩(5f));
			}
			yield return base._Mono组件.StartCoroutine(Cor_冲刺(3f));
			yield return base._Mono组件.StartCoroutine(Cor_二连());
			break;
		case 2:
			yield return base._Mono组件.StartCoroutine(Cor_空中飞刀接冲刺挥刀斩_1());
			yield return base._Mono组件.StartCoroutine(Cor_空中飞刀接冲刺挥刀斩_2());
			yield return base._Mono组件.StartCoroutine(Cor_空中飞刀接冲刺挥刀斩_3());
			yield return base._Mono组件.StartCoroutine(Cor_冲刺斩(3f));
			yield return base._Mono组件.StartCoroutine(Cor_二连());
			break;
		case 3:
			yield return base._Mono组件.StartCoroutine(Cor_旋风斩_起手());
			yield return base._Mono组件.StartCoroutine(Cor_旋风斩_中间());
			yield return base._Mono组件.StartCoroutine(Cor_旋风斩_中间());
			yield return base._Mono组件.StartCoroutine(Cor_旋风斩_中间());
			if (Random.value < 0.5f)
			{
				yield return base._Mono组件.StartCoroutine(Cor_旋风斩_中间());
			}
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
			break;
		case 4:
			yield return base._Mono组件.StartCoroutine(Cor_旋风斩_起手());
			yield return base._Mono组件.StartCoroutine(Cor_旋风二连斩接八字斩());
			break;
		case 5:
			base._Ani.Play("攻击_挥刃跳跃斩", 0, 0f);
			base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 挥刃斩._伤害系数;
			base._Mono组件._C_攻击判断._击退力度 = 挥刃斩._击退力度;
			base._Rb2D.velocity = Vector2.zero;
			F_更新攻击方向();
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
			F_更新攻击方向();
			base._Rb2D.velocity = _攻击方向 * 45f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.42f));
			F_更新攻击方向();
			base._Rb2D.velocity = _攻击方向 * 21f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
			F_更新攻击方向();
			base._Rb2D.velocity = _攻击方向 * 38f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
			F_更新攻击方向();
			base._Rb2D.velocity = _攻击方向 * 26f;
			base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 跳跃斩._伤害系数;
			base._Mono组件._C_攻击判断._击退力度 = 跳跃斩._击退力度;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.58f));
			F_更新攻击方向();
			base._Rb2D.velocity = _攻击方向 * 45f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.72f));
			F_更新攻击方向();
			base._Rb2D.velocity = _攻击方向 * 26f;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.45f));
			break;
		}
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private void F_更新攻击方向()
	{
		_目标位置 = _Data._Pos_追击目标位置;
		Vector2 vector = base._Mono.transform.position;
		_攻击方向 = (_目标位置 - vector).normalized;
		base._Mono.transform.localScale = new Vector3((_攻击方向.x > 0f) ? 1 : (-1), 1f, 1f);
	}

	private IEnumerator Cor_冲刺(float 冲刺额外距离)
	{
		base._Ani.Play("攻击_冲刺", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 冲刺._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 冲刺._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_更新攻击方向();
		Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
		Physics2D.IgnoreLayerCollision(6, 10, ignore: true);
		base._Rb2D.drag = 0f;
		float num = Vector2.Distance(base._Mono组件.transform.position, _目标位置 + _攻击方向 * 冲刺额外距离);
		float num2 = 50f;
		base._Rb2D.velocity = _攻击方向 * num2;
		float time = num / num2;
		base._Rb2D.velocity = _攻击方向 * num2;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(time));
		base._Rb2D.drag = _EnterDrag;
		base._Rb2D.velocity = _攻击方向 * 12f;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		Physics2D.IgnoreLayerCollision(6, 10, ignore: false);
		base._Ani.Play("攻击_冲刺_结束站立", 0, 0f);
		float time2 = Random.Range(0.2f, 0.4f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(time2));
	}

	private IEnumerator Cor_冲刺斩(float 冲刺额外距离)
	{
		base._Ani.Play("攻击_冲刺斩", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 冲刺斩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 冲刺斩._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_更新攻击方向();
		Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
		Physics2D.IgnoreLayerCollision(6, 10, ignore: true);
		base._Rb2D.drag = 0f;
		float num = Vector2.Distance(base._Mono组件.transform.position, _目标位置 + _攻击方向 * 冲刺额外距离);
		float num2 = 50f;
		base._Rb2D.velocity = _攻击方向 * num2;
		float num3 = num / num2;
		if (num3 <= 0.42f)
		{
			num3 = 0.42f;
		}
		base._Rb2D.velocity = _攻击方向 * num2;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(num3));
		base._Rb2D.drag = _EnterDrag;
		base._Rb2D.velocity = _攻击方向 * 12f;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		Physics2D.IgnoreLayerCollision(6, 10, ignore: false);
		base._Ani.Play("攻击_冲刺斩_结束站立", 0, 0f);
		float time = Random.Range(0.2f, 0.4f);
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(time));
	}

	private IEnumerator Cor_二连()
	{
		base._Ani.Play("攻击_二连", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 二连._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 二连._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.375f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.55f));
	}

	private IEnumerator Cor_旋风斩_起手()
	{
		base._Ani.Play("攻击_旋风斩_起手", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 旋风斩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 旋风斩._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
	}

	private IEnumerator Cor_旋风斩_中间()
	{
		base._Ani.Play("攻击_旋风斩_中间", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 旋风斩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 旋风斩._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 18f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 18f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.17f));
	}

	private IEnumerator Cor_旋风二连斩接八字斩()
	{
		base._Ani.Play("攻击_旋风二连斩接八字斩", 0, 0.133f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 八字斩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 八字斩._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 18f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 18f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.66f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.66f));
	}

	private IEnumerator Cor_空中飞刀接冲刺挥刀斩_1()
	{
		base._Ani.Play("攻击_空中飞刀", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 空中飞刀._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 空中飞刀._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
		_Mono_黑手道人._空中 = true;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.42f));
	}

	private IEnumerator Cor_空中飞刀接冲刺挥刀斩_2()
	{
		第一把飞刀位置 = _Data._Pos_追击目标位置;
		Vector2 自身向玩家的方向 = (_Data._Pos_追击目标位置 - base._Mono.transform.position).normalized;
		go_飞刀1 = null;
		go_飞刀2 = null;
		F_更新攻击方向();
		Vector2 vector = (第一把飞刀位置 = _Data._Pos_追击目标位置);
		if (vector.y > base._Mono.transform.position.y)
		{
			vector.y = base._Mono.transform.position.y;
		}
		Vector2 vector2 = _Mono_黑手道人._Tsf_飞刀.position;
		Vector2 vector3 = vector2 - vector;
		float magnitude = vector3.magnitude;
		Vector2 dir = vector3 / magnitude;
		C_飞刀 c_飞刀 = Object.Instantiate(_Mono_黑手道人._C_飞刀_Prefab, vector, Quaternion.identity);
		go_飞刀1 = c_飞刀.gameObject;
		c_飞刀.F_Init_生成(vector2, dir, magnitude);
		c_飞刀.F_设置伤害(base._Mono._Data._Atk * 空中飞刀._伤害系数, 空中飞刀._伤害系数);
		c_飞刀.F_发射();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.58f));
		Vector2 vector4 = 第一把飞刀位置 + 自身向玩家的方向 * 2f;
		if (vector4.y > base._Mono.transform.position.y)
		{
			vector4.y = base._Mono.transform.position.y;
		}
		Vector2 vector5 = _Mono_黑手道人._Tsf_飞刀.position;
		Vector2 vector6 = vector5 - vector4;
		float magnitude2 = vector6.magnitude;
		Vector2 dir2 = vector6 / magnitude2;
		C_飞刀 c_飞刀2 = Object.Instantiate(_Mono_黑手道人._C_飞刀_Prefab, vector4, Quaternion.identity);
		go_飞刀2 = c_飞刀2.gameObject;
		c_飞刀2.F_Init_生成(vector5, dir2, magnitude2);
		c_飞刀2.F_设置伤害(base._Mono._Data._Atk * 空中飞刀._伤害系数, 空中飞刀._伤害系数);
		c_飞刀2.F_发射();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
	}

	private IEnumerator Cor_空中飞刀接冲刺挥刀斩_3()
	{
		_Mono_黑手道人._空中 = false;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: true);
		Physics2D.IgnoreLayerCollision(6, 10, ignore: true);
		base._Rb2D.drag = 0f;
		float num = Vector2.Distance(base._Mono组件.transform.position, 第一把飞刀位置);
		float num2 = 50f;
		base._Rb2D.velocity = _攻击方向 * num2;
		float time = num / num2;
		base._Rb2D.velocity = _攻击方向 * num2;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(time));
		base._Rb2D.drag = _EnterDrag;
		base._Rb2D.velocity = _攻击方向 * 8f;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		Physics2D.IgnoreLayerCollision(6, 10, ignore: false);
		base._Ani.Play("攻击_空中飞刀_拿刀", 0, 0f);
		if (go_飞刀1 != null)
		{
			Object.Destroy(go_飞刀1);
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.33f));
		if (go_飞刀2 != null)
		{
			Object.Destroy(go_飞刀2);
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.17f));
	}
}
