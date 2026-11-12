using System;
using System.Collections;
using DG.Tweening;
using DSB;
using UnityEngine;

public class Enemy_金池_僵尸 : MonoBehaviour, IBeAttack, I定身术
{
	private Action _死亡回调;

	public GameObject _GO_定身术特效;

	[HideInInspector]
	public Rigidbody2D _Rb2D;

	[HideInInspector]
	public Animator _Ani;

	[HideInInspector]
	public Collider2D _Co2D_自身碰撞器;

	[HideInInspector]
	public bool _Is死亡;

	[HideInInspector]
	public SpriteRenderer _SR_Model;

	[HideInInspector]
	public SpriteRenderer _SR_影子;

	private Transform _Tsf_Target;

	private EnemyAI_幽魂_金池 _金池长老;

	public float _Speed = 3f;

	public bool _Is被定 { get; set; }

	public int _阵营
	{
		get
		{
			return 2;
		}
		set
		{
			MyLog.LogError("114514?????");
		}
	}

	public Vector2 _BeAttack_Pos { get; set; }

	public float _BeAttack_击退力度 { get; set; }

	public int F_得到阵营()
	{
		return _阵营;
	}

	private void Start()
	{
		_Co2D_自身碰撞器 = GetComponent<Collider2D>();
		_Rb2D = GetComponent<Rigidbody2D>();
		_Ani = GetComponent<Animator>();
		_SR_Model = base.transform.Find("Model").GetComponent<SpriteRenderer>();
		_SR_影子 = base.transform.Find("影子").GetComponent<SpriteRenderer>();
		DSB_MonoSingleton<DSB_EventManager>.Instance.Subscribe(EventArgs_切换地图_开始._EventID, F_On_Event_切换地图);
	}

	public void F_On_Event_切换地图(DSB_EventArgs args)
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void OnDestroy()
	{
		DSB_MonoSingleton<DSB_EventManager>.Instance.UnSubscribe(EventArgs_切换地图_开始._EventID, F_On_Event_切换地图);
	}

	private void FixedUpdate()
	{
		if (!(_Tsf_Target == null) && !_Is死亡 && !_Is被定)
		{
			Vector2 vector = (_Tsf_Target.position - base.transform.position).normalized;
			_Rb2D.AddForce(vector * _Speed);
			base.transform.localScale = new Vector3((_Tsf_Target.position.x > base.transform.position.x) ? 1 : (-1), 1f, 1f);
			if (Vector2.Distance(_Tsf_Target.position, base.transform.position) < 1.5f)
			{
				_Is死亡 = true;
				_死亡回调?.Invoke();
				_SR_Model.DOFade(0f, 2f);
				_SR_影子.DOFade(0f, 2f);
				_金池长老?.F_接触僵尸_回复生命();
				Invoke("F_销毁", 3f);
			}
		}
	}

	private void F_销毁()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void BeAttack(AttackInfo attackInfo)
	{
		if (!_Is死亡)
		{
			_Is死亡 = true;
			_死亡回调?.Invoke();
			StartCoroutine(Cor_死亡());
		}
	}

	private IEnumerator Cor_死亡()
	{
		_Co2D_自身碰撞器.enabled = false;
		_Ani.Play("受击");
		_Rb2D.velocity = 15f * new Vector2(base.transform.position.x - _BeAttack_Pos.x, 0f).normalized;
		base.transform.localScale = new Vector3((_BeAttack_Pos.x > base.transform.position.x) ? 1 : (-1), 1f, 1f);
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_小妖死亡();
		yield return new WaitForSeconds(0.5f);
		_Rb2D.velocity = Vector2.zero;
		_Ani.Play("死亡");
		yield return new WaitForSeconds(2.5f);
		_SR_Model.DOFade(0f, 2f);
		_SR_影子.DOFade(0f, 2f);
		yield return new WaitForSeconds(2.5f);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void F_SetTaget(Transform tsf, Action 死亡回调)
	{
		_Tsf_Target = tsf;
		_金池长老 = tsf.GetComponent<EnemyAI_幽魂_金池>();
		_死亡回调 = 死亡回调;
	}

	public void F_定身(float 时间)
	{
		_Is被定 = true;
		_Rb2D.velocity = Vector2.zero;
		_Ani.speed = 0f;
		_GO_定身术特效.SetActive(value: true);
		_GO_定身术特效.transform.localScale = base.transform.localScale;
		Invoke("F_定身结束", 时间);
	}

	public void F_定身结束()
	{
		_Ani.speed = 1f;
		_Is被定 = false;
		_GO_定身术特效.SetActive(value: false);
	}
}
