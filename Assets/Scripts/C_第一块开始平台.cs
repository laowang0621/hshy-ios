using DG.Tweening;
using DSB;
using UnityEngine;

public class C_第一块开始平台 : MonoBehaviour
{
	public C_是马楼就下地狱100层 _C_是马楼就下地狱100层;

	public Collider2D _Co_触发器;

	public Collider2D _Co;

	public SpriteRenderer _SR;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3 && collision.gameObject.CompareTag("Player"))
		{
			_Co_触发器.enabled = false;
			_C_是马楼就下地狱100层.F_玩家开始下地狱();
			Invoke("F_隐藏平台", 1.5f);
		}
	}

	private void F_隐藏平台()
	{
		_Co.enabled = false;
		_SR.DOFade(0f, 0.5f).OnComplete(delegate
		{
		});
	}

	private void OnEnable()
	{
		F_重置状态();
	}

	public void F_重置状态()
	{
		_Co_触发器.enabled = true;
		_Co.enabled = true;
		_SR.DOKill();
		_SR.SetAlpha(1f);
	}
}
