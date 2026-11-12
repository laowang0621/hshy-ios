using DG.Tweening;
using UnityEngine;

public class C_是马楼就下地狱100层_死亡区域 : MonoBehaviour
{
	public Transform _Tsf_上;

	public Transform _Tsf_下;

	private float _开始时进入所需时间 = 1.5f;

	private C_是马楼就下地狱100层 _是马楼就下地狱100层;

	public C_是马楼就下地狱100层_死亡区域_下 _是马楼就下地狱100层_死亡区域_下;

	public void F_Init(C_是马楼就下地狱100层 是马楼就下地狱100层, Vector2 pos)
	{
		base.transform.position = pos;
		_是马楼就下地狱100层 = 是马楼就下地狱100层;
		_Tsf_上.DOKill();
		_Tsf_下.DOKill();
		_Tsf_上.localPosition = new Vector3(0f, 15f, 0f);
		_Tsf_下.localPosition = new Vector3(0f, -15f, 0f);
		_Tsf_上.gameObject.SetActive(value: false);
		_Tsf_下.gameObject.SetActive(value: false);
		_是马楼就下地狱100层_死亡区域_下.F_Init(是马楼就下地狱100层);
		_Tsf_下.gameObject.SetActive(value: true);
	}

	public void F_开始下地狱()
	{
		_Tsf_上.gameObject.SetActive(value: true);
		_Tsf_上.DOLocalMoveY(9.5f, _开始时进入所需时间).SetEase(Ease.Linear);
		_Tsf_下.DOLocalMoveY(-9.5f, _开始时进入所需时间).SetEase(Ease.Linear);
	}

	public void F_镜头移动调用(float 镜头y)
	{
		Vector3 position = base.transform.position;
		position.y = 镜头y;
		base.transform.position = position;
	}

	public void F_到达底部()
	{
		_Tsf_下.DOLocalMoveY(-25f, _开始时进入所需时间).OnComplete(delegate
		{
			_Tsf_下.gameObject.SetActive(value: false);
		}).SetEase(Ease.Linear);
	}
}
