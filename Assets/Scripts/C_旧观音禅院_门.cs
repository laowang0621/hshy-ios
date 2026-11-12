using DSB;
using UnityEngine;

public class C_旧观音禅院_门 : MonoBehaviour
{
	public GameObject _GO_门打开;

	public GameObject _GO_门碰撞;

	private void OnEnable()
	{
		_GO_门打开.SetActive(value: false);
		_GO_门碰撞.SetActive(value: true);
		bool[] 钟 = DSB_MonoSingleton<MyDataManager>.Instance._Data_SL._钟1;
		for (int i = 0; i < 钟.Length; i++)
		{
			if (!钟[i])
			{
				return;
			}
		}
		_GO_门打开.SetActive(value: true);
		_GO_门碰撞.SetActive(value: false);
	}
}
