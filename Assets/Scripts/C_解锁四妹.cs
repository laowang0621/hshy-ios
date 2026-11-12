using System;
using DSB;
using UnityEngine;

[Obsolete("这个脚本应该不需要了")]
public class C_解锁四妹 : MonoBehaviour
{
	public Transform _Tsf_四姐位置;

	public GameObject _烟雾_Prefab;

	public void F_解锁()
	{
		if (!DSB_MonoSingleton<GameManager>.Instance.F_解锁并生成同伴(3002, _Tsf_四姐位置.position, 生成烟雾: false, 强制生成: false, 更改朝向: true))
		{
			UnityEngine.Object.Instantiate(_烟雾_Prefab, _Tsf_四姐位置.position + new Vector3(0f, 1f), Quaternion.identity);
		}
	}
}
