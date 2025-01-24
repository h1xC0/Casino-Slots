using UnityEngine;
using Zenject;

namespace Slots.Game.Audio
{
    public class BackgroundMusic : MonoBehaviour
    {
        private IAudioService _audioService;

        [Inject]
        private void Initialize(IAudioService audioService)
        {
            _audioService = audioService;
            _audioService.Play("Casino Crime Funk", true);
            _audioService.SetVolume("Casino Crime Funk", 0.3f);
        }
    }
}