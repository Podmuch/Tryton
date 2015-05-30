//Explosion Controller
//  destroys the object after the specified time
using UnityEngine;

namespace Asteroids.Explosion
{
    public class ExplosionController : MonoBehaviour
    {
        private float lifeTime = 0.5f;
        // Update is called once per frame
        void Update()
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime < 0) Destroy(gameObject);
        }
    }
}
