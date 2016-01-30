using UnityEngine;

namespace LayerManagement.Action.Finite
{
    [System.Serializable]
    public class ScaleToByTimeInfo
    {
        public float time = 0.0f;
        public Vector3 to;
        [Range(0.0f, 1.0f)]
        public float delay = 0.0f;

        public void update(ScaleToByTimeInfo v)
        {
            this.time = v.time;
            this.to = v.to;
            this.delay = v.delay;
        }

        public ScaleToByTimeInfo()
        {

        }

        public ScaleToByTimeInfo(float time, Vector3 to, float delay)
        {
            this.time = time;
            this.to = to;
            this.delay = delay;
        }
    }

    public class ScaleToByTime : AFiniteAction<ScaleToByTimeInfo>
    {

        public ScaleToByTimeInfo actionInfo;
        public string ID = "";

        private Vector3 from;
        private float elapsedTime;

        override protected ScaleToByTimeInfo getT()
        {
            return this.actionInfo;
        }

        override protected void setT(ScaleToByTimeInfo v)
        {
            this.actionInfo = v;
        }

        override public void runActionWith(ScaleToByTimeInfo v)
        {
            this.actionInfo.update(v);
            isRunning = true;

            from = this.transform.localScale;
            elapsedTime = 0.0f;
        }

        override protected void incrementAction(float deltaTime)
        {
            elapsedTime += deltaTime * this.actionSpeed;
            float t = AFiniteAction<ScaleToByTimeInfo>.delayTime(this.actionInfo.delay, this.elapsedTime, this.actionInfo.time);
            if (t > 0.0f)
            {
                this.transform.localScale = Vector3.Lerp(this.from, this.actionInfo.to, t);
            }

            if (t >= 1.0f)
            {
                this.actionCompleted();
            }
        }

        override protected void resetAction()
        {

        }
    }
}