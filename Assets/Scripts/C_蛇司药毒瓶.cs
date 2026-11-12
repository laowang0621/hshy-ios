using DG.Tweening;
using UnityEngine;

public class C_蛇司药毒瓶 : MonoBehaviour
{
	public float _飞行速度 = 5f;

	public Transform _Tsf_Model;

	public C_大呱呱4_毒液 _毒液_Prefab;

	public AnimationCurve _AC;

	private int _阵营 = 2;

	public float _每次伤害 = 5f;

	public float _频率 = 0.2f;

	public float _持续时间 = 8f;

	public void F_Init(Vector2 目标位置, int 阵营 = 2)
	{
		float duration = Vector2.Distance(目标位置, base.transform.position) / _飞行速度;
		_阵营 = 阵营;
		_Tsf_Model.DOLocalMoveY(0f, duration).SetEase(_AC);
		base.transform.DOMove(目标位置, duration).SetEase(Ease.Linear).OnComplete(delegate
		{
			F_爆炸();
		});
	}

	public void F_Init(Vector2 目标位置, float 每秒伤害, float 频率, float 持续时间, int 阵营)
	{
		float duration = Vector2.Distance(目标位置, base.transform.position) / _飞行速度;
		_阵营 = 阵营;
		_频率 = Mathf.Clamp(频率, 0.1f, 1f);
		_持续时间 = 持续时间;
		_每次伤害 = 每秒伤害 * _频率;
		_Tsf_Model.DOLocalMoveY(0f, duration).SetEase(_AC);
		base.transform.DOMove(目标位置, duration).SetEase(Ease.Linear).OnComplete(delegate
		{
			F_爆炸();
		});
	}

	private void F_爆炸()
	{
		_Tsf_Model.DOKill();
		base.transform.DOKill();
		F_生成一个毒液();
		Object.Destroy(base.gameObject, 0.25f);
	}

	private void F_生成一个毒液()
	{
		Object.Instantiate(_毒液_Prefab, base.transform.position, Quaternion.identity).F_Init(_阵营, _每次伤害, _频率, _持续时间);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			F_爆炸();
		}
	}
}
