using System.Collections;
using System.Collections.Generic;
using DSB;
using UnityEngine;

public class C_雪僵尸_障碍 : MonoBehaviour, IBeAttack, IEnemy
{
	public SpriteRenderer _SR_静止;

	public List<Sprite> _List_静止_Sprite;

	private bool _已经击碎;

	public GameObject _GO_烟雾_Prefab;

	public Vector3 _生成偏移 = new Vector3(0f, 1f, 1f);

	private GameObject _GO_灵蕴;

	public bool _有概率爆出经验;

	public int _阵营
	{
		get
		{
			return 2;
		}
		set
		{
			MyLog.LogError(114514);
		}
	}

	public Vector2 _BeAttack_Pos { get; set; }

	public float _BeAttack_击退力度 { get; set; }

	private void Awake()
	{
		_SR_静止.sprite = _List_静止_Sprite[Random.Range(0, _List_静止_Sprite.Count)];
		_GO_灵蕴 = base.transform.Find("C_灵蕴")?.gameObject;
	}

	public void BeAttack(AttackInfo attackInfo)
	{
		if (!_已经击碎)
		{
			_已经击碎 = true;
			if (_有概率爆出经验)
			{
				StartCoroutine(Cor_破碎());
				return;
			}
			Object.Instantiate(_GO_烟雾_Prefab, base.transform.position + _生成偏移, Quaternion.identity);
			base.gameObject.SetActive(value: false);
		}
	}

	private IEnumerator Cor_破碎()
	{
		Object.Instantiate(_GO_烟雾_Prefab, base.transform.position + _生成偏移, Quaternion.identity);
		if (Random.value < 0.6f)
		{
			if (_GO_灵蕴 != null)
			{
				_GO_灵蕴.SetActive(value: true);
			}
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_收集灵韵();
			DSB_MonoSingleton<GameManager>.Instance.F_获得经验(10f);
			yield return new WaitForSeconds(0.15f);
		}
		base.gameObject.SetActive(value: false);
	}

	public void F_显示()
	{
		_SR_静止.sprite = _List_静止_Sprite[Random.Range(0, _List_静止_Sprite.Count)];
		base.gameObject.SetActive(!_已经击碎);
	}

	public void F_重置()
	{
		_已经击碎 = false;
	}

	public void F_隐藏()
	{
		base.gameObject.SetActive(value: false);
	}
}
