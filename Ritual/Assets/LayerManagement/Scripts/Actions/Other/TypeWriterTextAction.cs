using UnityEngine;

namespace LayerManagement.Action.Finite
{
    [System.Serializable]
    public class TypeWriterTextActionInfo
    {
        public float time = 0.0f;
        [TextArea(4,10)]
        public string text;
        public TextMesh textMesh;
        [Range(0.0f, 1.0f)]
        public float delay = 0.0f;

        public void update(TypeWriterTextActionInfo v)
        {
            this.time = v.time;
            this.text = v.text;
            this.textMesh = v.textMesh;
            this.delay = v.delay;
        }

        public TypeWriterTextActionInfo()
        {

        }

        public TypeWriterTextActionInfo(float time, string text, TextMesh textMesh, float delay)
        {
            this.time = time;
            this.text = text;
            this.textMesh = textMesh;
            this.delay = delay;
        }
    }

    public class TypeWriterTextAction : AFiniteAction<TypeWriterTextActionInfo>
    {
        private float elapsedTime;

        public TypeWriterTextActionInfo actionInfo;
        override protected TypeWriterTextActionInfo getT()
        {
            return this.actionInfo;
        }

        override protected void setT(TypeWriterTextActionInfo v)
        {
            this.actionInfo = v;
        }

        override public void runActionWith(TypeWriterTextActionInfo v)
        {
            this.actionInfo.update(v);
            isRunning = true;

            elapsedTime = 0.0f;

            if (v.time <= 0.0f)
            {
                incrementAction(0.0f);
            }
        }

        override protected void incrementAction(float deltaTime)
        {
            elapsedTime += deltaTime * this.actionSpeed;
            float t = AFiniteAction<TypeWriterTextActionInfo>.delayTime(this.actionInfo.delay, this.elapsedTime, this.actionInfo.time);

            if (t > 0.0f)
            {
                this.actionInfo.textMesh.text = this.actionInfo.text.Substring(0, Mathf.FloorToInt(((float)this.actionInfo.text.Length) * t));
            }

            if (t >= 1.0f)
            {
                this.isSpeeding = false;
                this.actionSpeed = 1.0f;
                this.actionCompleted();
            }
        }

        override protected void resetAction()
        {

        }

        public void completeNow()
        {
            if (this.isRunning)
            {
                this.elapsedTime = this.actionInfo.time;
                incrementAction(0.0f);
            }
        }

        private bool isSpeeding = false;
        public bool getIsSpeeding()
        {
            return isSpeeding;
        }

        public void completeInSeconds(float time)
        {
            if (this.isRunning && !isSpeeding)
            {
                this.isSpeeding = true;
                if (Mathf.Approximately(time, 0))
                {
                    this.completeNow();
                }
                else {
                    float remainingtime = this.actionInfo.time - this.elapsedTime;
                    this.actionSpeed = remainingtime / (time > remainingtime ? remainingtime : time);
                }
            }
        }
    }
}