using DSB;
using UnityEngine;

public class C_黄风带圣_召唤蝜蝂 : MonoBehaviour
{
	public C_攻击判断 _C_攻击判断;

	public Rigidbody2D _Rb2D;

	public float _Speed = 5f;

	public GameObject _震荡波Prefab;

	public void F_Ani_生成震荡波()
	{
		DSB_MonoSingleton<AudioManager>.Instance.F_Play_SFX_地震();
		Object.Instantiate(_震荡波Prefab, base.transform.position, Quaternion.identity).GetComponentInChildren<C_攻击判断>()._伤害 = _C_攻击判断._伤害;
	}

	public void F_Init(Vector3 dir, float 伤害, int 阵营 = 2)
	{
		dir = new Vector3((dir.x > 0f) ? 1 : (-1), 0f, 0f);
		base.transform.localScale = new Vector3((dir.x < 0f) ? 1 : (-1), 1f, 1f);
		base.transform.position -= dir * 4f;
		_Rb2D.velocity = dir * _Speed;
		_C_攻击判断._伤害 = 伤害;
		_C_攻击判断._阵营 = 阵营;
	}
}
