using DSB;
using UnityEngine;

public class C_传送区域_筋斗云_Boss : MonoBehaviour
{
	public Animator _Ani_筋斗云;

	public Collider2D _Co2D;

	public bool _需要弹出对话 = true;

	public Enum_对话 _Enum_对话 = Enum_对话.筋斗云_黑风山;

	public int _目标点;

	public C_对话 _C_对话;

	private void Awake()
	{
		_C_对话._Enum_对话类型 = _Enum_对话;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3)
		{
			DSB_MonoSingleton<AudioManager>.Instance.F_Play_UISFX_使用筋斗云();
			DSB_MonoSingleton<GameManager>.Instance.F_上香(_目标点, 需要提示: false);
		}
	}

	public void F_隐藏()
	{
		_Co2D.enabled = false;
		base.gameObject.SetActive(value: false);
	}

	public void F_显示()
	{
		_C_对话.gameObject.SetActive(value: false);
		DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(base.gameObject, GetHashCode(), 2f, delegate
		{
			base.gameObject.SetActive(value: true);
			_Ani_筋斗云.Play("降落");
			Invoke("F_弹出对话", 0.8f);
			Invoke("F_开启传送", 1.3f);
		});
	}

	private void F_弹出对话()
	{
		if (_需要弹出对话)
		{
			_C_对话.gameObject.SetActive(value: true);
		}
	}

	private void F_开启传送()
	{
		_Co2D.enabled = true;
	}

	public void F_直接显示()
	{
		base.gameObject.SetActive(value: true);
		_Co2D.enabled = true;
	}
}
