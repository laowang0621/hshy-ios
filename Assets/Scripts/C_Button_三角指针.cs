using UnityEngine;
using UnityEngine.EventSystems;

public class C_Button_三角指针 : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
	public GameObject _GO_三角指针;

	private void Start()
	{
		_GO_三角指针.SetActive(value: false);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		EventSystem.current.SetSelectedGameObject(base.gameObject);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
	}

	public void OnSelect(BaseEventData eventData)
	{
		_GO_三角指针.SetActive(value: true);
	}

	public void OnDeselect(BaseEventData eventData)
	{
		_GO_三角指针.SetActive(value: false);
	}
}
