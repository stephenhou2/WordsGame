﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeBack : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		bool strikeBack = isEffective (this.scaler * skillLevel);
		if (strikeBack && triggerType == TriggerType.BeHit) {
			#warning 进行一次普通攻击
			float dodge = seed * target.agility / (1 + seed * target.agility);
			//判断对方是否闪避成功
			if (isEffective (dodge)) {
				Debug.Log ("enemy dodge your attack");
				//目标触发闪避成功效果
				target.OnTrigger (TriggerType.Dodge, 0);
				return;
			}
			//原始物理伤害值
			int originalDamage = (int)(self.attack * target.hurtScaler * self.critScaler);

			//抵消护甲作用后的实际伤害值
			int actualDamage = (int)(originalDamage / (1 + seed * target.amour));

			//抵消的伤害值
			int DamageOffset = originalDamage - actualDamage;

			//己方触发命中效果
			self.OnTrigger (TriggerType.Hit, 0);
			//目标触发被击中效果
			target.OnTrigger (TriggerType.BeHit, DamageOffset);

			target.health -= actualDamage;
		}
	}
}
