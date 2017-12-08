namespace TheGame
{
    public class Save
    {
        public Save(Player player, string saveName)
        {
            string[] save = PlayerToFile(player);
            System.IO.File.WriteAllLines(@"Saves\" + saveName, save);
        }

        private string[] PlayerToFile(Player player)
        {

        }

        private int 
    }
}