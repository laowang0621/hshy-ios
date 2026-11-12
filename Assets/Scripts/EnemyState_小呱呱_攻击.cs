using System.Collections;
using UnityEngine;

public class EnemyState_小呱呱_攻击 : EnemyState_小呱呱
{
	private Coroutine _Cor_动作中向前力;

	private bool is朝向右边;

	public override void OnEnter()
	{
		base._Mono组件._C_攻击判断._伤害 = _Data._Atk;
		Vector2 vector = base._Mono.transform.position;
		is朝向右边 = ((Vector2)_Data._Pos_追击目标位置).x > vector.x;
		if (_Cor_动作中向前力 != null)
		{
			base._Mono.StopCoroutine(_Cor_动作中向前力);
		}
		_Cor_动作中向前力 = base._Mono.StartCoroutine(Cor_动作中向前力());
		base._Ani.Play("攻击");
		_Timer = _Data._Dict_Anis_name_time["攻击"];
	}

	public override void OnExit()
	{
		if (_Cor_动作中向前力 != null)
		{
			base._Mono.StopCoroutine(_Cor_动作中向前力);
			_Cor_动作中向前力 = null;
		}
	}

	private IEnumerator Cor_动作中向前力()
	{
		base._Rb2D.velocity = Vector2.zero;
		float time = 0f;
		while (time < 0.22f)
		{
			if (!base._Mono._Is被定)
			{
				time += Time.deltaTime;
			}
			yield return null;
		}
		base._Rb2D.velocity = new Vector2((is朝向右边 ? 1 : (-1)) * 8, 0f);
		float num = Mathf.Abs(base._Mono.transform.localScale.x);
		base._Mono.transform.localScale = new Vector3(is朝向右边 ? num : (0f - num), num, num);
	}

	public override void OnUpdate()
	{
		_Timer -= Time.deltaTime;
		if (_Timer <= 0f)
		{
			base._Mono.ChangeState(EnemyStateName_Type.站立);
		}
	}
}
