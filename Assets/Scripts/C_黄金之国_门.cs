using DG.Tweening;
using DSB;
using UnityEngine;

public class C_黄金之国_门 : MonoBehaviour
{
	public GameObject _GO_门关闭;

	private void OnEnable()
	{
		if (DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._猪员外_阶段 != 4)
		{
			_GO_门关闭.GetComponent<SpriteRenderer>().SetAlpha(1f);
			_GO_门关闭.SetActive(value: true);
		}
		else
		{
			_GO_门关闭.GetComponent<SpriteRenderer>().SetAlpha(0f);
			_GO_门关闭.SetActive(value: false);
		}
	}

	public void F_开门()
	{
		_GO_门关闭.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f).OnComplete(delegate
		{
			_GO_门关闭.SetActive(value: false);
		});
	}
}
