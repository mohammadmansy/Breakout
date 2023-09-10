using UnityEngine;

public class BallReflection : MonoBehaviour
{
    public Vector2 ReflectBallVelocity(Vector2 ballVelocity, Vector2 colliderNormal)
    {
        // Calculate the dot product between the ball velocity and the collider normal
        float dotProduct = Vector2.Dot(ballVelocity, colliderNormal);

        // Calculate the reflected velocity by subtracting twice the dot product from the original velocity
        Vector2 reflectedVelocity = ballVelocity - 2f * dotProduct * colliderNormal;
      //  BouncyBall.audio.Play();


        return reflectedVelocity;
    }
}