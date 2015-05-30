//Static Sounds
using UnityEngine;

namespace Asteroids.Static
{
    public class StaticSound : MonoBehaviour
    {
        public AudioSource[] sounds;

        private void Awake()
        {
            //soundtrack
            sounds[0].Play();
            //alternative soundtrack <-not used
            sounds[1].Stop();
            //player shoot
            sounds[2].Stop();
            //enemy shoot
            sounds[3].Stop();
        }
    }
}
