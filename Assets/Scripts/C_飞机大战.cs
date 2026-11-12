using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DSB;
using UnityEngine;

public class C_飞机大战 : MonoBehaviour, IEnemy
{
	[Serializable]
	public class C_背景移动
	{
		public Transform _Tsf_背景;

		public float _背景_移动速度 = 5f;

		public float _背景_宽度 = 20f;

		public void OnLateUpdate(float dt)
		{
			_Tsf_背景.position += _背景_移动速度 * Time.deltaTime * Vector3.left;
			if (_Tsf_背景.position.x < 0f - _背景_宽度)
			{
				_Tsf_背景.position += new Vector3(_背景_宽度, 0f, 0f);
			}
		}
	}

	public C_触发器 _入口;

	public C_触发器 _出口;

	public Transform _Tsf_Start;

	public float _飞机大战时间 = 60f;

	private float _飞机大战时间_Timer;

	public Transform _Tsf_结束位置1;

	public GameObject _中墙;

	public EnemyAI_青蝠_飞机 _青蝠_飞机_Prefab;

	private Vector2 _Pos1;

	private Vector2 _Pos2;

	public Transform _Tsf_飞机_生成范围;

	private bool _开始打飞机;

	private Coroutine _Cor_飞机大战中;

	public List<C_背景移动> _List_背景移动;

	private void Awake()
	{
		_飞机大战时间 = DSB_MonoSingleton<MyDataManager>.Instance._SOData_Player.飞机大战时长;
		Vector2 vector = _Tsf_飞机_生成范围.position;
		Vector2 vector2 = _Tsf_飞机_生成范围.localScale / 2f;
		_Pos1 = vector - vector2;
		_Pos2 = vector + vector2;
	}

	private void OnEnable()
	{
		_中墙.SetActive(value: true);
	}

	public void F_隐藏()
	{
		_开始打飞机 = false;
		if (_Cor_飞机大战中 != null)
		{
			StopCoroutine(_Cor_飞机大战中);
			_Cor_飞机大战中 = null;
		}
	}

	private void OnDisable()
	{
		EventArgs_特殊场景.Trigger(EventArgs_特殊场景.Type_EventArgs_特殊场景.飞机大战, 是进入: false);
	}

	public void F_显示()
	{
		PlayerController pC = DSB_MonoSingleton<GameManager>.Instance._PC;
		pC.F_SetPos(_Tsf_Start.position);
		pC.F_开始打飞机();
		pC.F_玩家转向(1f);
		_飞机大战时间_Timer = _飞机大战时间;
		_Cor_飞机大战中 = StartCoroutine(Cor_飞机大战中());
	}

	private IEnumerator Cor_飞机大战中()
	{
		yield return new WaitForEndOfFrame();
		EventArgs_特殊场景.Trigger(EventArgs_特殊场景.Type_EventArgs_特殊场景.飞机大战, 是进入: true);
		_开始打飞机 = true;
		yield return new WaitForSeconds(1.25f);
		while (true)
		{
			F_生成飞机();
			yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
		}
	}

	public void F_重置()
	{
	}

	public Vector2 F_得到范围内随机点()
	{
		return new Vector2(UnityEngine.Random.Range(_Pos1.x, _Pos2.x), UnityEngine.Random.Range(_Pos1.y, _Pos2.y));
	}

	public void F_生成飞机()
	{
		UnityEngine.Object.Instantiate(_青蝠_飞机_Prefab, F_得到范围内随机点(), Quaternion.identity).Init();
	}

	public void F_结束打飞机()
	{
		if (_Cor_飞机大战中 != null)
		{
			StopCoroutine(_Cor_飞机大战中);
			_Cor_飞机大战中 = null;
		}
		EventArgs_特殊场景.Trigger(EventArgs_特殊场景.Type_EventArgs_特殊场景.飞机大战, 是进入: false);
		StartCoroutine(Cor_结束打飞机());
	}

	private IEnumerator Cor_结束打飞机()
	{
		DSB_MonoSingleton<GameManager>.Instance._PC.F_结束打飞机();
		Transform tsf_PC = DSB_MonoSingleton<GameManager>.Instance._PC.transform;
		Rigidbody2D rb2D_PC = tsf_PC.GetComponent<Rigidbody2D>();
		yield return new WaitForSeconds(0.5f);
		_中墙.SetActive(value: false);
		yield return new WaitForEndOfFrame();
		float num = Vector2.Distance(tsf_PC.position, _Tsf_结束位置1.position) * 0.18f;
		tsf_PC.DOMove(_Tsf_结束位置1.position, num);
		yield return new WaitForSeconds(num);
		yield return new WaitForEndOfFrame();
		rb2D_PC.drag = 0f;
		rb2D_PC.velocity = new Vector2(35f, 0f);
	}

	private void LateUpdate()
	{
		foreach (C_背景移动 item in _List_背景移动)
		{
			item.OnLateUpdate(Time.deltaTime);
		}
		if (_开始打飞机)
		{
			_飞机大战时间_Timer -= Time.deltaTime;
			if (_飞机大战时间_Timer <= 0f)
			{
				_开始打飞机 = false;
				F_结束打飞机();
			}
		}
	}
}
