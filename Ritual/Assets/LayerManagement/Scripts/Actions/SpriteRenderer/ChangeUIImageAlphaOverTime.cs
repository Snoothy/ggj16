using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace LayerManagement.Action.Finite {

	[System.Serializable]
	public class ChangeUIImageAlphaOverTimeInfo {
		public float time = 0.0f;
		public float to;
		[Range(0.0f, 1.0f)]
		public float delay = 0.0f;

		public void update(ChangeUIImageAlphaOverTimeInfo v) {
			this.time = v.time;
			this.to = v.to;
			this.delay = v.delay;
		}
		
		public ChangeUIImageAlphaOverTimeInfo () {
			
		}
		
		public ChangeUIImageAlphaOverTimeInfo (float time, float to, float delay) {
			this.time = time;
			this.to = to;
			this.delay = delay;
		}
	}

	[RequireComponent(typeof(SpriteRenderer))]
	public class ChangeUIImageAlphaOverTime : AFiniteAction<ChangeUIImageAlphaOverTimeInfo> {

		public ChangeUIImageAlphaOverTimeInfo actionInfo;
		private Image _spr;
		protected Image getImage () {
			_spr = _spr ? _spr : this.GetComponent<Image>();
			return _spr;
		}

		private float from;
		private float elapsedTime;

		override protected ChangeUIImageAlphaOverTimeInfo getT () {
			return this.actionInfo;
		}
		
		override protected void setT (ChangeUIImageAlphaOverTimeInfo v) {
			this.actionInfo = v;
		}
		
		override public void runActionWith (ChangeUIImageAlphaOverTimeInfo v) {
			this.actionInfo.update(v);
			isRunning = true;

			from = this.getImage().color.a;
			elapsedTime = 0.0f;
		}
		
		override protected void incrementAction (float deltaTime) {
			elapsedTime += deltaTime * this.actionSpeed;
			float t = AFiniteAction<ChangeUIImageAlphaOverTimeInfo>.delayTime(this.actionInfo.delay, this.elapsedTime, this.actionInfo.time);
			Color color = getImage().color;
			color.a = Mathf.Lerp(this.from, this.actionInfo.to, t);
			getImage().color = color;

			if (t >= 1.0f) {
				this.actionCompleted();
			}
		}
		
		override protected void resetAction () {

		}
	}

}
