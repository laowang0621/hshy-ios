using UnityEngine;

public interface IBeAttack
{
	int _阵营 { get; set; }

	Vector2 _BeAttack_Pos { get; set; }

	float _BeAttack_击退力度 { get; set; }

	bool _开启反弹弹道 => false;

	void BeAttack(AttackInfo attackInfo);
}
