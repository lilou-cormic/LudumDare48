using UnityEngine;

namespace PurpleCable
{
    public class LocalizedGameObject : MonoBehaviour
    {
        [SerializeField] string Language = "French";

        private void Start()
        {
            if (TextManager.CurrLanguage != Language)
                gameObject.SetActive(false);
        }

        private void OnValidate()
        {
            if (gameObject.name.Length > 3)
            {
                Language = GetLanguage(gameObject.name.Substring(gameObject.name.Length - 3));

                string GetLanguage(string langCode)
                {
                    switch (langCode)
                    {
                        case "_En":
                            return "English";

                        case "_Fr":
                            return "French";

                        case "_Es":
                            return "Spanish";

                        case "_Pt":
                            return "Portuguese";

                        case "_De":
                            return "German";

                        case "_Nl":
                            return "Dutch";

                        case "_Tr":
                            return "Turkish";

                        case "_Ru":
                            return "Russian";
                    }

                    return Language;
                }
            }
        }
    }
}
