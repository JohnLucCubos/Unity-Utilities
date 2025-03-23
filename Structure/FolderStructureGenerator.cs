using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;

namespace MyUnity.Utilities
{
    public class FolderStructureGenerator : EditorWindow
    {
        static TextField userInput;
        [SerializeField] string defaultLocation = "\\Unity-Utilities\\Scripts\\Structure\\FolderStructure";
        [MenuItem("Tools/Folder Structure Generator")]
        public static void ShowWindow()
        {
            GetWindow(typeof(FolderStructureGenerator));
        }

        private void CreateGUI()
        {
            // * Future Update:
            // - add a drag and drop feature
            // Metadata
            titleContent = new GUIContent("Folder Structure Generator");
            maxSize = new Vector2(400, 100);
            // Base elements
            VisualElement root = rootVisualElement;
            Box box = new Box();    // Used to separate each element into a block
            // VisualElements objects can contain other VisualElement following a tree hierarchy
            box.Add(new Label("Write the name of the json file:"));
            userInput = new TextField();
            userInput.value = defaultLocation;
            userInput.label = "json";
            box.Add(userInput);
            // Create button
            Button button = new Button(null, GenerateFolderStructure);
            button.name = "Generate";
            button.text = "Generate";
            box.Add(button);

            root.Add(box);
        }

        private static void GenerateFolderStructure()
        {
            // Load folders from JSON file
            string _jsonPath = Path.Combine(Application.dataPath, userInput.text + ".json");
            if (!File.Exists(_jsonPath))
            {
                Debug.LogError($"json file not found at .../Assets/{userInput.text}");
                return;
            }

            string _jsonContent = File.ReadAllText(_jsonPath);
            FolderList _folderList = JsonUtility.FromJson<FolderList>(_jsonContent);
            string[] _folders = _folderList.folders;

            foreach (string _folder in _folders)
            {
                string _folderPath = "Assets/" + _folder.Replace("\\", "/"); // Ensure path format
                // skip if file already exists
                if (AssetDatabase.IsValidFolder(_folderPath))
                    continue;

                string _parentFolder = Path.GetDirectoryName(_folderPath);
                string _newFolderName = Path.GetFileName(_folderPath);

                AssetDatabase.CreateFolder(_parentFolder, _newFolderName);
                Debug.Log("Created folder: " + _folderPath);
            }

            AssetDatabase.Refresh(); // Refresh once after all folder creation
            Debug.Log("Folder structure generated successfully from JSON!");
        }

        [System.Serializable]
        private class FolderList
        {
            public string[] folders = null;
        }
    }

}
