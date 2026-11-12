using System.Collections.Generic;
using UnityEngine;

public class C_大呱呱3_随机冰锥 : MonoBehaviour
{
	public SpriteRenderer _SR_冰锥;

	public List<Sprite> _List_冰锥;

	private void Awake()
	{
		_SR_冰锥.sprite = _List_冰锥[Random.Range(0, _List_冰锥.Count)];
	}
}
