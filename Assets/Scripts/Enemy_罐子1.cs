using System;
using System.Collections;
using DSB;
using UnityEngine;

public class Enemy_罐子1 : MonoBehaviour, IBeAttack, IEnemy
{
	private GameObject _GO_灵蕴;

	private Animator _Ani;

	private Collider2D _Co2D;

	public bool _Is死亡;

	public int _阵营
	{
		get
		{
			return 3;
		}
		set
		{
			MyLog.LogError("？？？？？");
		}
	}

	public Vector2 _BeAttack_Pos { get; set; }

	public float _BeAttack_击退力度 { get; set; }

	public event Action _Event_死亡 = delegate
	{
	};

	private void Awake()
	{
		_Ani = GetComponent<Animator>();
		_GO_灵蕴 = base.transform.Find("C_灵蕴").gameObject;
		_Co2D = GetComponent<Collider2D>();
	}

	public void BeAttack(AttackInfo attackInfo)
	{
		if (!_Is死亡)
		{
			this._Event_死亡?.Invoke();
			_Co2D.enabled = false;
			StartCoroutine(Cor_破碎());
		}
	}

	private IEnumerator Cor_破碎()
	{
		_Ani.Play("破碎");
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_打碎罐子();
		_Is死亡 = true;
		yield return new WaitForSeconds(0.1f);
		if (UnityEngine.Random.value < 0.4f)
		{
			_GO_灵蕴.SetActive(value: true);
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_收集灵韵();
			DSB_MonoSingleton<GameManager>.Instance.F_获得经验(5f);
		}
		yield return new WaitForSeconds(3f);
		base.gameObject.SetActive(value: false);
	}

	public void F_隐藏()
	{
	}

	public void F_显示()
	{
		if (_Is死亡)
		{
			base.gameObject.SetActive(value: false);
		}
		else
		{
			base.gameObject.SetActive(value: true);
		}
	}

	public void F_重置()
	{
		_Is死亡 = false;
		_Co2D.enabled = true;
		_GO_灵蕴.SetActive(value: false);
	}
}
