using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poision : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		//原始伤害值
		int originalDamage = (int)((this.scaler * skillLevel / ( 1 + this.scaler * skillLevel)) * (self.attack + self.magic) * this.actionCount * self.hurtScaler);

		//		//抵消的伤害值
		//		int DamageOffset = originalDamage - actualDamage;
		//
		//		//己方触发命中效果
		//		self.OnTrigger (TriggerType.Hit, 0);
		//		//目标触发被击中效果
		//		target.OnTrigger (TriggerType.BeHit, DamageOffset);

		self.health -= originalDamage;

	}
}
