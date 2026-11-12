using DSB;
using UnityEngine;

public class UI_Test_测试手柄PC布局切换 : MonoBehaviour
{
	public Animator _Ani_精魄;

	public Animator _Ani_法术;

	private void Start()
	{
		_Ani_精魄.Play("键盘布局_精魄", 0, 0f);
		_Ani_法术.Play("键盘布局_法术", 0, 0f);
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_Any键盘 += delegate
		{
			_Ani_精魄.Play("键盘布局_精魄", 0, 0f);
			_Ani_法术.Play("键盘布局_法术", 0, 0f);
		};
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_Any手柄 += delegate
		{
			_Ani_精魄.Play("手柄布局_精魄", 0, 0f);
			_Ani_法术.Play("默认", 0, 0f);
		};
	}
}
