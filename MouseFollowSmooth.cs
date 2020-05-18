private void MouseFollow() {  

        // get mouse pos in screen coordinates and convert to world
        var mousePosition = Input.mousePosition;
		// add camera z value
        mousePosition.z = 100;

        // Vector between mouse point and pivot
        MouseLookPos = Camera.main.ScreenToWorldPoint(mousePosition) - LeftArm.position;

        // angle from above vector for vertical rotation
        var angle           = Mathf.Atan2(MouseLookPos.z, MouseLookPos.y) * Mathf.Rad2Deg;
        var clampedAngle    = 0f;
        angle               = (LookDirection == LookDirection.RIGHT) ? -angle : angle;
        clampedAngle = Mathf.Clamp(angle, 45, 120);

        // create rotation using found angle
        var angleOffset = 95;
        Quaternion leftRotation = Quaternion.AngleAxis(clampedAngle - angleOffset, Vector3.forward);
        Quaternion rightRotation = Quaternion.Inverse(Quaternion.AngleAxis(clampedAngle - angleOffset, Vector3.forward));

        LeftArm.localRotation = Quaternion.Slerp(LeftArm.localRotation, leftRotation, gunRotationSpeed);
        RightArm.localRotation = Quaternion.Slerp(RightArm.localRotation, rightRotation, gunRotationSpeed);

        Debug.DrawRay(LeftArm.position, MouseLookPos, Color.red);
    }