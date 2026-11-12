using DG.Tweening;
using UnityEngine;

public class C_上下浮动 : MonoBehaviour
{
	private void Start()
	{
		base.transform.DOMoveY(base.transform.position.y + 1.5f, 0.6f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
	}

	private void OnDestroy()
	{
		base.transform.DOKill();
	}
}
