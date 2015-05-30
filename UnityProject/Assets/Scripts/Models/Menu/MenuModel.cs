//Menu Model
//  send onClick function to the play button
using System;

namespace Asteroids.Menu 
{
    //inherits from base abstract class for all Models (drawing)
    class MenuModel : AbstractModel
    {
        public MenuModel()
        {
            DrawParams=new Action(() => { });
        }
        public MenuModel(Action _onClick)
        {
            DrawParams = _onClick;
        }     
    }
}
