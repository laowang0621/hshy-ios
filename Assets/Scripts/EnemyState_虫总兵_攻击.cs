using System.Collections;
using System.Collections.Generic;
using DSB;
using UnityEngine;

public class EnemyState_虫总兵_攻击 : EnemyState_Boss
{
	private EnemyAI_虫总兵 _Mono_虫总兵;

	private DataInfo_Enemy招式数值 十字斩;

	private DataInfo_Enemy招式数值 重砸;

	private DataInfo_Enemy招式数值 旋风斩;

	private DataInfo_Enemy招式数值 气刃斩;

	private DataInfo_Enemy招式数值 左横斩;

	private DataInfo_Enemy招式数值 右横斩;

	private DataInfo_Enemy招式数值 上挑;

	private DataInfo_Enemy招式数值 单手斜斩;

	private DataInfo_Enemy招式数值 弹反;

	private DataInfo_Enemy招式数值 梨花针;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private float _EnterDrag;

	private bool _跟随移动;

	private Vector2 _MoveDirSpeed;

	private int _攻击序号 = 10;

	private List<int> _出招序号表 = new List<int>
	{
		10, 11, 12, 20, 21, 22, 30, 31, 32, 50,
		51, 52, 53, 54
	};

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		_Mono_虫总兵 = (EnemyAI_虫总兵)data._Mono;
		十字斩 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(18001);
		重砸 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(18002);
		旋风斩 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(18003);
		气刃斩 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(18004);
		左横斩 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(18005);
		右横斩 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(18006);
		上挑 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(18007);
		单手斜斩 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(18008);
		弹反 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(18009);
		梨花针 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(18010);
		return base.Init(data);
	}

	public override void OnUpdate()
	{
		if (_跟随移动)
		{
			base._Rb2D.velocity = Vector2.Lerp(base._Rb2D.velocity, _MoveDirSpeed, Time.deltaTime * 10.9f);
		}
	}

	public override void OnEnter()
	{
		_跟随移动 = false;
		base._Rb2D.velocity = Vector2.zero;
		_EnterDrag = base._Rb2D.drag;
		_Mono_虫总兵._格挡架势 = false;
		_Data._Mono组件.StopAllCoroutines();
		if (_Mono_虫总兵._触发格挡)
		{
			base._Mono组件.StartCoroutine(Cor_攻击_触发格挡());
		}
		else
		{
			base._Mono组件.StartCoroutine(Cor_攻击());
		}
	}

	public override void OnExit()
	{
		_Data._Mono组件.StopAllCoroutines();
		_跟随移动 = false;
		_Mono_虫总兵._格挡架势 = false;
		base._Rb2D.bodyType = RigidbodyType2D.Dynamic;
		base._Mono组件._Co2D_自身碰撞器.enabled = true;
		base._Rb2D.drag = _EnterDrag;
		Physics2D.IgnoreLayerCollision(6, 3, ignore: false);
		Physics2D.IgnoreLayerCollision(6, 10, ignore: false);
	}

	private IEnumerator Cor_攻击()
	{
		_攻击序号 = _出招序号表.GetRandom();
		if (_Mono_虫总兵._开启固定出招)
		{
			_攻击序号 = _Mono_虫总兵._固定出招序号;
		}
		switch (_攻击序号)
		{
		case 10:
			yield return base._Mono组件.StartCoroutine(Cor_攻击_十字斩());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_重砸());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_重砸());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_重砸());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_斜劈气刃旋风斩());
			break;
		case 11:
			yield return base._Mono组件.StartCoroutine(Cor_攻击_十字斩());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_重砸());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_重砸());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_重砸());
			break;
		case 12:
			yield return base._Mono组件.StartCoroutine(Cor_攻击_十字斩());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_重砸());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_重砸());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_斜劈气刃旋风斩());
			break;
		case 20:
			yield return base._Mono组件.StartCoroutine(Cor_攻击_左砍());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_右砍());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_上挑());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_下挑劈());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_重砸());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_重砸());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_斜劈气刃旋风斩());
			break;
		case 21:
			yield return base._Mono组件.StartCoroutine(Cor_攻击_左砍());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_右砍());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_上挑());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_下挑劈());
			if (Random.value < 0.6f)
			{
				yield return base._Mono组件.StartCoroutine(Cor_攻击_重砸());
			}
			break;
		case 22:
			yield return base._Mono组件.StartCoroutine(Cor_攻击_左砍());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_右砍());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_上挑());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_下挑劈());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_重砸());
			if (Random.value < 0.7f)
			{
				yield return base._Mono组件.StartCoroutine(Cor_攻击_重砸());
			}
			break;
		case 30:
			yield return base._Mono组件.StartCoroutine(Cor_攻击_格挡());
			if (Random.value < 0.3f)
			{
				yield return base._Mono组件.StartCoroutine(Cor_攻击_暴雨梨花针());
			}
			else
			{
				yield return base._Mono组件.StartCoroutine(Cor_攻击_斜劈气刃旋风斩());
			}
			break;
		case 31:
			yield return base._Mono组件.StartCoroutine(Cor_攻击_格挡());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_暴雨梨花针());
			break;
		case 32:
			yield return base._Mono组件.StartCoroutine(Cor_攻击_暴雨梨花针());
			break;
		case 50:
			yield return base._Mono组件.StartCoroutine(Cor_攻击_上挑());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_斜劈气刃旋风斩());
			break;
		case 51:
			yield return base._Mono组件.StartCoroutine(Cor_攻击_上挑());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_重砸_快速());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_斜劈气刃旋风斩());
			break;
		case 52:
			yield return base._Mono组件.StartCoroutine(Cor_攻击_上挑());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_下挑劈());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_斜劈气刃旋风斩());
			break;
		case 53:
			yield return base._Mono组件.StartCoroutine(Cor_攻击_上挑());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_重砸_快速());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_上挑());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_斜劈气刃旋风斩());
			break;
		case 54:
			yield return base._Mono组件.StartCoroutine(Cor_攻击_上挑());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_斜劈());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_斜劈());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_斜劈());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_斜劈气刃旋风斩());
			break;
		}
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}

	private void F_发射弓箭(Vector2 当前位置, Vector2 目标位置, bool 需要音效 = false)
	{
		if (需要音效)
		{
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_弓狼射箭();
		}
		Object.Instantiate(_Mono_虫总兵._投射物Prefab, 当前位置, Quaternion.identity).F_Init(目标位置, base._Mono组件._C_攻击判断._伤害);
	}

	private void F_更新攻击方向()
	{
		_目标位置 = _Data._Pos_追击目标位置;
		Vector2 vector = base._Mono.transform.position;
		_攻击方向 = (_目标位置 - vector).normalized;
		base._Mono.transform.localScale = new Vector3((_攻击方向.x > 0f) ? 1 : (-1), 1f, 1f);
	}

	private IEnumerator Cor_攻击_十字斩()
	{
		base._Ani.Play("攻击_十字斩", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 十字斩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 十字斩._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.416f));
	}

	private IEnumerator Cor_攻击_重砸()
	{
		base._Ani.Play("攻击_重砸", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 重砸._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 重砸._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.41f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.38f));
	}

	private IEnumerator Cor_攻击_重砸_快速()
	{
		base._Ani.Play("攻击_重砸_快速", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 重砸._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 重砸._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.29f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.356f));
	}

	private IEnumerator Cor_攻击_重砸_急速()
	{
		base._Ani.Play("攻击_重砸_急速", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 重砸._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 重砸._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
	}

	private IEnumerator Cor_攻击_斜劈()
	{
		base._Ani.Play("攻击_斜劈", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 旋风斩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 旋风斩._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.54f));
	}

	private IEnumerator Cor_攻击_斜劈气刃旋风斩()
	{
		base._Ani.Play("攻击_斜劈气刃旋风斩", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 旋风斩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 旋风斩._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 气刃斩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 气刃斩._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.36f));
	}

	private IEnumerator Cor_攻击_斜劈气刃旋风斩_急速()
	{
		base._Ani.Play("攻击_斜劈气刃旋风斩", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 旋风斩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 旋风斩._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.29f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.3f));
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 气刃斩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 气刃斩._击退力度;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(1.07f));
	}

	private IEnumerator Cor_攻击_左砍()
	{
		base._Ani.Play("攻击_左砍", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 左横斩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 左横斩._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.16f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.37f));
	}

	private IEnumerator Cor_攻击_右砍()
	{
		base._Ani.Play("攻击_右砍", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 右横斩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 右横斩._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.16f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
	}

	private IEnumerator Cor_攻击_上挑()
	{
		base._Ani.Play("攻击_上挑", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 上挑._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 上挑._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.16f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.4f));
	}

	private IEnumerator Cor_攻击_上挑_急速()
	{
		base._Ani.Play("攻击_上挑", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 上挑._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 上挑._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.12f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.28f));
	}

	private IEnumerator Cor_攻击_下挑劈()
	{
		base._Ani.Play("攻击_下挑劈", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 单手斜斩._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 单手斜斩._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.2f));
		F_更新攻击方向();
		base._Rb2D.velocity = _攻击方向 * 26f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.34f));
	}

	private IEnumerator Cor_攻击_暴雨梨花针()
	{
		base._Ani.Play("攻击_暴雨梨花针", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 梨花针._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 梨花针._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		for (int i = 0; i < 25; i++)
		{
			Vector2 当前位置 = _Mono_虫总兵._Tsf_梨花针.position;
			当前位置 += Random.insideUnitCircle * 0.5f;
			Vector2 vector = base._Mono组件.transform.position;
			Vector2 @this = ((base._Mono.transform.localScale.x >= 0f) ? Vector2.right : Vector2.left);
			F_发射弓箭(当前位置, vector + @this.Rotate(Random.Range(-15f, 15f)) * Random.Range(2.5f, 18f));
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(Random.Range(0.03f, 0.05f)));
		}
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.25f));
	}

	private IEnumerator Cor_攻击_格挡()
	{
		_Mono_虫总兵._格挡次数 = 0;
		_Mono_虫总兵._格挡架势 = true;
		base._Ani.Play("攻击_格挡", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 重砸._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 重砸._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(Random.Range(1.5f, 2.5f)));
	}

	private IEnumerator Cor_攻击_触发格挡()
	{
		_Mono_虫总兵._触发格挡 = false;
		_Mono_虫总兵._格挡次数++;
		base._Ani.Play("攻击_触发格挡", 0, 0f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 重砸._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 重砸._击退力度;
		base._Rb2D.velocity = Vector2.zero;
		F_更新攻击方向();
		if (_Mono_虫总兵._格挡次数 >= 3)
		{
			_Mono_虫总兵._格挡架势 = false;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
			yield return base._Mono组件.StartCoroutine(Cor_攻击_重砸_急速());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_上挑_急速());
			yield return base._Mono组件.StartCoroutine(Cor_攻击_斜劈气刃旋风斩_急速());
		}
		else
		{
			_Mono_虫总兵._格挡架势 = true;
			yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(Random.Range(1.5f, 2.5f)));
			if (Random.value < 0.66f)
			{
				yield return base._Mono组件.StartCoroutine(Cor_攻击_暴雨梨花针());
			}
			else
			{
				yield return base._Mono组件.StartCoroutine(Cor_攻击_斜劈气刃旋风斩());
			}
		}
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}
}
