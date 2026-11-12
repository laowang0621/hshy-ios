public class C_平台_尖刺平台 : C_平台
{
	public C_攻击判断 _C_攻击判断;

	public float _频率 = 0.35f;

	private void Start()
	{
		F_清空之前的回调();
		_C_攻击判断._攻击命中回调 += F_持续造成伤害;
	}

	private void F_清空之前的回调()
	{
		CancelInvoke();
		_C_攻击判断._攻击命中回调 -= F_持续造成伤害;
	}

	private void F_持续造成伤害()
	{
		InvokeRepeating("F_对范围内敌人造成伤害", _频率, _频率);
	}

	private void F_对范围内敌人造成伤害()
	{
		_C_攻击判断?.F_对范围人员造成伤害();
	}
}
