using DG.Tweening;
using DSB;
using UnityEngine;

public class EnemyAI_百眼魔君_眼球 : MonoBehaviour
{
	public Transform _Tsf_眼球;

	public Transform _Model_眼白;

	public Transform _Model_眼珠;

	public SpriteRenderer _SR_眼球;

	public SpriteRenderer _SR_眼珠;

	public SpriteRenderer _SR_影子;

	private Transform _Tsf_Player;

	private void Awake()
	{
		_SR_眼球.SetAlpha(0f);
		_SR_眼珠.SetAlpha(0f);
		_SR_影子.SetAlpha(0f);
	}

	private void Start()
	{
		_Tsf_Player = DSB_MonoSingleton<GameManager>.Instance._PC.transform;
	}

	private void Update()
	{
		if (!(_Tsf_Player == null))
		{
			Vector3 normalized = (_Tsf_Player.position - _Model_眼珠.position).normalized;
			Vector3 vector = _Model_眼白.InverseTransformDirection(normalized);
			_Model_眼珠.localPosition = Vector3.Lerp(_Model_眼珠.localPosition, vector * 0.18f, 8f * Time.deltaTime);
		}
	}

	public void F_Init()
	{
		Vector3 position = base.transform.position;
		_Tsf_眼球.localPosition = new Vector3(0f, Random.Range(4f, 8f), 0f);
		_SR_眼球.DOFade(0.95f, Random.Range(0.4f, 0.8f)).SetEase(Ease.OutSine);
		_SR_眼珠.DOFade(0.95f, Random.Range(0.4f, 0.8f)).SetEase(Ease.OutSine);
		_SR_影子.DOFade(0.5f, Random.Range(0.4f, 0.8f)).SetEase(Ease.OutSine);
		_Tsf_眼球.localScale = Vector3.one;
		_Tsf_眼球.DOScale(Random.Range(1.05f, 1.12f), Random.Range(0.9f, 1.5f)).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
		_Model_眼白.DOLocalMoveY(Random.Range(0.2f, 0.5f), Random.Range(1.2f, 2f)).SetRelative().SetLoops(-1, LoopType.Yoyo)
			.SetEase(Ease.InOutSine);
		Vector3[] path = new Vector3[4]
		{
			position + new Vector3(Random.Range(0.15f, 0.25f), Random.Range(0.08f, 0.15f), 0f),
			position + new Vector3(Random.Range(-0.25f, -0.15f), Random.Range(-0.15f, -0.08f), 0f),
			position + new Vector3(Random.Range(0.08f, 0.18f), Random.Range(-0.22f, -0.12f), 0f),
			position
		};
		base.transform.DOPath(path, Random.Range(3.5f, 5.5f), PathType.CatmullRom).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
	}

	private void OnDestroy()
	{
		_Tsf_眼球.DOKill();
		_Model_眼白.DOKill();
		base.transform.DOKill();
	}

	public void F_销毁()
	{
		float duration = Random.Range(0.35f, 0.6f);
		_SR_眼球.DOFade(0f, duration).SetEase(Ease.InSine);
		_SR_眼珠.DOFade(0f, duration).SetEase(Ease.InSine);
		_SR_影子.DOFade(0f, duration).SetEase(Ease.InSine);
		DSB_MonoSingleton<DSB_TimerManager>.Instance.AddTimer(base.gameObject, 1f, delegate
		{
			Object.Destroy(base.gameObject);
		});
	}
}
