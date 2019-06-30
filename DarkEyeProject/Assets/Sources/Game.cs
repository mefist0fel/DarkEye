using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Game : MonoBehaviour {
    public static Game Instance = null;
    [SerializeField]
    private Camera controlCamera = null;
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private Transform aimPivot = null;
    [SerializeField]
    private Transform muzzlePivot = null;
    [SerializeField]
    private Bullet bulletPrefab = null;

    private Vector3 targetPosition;
    private Plane backPlane = new Plane(Vector3.back, new Vector3(0, 0, 20));

    void Awake() {
        Instance = this;
    }

    void Update() {
        var mouseRay = controlCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit, 100)) {
            target.position = hit.point;
            targetPosition = hit.point;
            AimMoveToDirection(hit.point);
        } else {
            if (backPlane.Raycast(mouseRay, out var enter)) {
                var point = mouseRay.direction * enter;
                targetPosition = point;
                target.position = point;
                AimMoveToDirection(point);
            }
        }

        if (Input.GetMouseButtonDown(0)) {
            Fire();
        }
    }

    private void Fire() {
        var bullet = Instantiate(bulletPrefab);
        var direction = targetPosition - muzzlePivot.position;
        bullet.Fire(muzzlePivot.position, direction.normalized);
    }

    private void AimMoveToDirection(Vector3 point) {
        var directionVector = aimPivot.position - point;
        aimPivot.LookAt(point, Vector3.up);
    }
}
