using UnityEngine;

public class DestroyOnFadeCompletion : MonoBehaviour {

	public void FadeCompleted() {
		Object.Destroy(gameObject);
	}

}
