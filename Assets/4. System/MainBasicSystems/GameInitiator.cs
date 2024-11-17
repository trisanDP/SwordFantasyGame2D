using OriginL.Player;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;

namespace BrokenLands {
    public class GameInitiator : MonoBehaviour {
        [SerializeField] PlayerScript playerScript;
        [SerializeField] GameManager gameManager;
        [SerializeField] UiManager uiManager;
        [SerializeField] GameAssets gameAssets;
        [SerializeField] GameObject Level;
        [SerializeField] GameObject UICanvas;
        [SerializeField] CameraManager cameraManager;


        [SerializeField] GameObject loadingScreen;


/*        public async void Start() {
            BindObjects();
            await InitializeObjects();
            await CreateObjects();
            loadingScreen?.SetActive(true);// make it a Script instead of gameobject and do LoadingScreen.Show();
            await BeginGame();
        }

        void BindObjects() {
            cameraManager = Instantiate(cameraManager);
            if(GameManager.Instance == null) {
                gameManager = Instantiate(gameManager);
            }
            if(UiManager.Instance == null) {
                uiManager = Instantiate(uiManager);
            }


        }

        private async UniTask InitializeObjects() {
            await _inputSystem.enable();
        }


        async UniTask CreateObjects() { //Load Heavy Objects
            gameAssets = Instantiate(gameAssets);
            Level = Instantiate(Level);
            UICanvas = Instantiate(UICanvas);
            playerScript = Instantiate(playerScript);
        }

        void PrepareGame() {

        }

        void BeginGame() {

        }*/
    }
}
