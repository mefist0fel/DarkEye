using UnityEngine;

public sealed class DeathAnimation : MonoBehaviour {
    [SerializeField]
    private float destroyTime = 0.3f;
    [SerializeField]
    private AnimationCurve curve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    private float timer = 0;
    void Start() {
        
    }

    private void Update() {
        timer += Time.deltaTime;
        transform.localScale = Vector3.one * curve.Evaluate(timer / destroyTime);
        if (timer > destroyTime) {
            Destroy(gameObject);
        }
    }
}
