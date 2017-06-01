using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, TriggerType triggerType, int attachedInfo)
	{

		//原始魔法伤害值
		int originalDamage = (int)((this.scaler * skillLevel / ( 1 + this.scaler * skillLevel)) * target.magic * this.actionCount * self.hurtScaler);

		//抵消魔抗作用后的实际伤害值
		int actualDamage = (int)(originalDamage / (1 + seed * self.magicResist));

		//抵消的伤害值
		int hurtOffset = originalDamage - actualDamage;

		self.OnTrigger (TriggerType.Debuff, hurtOffset);

//		//抵消的伤害值
//		int DamageOffset = originalDamage - actualDamage;
//
//		//己方触发命中效果
//		self.OnTrigger (TriggerType.Hit, 0);
//		//目标触发被击中效果
//		target.OnTrigger (TriggerType.BeHit, DamageOffset);

		self.health -= actualDamage;

		if (self.health < 0) {
			self.health = 0;
		}

	}
}
