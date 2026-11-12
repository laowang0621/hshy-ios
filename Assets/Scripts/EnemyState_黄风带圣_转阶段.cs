using System.Collections;
using DG.Tweening;
using DSB;
using UnityEngine;

public class EnemyState_黄风带圣_转阶段 : EnemyState_Boss
{
	private EnemyAI_黄风带圣 _Mono_黄风带圣;

	private DataInfo_Enemy招式数值 大圣扎猴;

	private Transform _Tsf_Player;

	private Vector2 _目标位置;

	private Vector2 _攻击方向;

	private Coroutine _Cor;

	protected Vector2 _MoveDirSpeed;

	protected float _MoveSpeed = 8f;

	private bool _已经触发攻击;

	private int _脚印Count;

	public override EnemyState_Boss Init(Enemy_StateData_Boss data)
	{
		_Mono_黄风带圣 = (EnemyAI_黄风带圣)data._Mono;
		大圣扎猴 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Enemy.F_招式数值(6012);
		return base.Init(data);
	}

	public override void OnEnter()
	{
		base._Ani.Play("移动");
		_Mono_黄风带圣._转阶段 = true;
		base._Mono组件._SR_Model.DOFade(0f, 0.88f);
		base._Mono组件._SR_影子.DOFade(0f, 0.88f);
		_Tsf_Player = DSB_MonoSingleton<GameManager>.Instance._PC.transform;
		_已经触发攻击 = false;
		if (_Cor != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor);
		}
		_Cor = _Data._Mono组件.StartCoroutine(Cor_跟踪());
		_Timer = 3f;
	}

	public override void OnUpdate()
	{
		if (_已经触发攻击)
		{
			return;
		}
		base._Rb2D.velocity = Vector2.Lerp(base._Rb2D.velocity, _MoveDirSpeed, Time.deltaTime * 10.9f);
		if (_Timer > 0f)
		{
			_Timer -= Time.deltaTime;
		}
		else if ((_Mono_黄风带圣.transform.position - _Tsf_Player.position).sqrMagnitude < 8f)
		{
			_已经触发攻击 = true;
			if (_Cor != null)
			{
				_Data._Mono组件.StopCoroutine(_Cor);
			}
			_Cor = _Data._Mono组件.StartCoroutine(Cor_大圣扎猴());
		}
	}

	public override void OnExit()
	{
		if (_Cor != null)
		{
			_Data._Mono组件.StopCoroutine(_Cor);
			_Cor = null;
		}
		_Mono_黄风带圣._转阶段 = false;
	}

	private void F_生成脚印()
	{
		Object.Instantiate(_Mono_黄风带圣._GO_脚印Prefab, _Mono_黄风带圣.transform.position, Quaternion.identity).transform.localScale = new Vector3((_Mono_黄风带圣.transform.localScale.x > 0f) ? 1 : (-1), ((_脚印Count++ & 1) == 1) ? 1 : (-1), 1f);
	}

	private void F_更新攻击方向()
	{
		_目标位置 = _Tsf_Player.position;
		Vector2 vector = base._Mono.transform.position;
		_攻击方向 = (_目标位置 - vector).normalized;
		base._Mono.transform.localScale = new Vector3((_攻击方向.x > 0f) ? 1 : (-1), 1f, 1f);
	}

	private IEnumerator Cor_跟踪()
	{
		float num = Random.Range(8, 12);
		float num2 = 0.3f;
		int count = (int)(num / num2) + 1;
		WaitForSeconds wfs = new WaitForSeconds(num2);
		for (int i = 0; i < count; i++)
		{
			F_更新攻击方向();
			F_生成脚印();
			_MoveDirSpeed = _攻击方向 * _MoveSpeed;
			yield return wfs;
		}
		base._Mono组件._SR_Model.DOFade(1f, 0.88f);
		base._Mono组件._SR_影子.DOFade(1f, 0.88f);
		base._Mono.ChangeState(EnemyStateName_Type.追击);
	}

	private IEnumerator Cor_大圣扎猴()
	{
		base._Ani.Play("大圣扎猴", 0, 0f);
		base._Mono组件._SR_Model.DOFade(1f, 0.33f);
		base._Mono组件._SR_影子.DOFade(1f, 0.33f);
		base._Mono组件._C_攻击判断._伤害 = base._Mono._Data._Atk * 大圣扎猴._伤害系数;
		base._Mono组件._C_攻击判断._击退力度 = 大圣扎猴._击退力度;
		F_更新攻击方向();
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.15f));
		base._Rb2D.velocity = _攻击方向 * 35f;
		yield return _Data._Mono组件.StartCoroutine(Cor_用于子类延迟等待(0.5f));
		base._Mono.ChangeState(EnemyStateName_Type.站立);
	}
}
