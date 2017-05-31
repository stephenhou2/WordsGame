using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		int index = Random.Range (0, target.skills.Count);

		Skill s = target.skills [index];

		s.skillLevel = skillLevel;

		self.skills.Add (target.skills [index]);


	}

	public override void AddStateTo (BattleAgent ba)
	{
		base.AddStateTo (ba);
		ba.skills.RemoveAt (ba.skills.Count - 1);
	}

}
