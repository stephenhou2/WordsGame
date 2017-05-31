﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gambler : StateSkillEffect {

	[HideInInspector]public StateSkillEffect[] stateSkillEffects;//针对赌徒维护的状态表单

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		if (stateSkillEffects == null) {
			stateSkillEffects = GameManager.gameManager.GenerateStates (null);
		}

		bool succeedGamble = isEffective (0.5f + skillLevel * this.scaler);

		if (succeedGamble) {
			
			StateSkillEffect buff = null;


			while (buff == null) {
				int index = Random.Range (0, stateSkillEffects.Length);

				StateSkillEffect sse = stateSkillEffects [index];
				
				if (sse.effectType == EffectType.Buff) {
					buff = Instantiate (sse);
					buff.effectDuration = 1;
				}

			}
			self.AddState (buff,skillLevel);
			return;
		}

		StateSkillEffect debuff = null;

		while (debuff == null) {
			int index = Random.Range (0, stateSkillEffects.Length);

			StateSkillEffect sse = stateSkillEffects [index];

			if (sse.effectType == EffectType.DeBuff) {
				debuff = Instantiate (sse);
				debuff.effectDuration = 1;
			}
		}
		self.AddState (debuff,skillLevel);
	}
}
