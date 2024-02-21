namespace DefaultNamespace.Events
{
    public struct TransitionBetweenScenes
    {
        public string NameScene;

        public TransitionBetweenScenes(string nameScene)
        {
            NameScene = nameScene;
        }
    }
}