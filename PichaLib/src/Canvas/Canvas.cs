using System.Collections.Generic;

namespace PichaLib
{
    public class Canvas
    {
        public SortedDictionary<int, Layer> Layers = new SortedDictionary<int, Layer>();
        public (int W, int H) Size = (16, 16);

        // useful for the app only.
        public bool AutoGen = false;
        public float TimeToGen = 3f;
        public Chroma TransparencyFG = Chroma.CreateFromHex("#298c8c8c");
        public Chroma TransparencyBG = new Chroma(.1f, .1f, .1f, 0f);
    }
}