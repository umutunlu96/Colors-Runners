using Enums;

namespace Commands
{
    public class SaveGameCommand<T>
    {
        public void OnSaveGameData(SaveStates state, T data)
        {
           
            switch(state)
            {
                case SaveStates.Score: ES3.Save("Score", data);break;
                case SaveStates.Level: ES3.Save("Level", data);break;
                case SaveStates.Saturation: ES3.Save("Saturation", data);break;
                case SaveStates.PayedAmouth: ES3.Save("PayedAmouth", data);break;
            }

        }
    }
}