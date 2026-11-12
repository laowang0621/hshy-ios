using DG.Tweening;
using DSB;
using UnityEngine;

public class C_大呱呱4_毒液 : MonoBehaviour
{
	public SpriteRenderer _SR_Model;

	public C_攻击判断 _C_攻击判断;

	private void Awake()
	{
		_SR_Model.SetAlpha(0f);
		_SR_Model.DOFade(0.8f, 0.5f);
		_C_攻击判断._开启僵直效果 = false;
	}

	public void F_Init(int 阵营, float 伤害, float 频率, float 持续时间)
	{
		_C_攻击判断._阵营 = 阵营;
		_C_攻击判断._伤害 = 伤害;
		_C_攻击判断._僵直伤害 = 0f;
		InvokeRepeating("F_对范围内敌人造成伤害", 频率, 频率);
		Invoke("F_结束", 持续时间);
	}

	private void F_对范围内敌人造成伤害()
	{
		_C_攻击判断?.F_对范围人员造成伤害();
	}

	private void F_结束()
	{
		_SR_Model.DOFade(0f, 1.5f).OnComplete(delegate
		{
			Object.Destroy(base.gameObject);
		});
	}
}
