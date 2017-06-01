using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, TriggerType triggerType, int attachedInfo)
	{
		int index = Random.Range (0, target.skills.Count);

		Skill s = Instantiate(target.skills [index]);

		s.skillLevel = skillLevel;

		s.actionCount = 0;

		s.isCopiedSkill = true;

		self.skills.Add (s);


	}

}
