using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrow : BaseSkillEffect {

	public override void AffectAgent (BattleAgent self, 
		BattleAgent target,
		int skillLevel,
		TriggerType triggerType,
		int arg)
	{

		int attackCount = 0;//攻击次数计数

		Debug.Log ("use magic arrow");

		do {
			attackCount++;
			//计算对方闪避率
			float dodge = seed * target.agility / (1 + seed * target.agility);
			//判断对方是否闪避成功
			if (isEffective (dodge)) {
				Debug.Log ("enemy dodge your attack");
				//目标触发闪避成功效果
				target.OnTrigger (TriggerType.Dodge, 0);
				return;
			}

			// 对方未闪避成功，判断自己是否打出了暴击
			bool isCrit = isEffective (seed * self.crit / (1 + seed * self.crit));

			if (isCrit) {
				self.critScaler = 2.0f;
			}

			//原始魔法伤害值
			int originalDamage = (int)((scaler * skillLevel + 1) * self.magic * target.hurtScaler * self.critScaler);

			Debug.Log("original damage" + originalDamage);

			//抵消魔抗作用后的实际伤害值
			int actualDamage = (int)(originalDamage / (1 + seed * target.magicResist) + 0.5f);


			Debug.Log("actual damage" + actualDamage);
			//抵消的伤害值
			int DamageOffset = originalDamage - actualDamage;

			//己方触发命中效果
			self.OnTrigger (TriggerType.MagicalHit, 0);
			//目标触发被击中效果
			target.OnTrigger (TriggerType.BeMagicalHit, DamageOffset);

			target.health -= actualDamage;

			self.critScaler = 1.0f;

			if(target.health < 0){
				target.health = 0;
			}
			Debug.Log("target health:" + target.health);

		} while(attackCount < self.attackTime);


	}
}
