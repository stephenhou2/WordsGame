using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeBack : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, TriggerType triggerType, int attachedInfo)
	{

		bool strikeBack = isEffective (this.scaler * skillLevel);

		if (strikeBack && (triggerType == TriggerType.BePhysicalHit || triggerType == TriggerType.BeMagicalHit)) {

			Debug.Log (self.agentName + "使用了反击");

			float dodge = seed * target.agility / (1 + seed * target.agility);

			//判断对方是否闪避成功
			if (isEffective (dodge)) {
				Debug.Log ("enemy dodge your attack");
				//目标触发闪避成功效果
				target.OnTrigger (TriggerType.Dodge, 0);
				return;
			}

			bool isCrit = isEffective (seed * self.crit / (1 + seed * self.crit));

			if (isCrit) {
				self.critScaler = 2.0f;
			}

			//原始物理伤害值
			int originalDamage = (int)(self.attack * target.hurtScaler * self.critScaler);

			//抵消护甲作用后的实际伤害值
			int actualDamage = (int)(originalDamage / (1 + seed * target.amour));

			//抵消的伤害值
			int DamageOffset = originalDamage - actualDamage;

			//己方触发命中效果
			self.OnTrigger (TriggerType.PhysicalHit, 0);
			//目标触发被击中效果
			target.OnTrigger (TriggerType.BePhysicalHit, DamageOffset);

			target.health -= actualDamage;

			if (target.health < 0) {
				target.health = 0;
			}

			self.critScaler = 1.0f;
		}
	}
}
