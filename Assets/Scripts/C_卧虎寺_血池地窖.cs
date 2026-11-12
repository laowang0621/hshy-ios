using DG.Tweening;
using DSB;
using UnityEngine;

public class C_卧虎寺_血池地窖 : MonoBehaviour
{
	public Collider2D _Co2D_地窖开关;

	public GameObject _GO_提示缺少道具;

	public GameObject _GO_台阶空气墙;

	public GameObject _GO_血池;

	public GameObject _传送区域_地窖;

	public GameObject _GO_地窖门;

	public bool _虎先锋死亡;

	public bool 获得石之厉;

	public bool _地窖入口已开启;

	private void OnEnable()
	{
		_虎先锋死亡 = DSB_MonoSingleton<MyDataManager>.Instance.F_判断Boss是否已经击败(20201);
		获得石之厉 = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._获得石之厉;
		_地窖入口已开启 = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._地窖入口已开启;
		if (_虎先锋死亡 && _地窖入口已开启)
		{
			_Co2D_地窖开关.enabled = false;
			_GO_提示缺少道具.SetActive(value: false);
			_GO_台阶空气墙.SetActive(value: false);
			_GO_血池.SetActive(value: false);
			_GO_地窖门.SetActive(value: true);
			_传送区域_地窖.SetActive(value: true);
		}
		else
		{
			_Co2D_地窖开关.enabled = _虎先锋死亡 && 获得石之厉;
			_GO_提示缺少道具.SetActive(!获得石之厉);
			_GO_台阶空气墙.SetActive(!_虎先锋死亡);
			_GO_血池.SetActive(value: true);
			_GO_地窖门.SetActive(value: false);
			_传送区域_地窖.SetActive(value: false);
			_GO_血池.GetComponent<SpriteRenderer>().SetAlpha(1f);
		}
	}

	public void F_虎先锋死亡()
	{
		_GO_台阶空气墙.SetActive(value: false);
		_GO_提示缺少道具.SetActive(!获得石之厉);
		_Co2D_地窖开关.enabled = 获得石之厉;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3)
		{
			_Co2D_地窖开关.enabled = false;
			_传送区域_地窖.SetActive(value: true);
			_GO_地窖门.SetActive(value: true);
			_GO_血池.GetComponent<SpriteRenderer>().DOFade(0f, 1f).OnComplete(delegate
			{
				_GO_血池.SetActive(value: false);
			});
			DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._地窖入口已开启 = true;
			DSB_MonoSingleton<MyDataManager>.Instance.F_保存数据();
		}
	}
}
