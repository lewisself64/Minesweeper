namespace System.Windows.Forms
{
    internal class MineButton : Button
    {
        public int x;
        public int y;
        public int mineCount;

        public bool flagged;
        public bool mine;

        public MineButton(int pX, int pY)
        {
            y = pY;
            x = pX;
            mine = false;
            flagged = false;

            // Set the padding for the button spacing
            this.Margin = new System.Windows.Forms.Padding(1);
        }
    }
}