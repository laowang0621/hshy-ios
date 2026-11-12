using DG.Tweening;
using DSB;
using UnityEngine;

public class C_精魄 : MonoBehaviour
{
	[Header("'_解锁精魄ID' 同 '_宝箱ID' 主要用于同步位置(玩家未拾取需要保留)")]
	[HideInInspector]
	public int _解锁精魄ID;

	public Transform _Tsf_UI;

	private ItemUI_交互 _ItemUI_交互键;

	public SpriteRenderer _SR_Model;

	public SpriteRenderer _SR_影子;

	private bool _已经开启;

	private void Awake()
	{
		_ItemUI_交互键 = DSB_MonoSingleton<UIManager>.Instance.F_Get_ItemUI_交互();
		_ItemUI_交互键._可见性 = false;
		_ItemUI_交互键.F_SetPos(_Tsf_UI.position);
		_ItemUI_交互键.F_SetText("吸取");
	}

	private void OnDestroy()
	{
		if (_ItemUI_交互键 != null)
		{
			Object.Destroy(_ItemUI_交互键.gameObject);
		}
	}

	public void F_开箱()
	{
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 -= F_开箱;
		_已经开启 = true;
		_ItemUI_交互键._可见性 = false;
		_SR_Model.DOKill();
		_SR_影子.DOKill();
		_SR_Model.DOFade(0f, 0.5f);
		_SR_影子.DOFade(0f, 0.5f);
		DSB_MonoSingleton<GameManager>.Instance.F_解锁技能_精魄(_解锁精魄ID);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!_已经开启 && collision.gameObject.layer == 3)
		{
			_ItemUI_交互键._可见性 = true;
			_ItemUI_交互键.F_SetPos(_Tsf_UI.position);
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 += F_开箱;
		}
	}

	protected virtual void LateUpdate()
	{
		if (_ItemUI_交互键._可见性)
		{
			_ItemUI_交互键.F_SetPos(_Tsf_UI.position);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (!_已经开启 && collision.gameObject.layer == 3)
		{
			_ItemUI_交互键._可见性 = false;
			DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 -= F_开箱;
		}
	}

	private void OnDisable()
	{
		DSB_MonoSingleton<MyInputManager>.Instance._Key_Down_交互 -= F_开箱;
	}

	public void F_隐藏()
	{
		base.gameObject.SetActive(value: false);
	}

	public void F_显示(Vector2 pos)
	{
		base.gameObject.SetActive(value: true);
		base.transform.position = pos;
		_SR_Model.DOKill();
		_SR_影子.DOKill();
		_SR_Model.SetAlpha(0f);
		_SR_影子.SetAlpha(0f);
		_SR_Model.DOFade(1f, 0.85f);
		_SR_影子.DOFade(0.8f, 0.85f);
	}
}
