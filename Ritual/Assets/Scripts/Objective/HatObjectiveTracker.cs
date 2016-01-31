using UnityEngine;

public class HatObjectiveTracker : ObjectiveTracker {

    public Transform altAttachPoint;

    override protected void HandleProduct(Transform other)
    {
        AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = this.gameObject.AddComponent<AudioSource>();
        }

        audioSource.PlayOneShot(correctAnswerClip, 0.3f);

        foreach (HatObjectiveTracker hatObjectiveTracker in GameObject.FindObjectsOfType<HatObjectiveTracker>())
        {
            GameObject duplicatedHat = Instantiate(other.gameObject);
            if (duplicatedHat.GetComponent<SpringJoint>() != null)
            {
                Destroy(duplicatedHat.GetComponent<SpringJoint>());
            }
            Destroy(duplicatedHat.GetComponent<Rigidbody>());

            RenestFirstSprite(duplicatedHat.transform, hatObjectiveTracker.GetComponent<RandomSittingGuest>().spr.sprite.name.ToUpper().Contains("SIDE") ? hatObjectiveTracker.altAttachPoint : hatObjectiveTracker.attachPoint);

            hatObjectiveTracker.outline.SetActive(false);
            duplicatedHat.transform.tag = "Untagged";
            hatObjectiveTracker.completed = true;
        }
        Destroy(other.gameObject);
    }

}
