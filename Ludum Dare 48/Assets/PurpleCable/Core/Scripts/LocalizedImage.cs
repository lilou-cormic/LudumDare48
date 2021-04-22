using UnityEngine;
using UnityEngine.UI;

namespace PurpleCable
{
    [RequireComponent(typeof(Image))]
    public class LocalizedImage : MonoBehaviour
    {
        private Image Image = null;

        [SerializeField] Sprite SpriteFr = null;
        [SerializeField] Sprite SpriteEs = null;
        [SerializeField] Sprite SpritePt = null;
        [SerializeField] Sprite SpriteDe = null;
        [SerializeField] Sprite SpriteNl = null;
        [SerializeField] Sprite SpriteTr = null;
        [SerializeField] Sprite SpriteRu = null;

        private void Awake()
        {
            Image = GetComponent<Image>();
            TextManager.LanguageChanged += TextManager_LanguageChanged;
        }

        private void OnDestroy()
        {
            TextManager.LanguageChanged -= TextManager_LanguageChanged;
        }

        private void Start()
        {
            SetSprite();
        }

        private void SetSprite()
        {
            switch (TextManager.CurrLanguage)
            {
                case "French":
                    Image.sprite = SpriteFr;
                    break;

                case "Spanish":
                    Image.sprite = SpriteEs;
                    break;

                case "Portuguese":
                    Image.sprite = SpritePt;
                    break;

                case "German":
                    Image.sprite = SpriteDe;
                    break;

                case "Dutch":
                    Image.sprite = SpriteNl;
                    break;

                case "Turkish":
                    Image.sprite = SpriteTr;
                    break;

                case "Russian":
                    Image.sprite = SpriteRu;
                    break;
            }

            if (Image.sprite == null)
                gameObject.SetActive(false);
            else
                Image.SetNativeSize();
        }

        private void TextManager_LanguageChanged()
        {
            SetSprite();
        }
    }
}
