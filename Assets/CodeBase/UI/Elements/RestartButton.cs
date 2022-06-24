using CodeBase.Hero;
using CodeBase.Infrastructure.Restart;
using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class RestartButton : MonoBehaviour
    {
        
        private Button _button;
        
        private IRestartService _restartService;

        [Inject]
        private void Construct(IRestartService restartService) => 
            _restartService = restartService;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick() => 
            _restartService.RestartGame();
    }
}
