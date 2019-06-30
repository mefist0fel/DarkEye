using UnityEngine;

public sealed class ShowAnimation : MonoBehaviour {
    [SerializeField]
    private float showTime = 0.3f;
    [SerializeField]
    private AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private float timer = 0;
    void Awake() {
        transform.localScale = Vector3.zero;
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer > showTime) {
            enabled = false;
            transform.localScale = Vector3.one;
        } else {
            transform.localScale = Vector3.one * curve.Evaluate(timer / showTime);
        }
    }
}
