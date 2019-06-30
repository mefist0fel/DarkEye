using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {
    [SerializeField]
    private AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField]
    private float showTime = 1.5f;
    private float timer = 0;
    private Quaternion startRotation = Quaternion.identity;
    private Quaternion endRotation = Quaternion.identity;
    void Start() {
        startRotation = transform.localRotation;
        endRotation = Quaternion.LookRotation(Game.Instance.transform.position - transform.position, Random.insideUnitSphere.normalized);
    }

    void Update() {
        timer += Time.deltaTime;
        if (timer > showTime) {
            timer = showTime;
            enabled = false;
        }
        transform.localRotation = Quaternion.Lerp(startRotation, endRotation, curve.Evaluate(timer / showTime));
    }
}
