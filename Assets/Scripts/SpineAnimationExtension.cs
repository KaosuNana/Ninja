using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public static class SpineAnimationExtension  {
	public static void Update(this SkeletonAnimation animation){
		animation.Update(Time.unscaledTime);
	}

}
