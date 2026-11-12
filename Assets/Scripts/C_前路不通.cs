using DSB;
using UnityEngine;

public class C_前路不通 : MonoBehaviour
{
	public string _前路不通提示 = "前路不通！";

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3)
		{
			DSB_MonoSingleton<UIManager>.Instance.F_显示Tips(_前路不通提示);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 3)
		{
			DSB_MonoSingleton<UIManager>.Instance.F_隐藏Tips();
		}
	}
}
