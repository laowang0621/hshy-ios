using DG.Tweening;
using UnityEngine;

public class C_前景层 : MonoBehaviour
{
	public Transform _Tsf_虚化层;

	private SpriteRenderer[] _List_SR_图层;

	private int Count;

	private void Awake()
	{
		_List_SR_图层 = _Tsf_虚化层.GetComponentsInChildren<SpriteRenderer>();
		SpriteRenderer[] list_SR_图层 = _List_SR_图层;
		for (int i = 0; i < list_SR_图层.Length; i++)
		{
			list_SR_图层[i].color = new Color(1f, 1f, 1f, 1f);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer != 3)
		{
			return;
		}
		if (Count == 0)
		{
			SpriteRenderer[] list_SR_图层 = _List_SR_图层;
			for (int i = 0; i < list_SR_图层.Length; i++)
			{
				list_SR_图层[i].DOFade(0.4f, 0.2f);
			}
		}
		Count++;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer != 3)
		{
			return;
		}
		Count--;
		if (Count == 0)
		{
			SpriteRenderer[] list_SR_图层 = _List_SR_图层;
			for (int i = 0; i < list_SR_图层.Length; i++)
			{
				list_SR_图层[i].DOFade(1f, 0.2f);
			}
		}
	}
}
