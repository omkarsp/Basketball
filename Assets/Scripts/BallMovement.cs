using System;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 initialPos;
    private bool isShot = false;

    [SerializeField]
    private Button shootButton;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        initialPos = transform.position;
    }

    #region unused code

    private Vector3 targetPos = new Vector3(0, -162);

    private float launchAngle = 45;

    private bool preferSmallAng = false;

    private void Shoot()
    {
        Debug.Log("shoot");

        float distance = Vector3.Distance(transform.position, new Vector3(targetPos.x, transform.position.y, targetPos.z));
        Debug.Log("distance: " + distance);

        double height = Math.Abs(distance * Math.Tan(this.launchAngle * (180 / Math.PI)) / 4);
        Debug.Log("height: " + height);

        double horizontalVelocity = distance / (/*this.projectileDuration **/ Math.Cos(this.launchAngle));

        double verticalVelocity = (height + (0.5 * Physics.gravity.magnitude/** this.projectileDuration * this.projectileDuration*/))
          / (/*this.projectileDuration **/ Math.Sin(this.launchAngle * (180 / Math.PI)));

        rb.velocity = new Vector3((float)horizontalVelocity, (float)verticalVelocity);

        Debug.Log("velocity magnitude: " + rb.velocity.magnitude);
        Debug.Log("target X pos: " + this.targetPos.x + "target X pos: " + this.targetPos.y);
    }

    float Magnitude_ToReachXY_InGravity_AtAngle(float x, float y, float g, float ang)
    {
        var sin2Theta = Math.Sin(2 * ang * (180 / Math.PI));
        var cosTheta = Math.Cos(ang * (180 / Math.PI));
        var inner = (x * x * g) / ((x * sin2Theta) - (2 * y * cosTheta * cosTheta));
        if (inner < 0)
        {
            return float.NaN;
        }
        var res = (float)Math.Sqrt(inner);
        return res;
    }

    float Angle_ToReachXY_InGravity_AtMagnitude(float x, float y, float g, float mag)
    {
        var innerSq = Math.Pow(mag, 4) - g * (g * x * x + 2 * y * mag * mag);
        if (innerSq < 0)
        {
            return float.NaN;
        }
        double innerATan;
        if (this.preferSmallAng)
        {
            innerATan = (mag * mag - Math.Sqrt(innerSq)) / (g * x);
        }
        else
        {
            innerATan = (mag * mag + Math.Sqrt(innerSq)) / (g * x);
        }

        var res = (float)(Math.Atan(innerATan) * (180 / Math.PI));
        return res;
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ground") rb.isKinematic = true;
        transform.position = initialPos;
        shootButton.interactable = true;
        isShot = false;
        transform.rotation = Quaternion.identity;
    }

    public void ShootNew()
    {
        rb.isKinematic = false;
        rb.AddForce(0, 400, 400);
        isShot = true;
        shootButton.interactable = false;
    }

    private void FixedUpdate()
    {
        if (isShot) transform.Rotate(20, 5, 2);
    }
}

